using UnityEngine;

public class FollowMainCamera : MonoBehaviour
{
    private Transform mainCamera;

    void Start()
    {
        mainCamera = Camera.main.transform;
    }

    void Update()
    {
        // Set the position of the light to the position of the main camera
        transform.position = mainCamera.position;

        // Set the rotation of the light to the rotation of the main camera
        transform.rotation = mainCamera.rotation;
    }
}