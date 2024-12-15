using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TactilePlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float idleTime = 1f; // in seconds
    private Rigidbody rb;
    private float lastInputTime;
    private Vector2 startTouchPos;
    private Vector2 currTouchPos;
    private Vector2 rotation;
    public float rotationSpeed = 1f;
    public float movementSmoothness = 10f;
    private SoundPlayer soundPlayer;
    public GameObject orbPrefab;
    private GameObject lastPlacedOrb;
    public bool isPlacingOrb = false;
    public RectTransform joystick;
    public Image joystickBackground;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameObject.SetActive(true);
        lastInputTime = Time.time;

        // Instantiate the SoundPlayer and pass the listener transform
        soundPlayer = new SoundPlayer(transform);

        //Joystick Size
        float scale = Screen.width / 1080f / 4;
        joystickBackground.rectTransform.localScale = new Vector3(scale, scale, 1);

    }

    private void Update()
    {
        bool moveHandled = false;
        bool rotateHandled = false;

        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (!moveHandled && touch.position.x < Screen.width / 2)
                {
                    HandleMovement(touch);
                    moveHandled = true;
                }
                else if (!rotateHandled && touch.position.x >= Screen.width / 2)
                {
                    HandleRotation(touch);
                    rotateHandled = true;
                }
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

        if (Time.time - lastInputTime > idleTime && rb.velocity != Vector3.zero)
        {
            rb.velocity = Vector3.zero;
        }

        // Play sounds on SoundPlayer
        soundPlayer.PlaySounds();
    }


    private void HandleMovement(Touch touch)
    {
        if (touch.phase == TouchPhase.Began)
        {
            startTouchPos = touch.position;
            // Pass the touch information to the joystick
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = startTouchPos;
            (joystick.GetComponent<DynamicJoystick>() as DynamicJoystick).OnPointerDown(eventData);
        }
        else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
        {
            currTouchPos = touch.position;
            Vector2 touchDelta = currTouchPos - startTouchPos;
            Vector3 targetMovementDirection = new Vector3(touchDelta.x, 0f, touchDelta.y).normalized;
            targetMovementDirection = Camera.main.transform.TransformDirection(targetMovementDirection);
            targetMovementDirection.y = 0f;

            // Smoothly transition between the old and new movement directions
            Vector3 newVelocity = Vector3.Lerp(rb.velocity, targetMovementDirection * moveSpeed, Time.deltaTime * movementSmoothness);
            rb.velocity = newVelocity;

            lastInputTime = Time.time;

            // Check if there is no last placed orb or if the player is far enough from it to place a new orb
            if (isPlacingOrb && (lastPlacedOrb == null || Vector3.Distance(transform.position, lastPlacedOrb.transform.position) > 0.5f))
            {
                // Add new Sound at the player's current position
                Sound newSound = new Sound(Resources.Load<AudioClip>("Sound Effects/footstep"), transform.position, 1.0f, 1.0f, Instantiate(orbPrefab));
                soundPlayer.AddSound(newSound, newSound.GetOrbObject()); // use GetOrbObject() to access the orbObject variable
                lastPlacedOrb = newSound.GetOrbObject(); // use GetOrbObject() to access the orbObject variable
            }
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            rb.velocity = Vector3.zero;
            // Pass the touch information to the joystick
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = currTouchPos;
            (joystick.GetComponent<DynamicJoystick>() as DynamicJoystick).OnPointerUp(eventData);
        }
    }


    private void HandleRotation(Touch touch)
    {
        if (touch.phase == TouchPhase.Moved)
        {
            rotation.x += -touch.deltaPosition.y * rotationSpeed * Time.deltaTime;
            rotation.y += touch.deltaPosition.x * rotationSpeed * Time.deltaTime;

            transform.rotation = Quaternion.Euler(rotation.x, rotation.y, 0);
        }
    }
}
