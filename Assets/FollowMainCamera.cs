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
        transform.position = mainCamera.position;
        transform.rotation = mainCamera.rotation;
    }
}
