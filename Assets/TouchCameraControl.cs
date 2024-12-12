using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class TouchCameraControl : MonoBehaviour
{
    public float rotationSpeed = 1.0f;
    public float verticalDamping = 0.5f;
    public float horizontalDamping = 0.5f;
    public float rotationSmoothness = 5f;
    public Transform playerTransform;
    public bool useGyroscope = true;
    public bool useLerpForGyro = false;
    private Gyroscope gyro;


    private float touchX;
    private float touchY;
    private float cameraRotationX;
    private float cameraRotationY;
    public Vector3 cameraOffset;

    private BrightnessToggle brightnessToggle;
    private PostProcessVolume postProcessVolume;
    private ColorGrading colorGrading;

    public Button placeButton;

    void Start()
    {
        cameraRotationX = 0f;
        cameraRotationY = 0f;
        transform.position = playerTransform.position + cameraOffset;

        brightnessToggle = GetComponent<BrightnessToggle>();
        postProcessVolume = GetComponent<PostProcessVolume>();

        if (postProcessVolume != null && postProcessVolume.profile.TryGetSettings(out colorGrading))
        {
            if (brightnessToggle != null)
            {
                SetBrightness(brightnessToggle.IsBrightnessDimmed ? 0.05f : 1f);
            }
        }

        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
        }
    }

    void Update()
    {
        if (useGyroscope && gyro != null)
        {
            if (!gyro.enabled)
            {
                gyro.enabled = true;
            }

            Quaternion gyroRotation = Quaternion.Euler(90f, 0f, 0f) * gyro.attitude * Quaternion.Euler(0f, 0f, 180f);
            Quaternion targetRotation = playerTransform.rotation * gyroRotation;
            if (useLerpForGyro)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSmoothness);
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSmoothness);
            }
        }
        else
        {
            if (gyro != null && gyro.enabled)
            {
                gyro.enabled = false;
            }

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                // Add button's rect to avoid touching it
                RectTransform buttonRect = placeButton.gameObject.GetComponent<RectTransform>();
                float buttonHeight = buttonRect.rect.height;

                if (touch.position.x > Screen.width / 2 && touch.position.y > buttonHeight)
                {
                    touchX = -touch.deltaPosition.x * rotationSpeed * horizontalDamping * Time.deltaTime;
                    touchY = -touch.deltaPosition.y * rotationSpeed * verticalDamping * Time.deltaTime;

                    cameraRotationY += touchX;
                    cameraRotationX -= touchY;

                    // Adjust the cameraRotationX to allow 360-degree rotation
                    cameraRotationX = cameraRotationX % 360f;
                }
            }

            Quaternion targetRotation = Quaternion.Euler(cameraRotationX, cameraRotationY, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSmoothness);
        }

        transform.position = playerTransform.position + cameraOffset;

        if (brightnessToggle != null)
        {
            if (brightnessToggle.IsBrightnessDimmed)
            {
                SetBrightness(0.05f);
            }
            else
            {
                SetBrightness(1f);
            }
        }
    }

    private void SetBrightness(float brightness)
    {
        if (colorGrading != null)
        {
            colorGrading.postExposure.value = Mathf.Lerp(-5f, 0f, brightness);
        }
    }

    public void ToggleGyroscope()
    {
        if (SystemInfo.supportsGyroscope)
        {
            useGyroscope = !useGyroscope;

            if (useGyroscope && gyro == null)
            {
                gyro = Input.gyro;
            }

            if (gyro != null)
            {
                gyro.enabled = useGyroscope;
            }
        }
    }

    public void ToggleGyroInterpolation()
    {
        useLerpForGyro = !useLerpForGyro;
    }
}
