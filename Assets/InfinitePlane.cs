using UnityEngine;

public class InfinitePlane : MonoBehaviour
{
    // Size of the plane game object.
    private Vector3 planeSize = new Vector3(100, 1, 100);

    // Threshold distance from edge of plane to trigger scaling.
    private float scalingThreshold = 10;

    // Maximum size of the plane game object.
    private Vector3 maxPlaneSize = new Vector3(3000, 1, 3000);

    // Minimum size of the plane game object.
    private Vector3 minPlaneSize = new Vector3(100, 1, 100);

    // Optimal size of the plane game object.
    private Vector3 optimalPlaneSize = new Vector3(30, 1, 30);

    // The player's transform component.
    private Transform playerTransform;

    // The contents of the plane.
    private Transform planeContents;

    private void Start()
    {
        // Get the player's transform component.
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // Set the initial scale of the plane game object.
        transform.localScale = planeSize;

        // Create an empty game object to hold the contents of the plane.
        planeContents = new GameObject("Plane Contents").transform;
        planeContents.parent = transform;
    }

    private void Update()
    {
        // Check if the player is close to the edge of the plane game object.
        if (Mathf.Abs(playerTransform.position.x - transform.position.x) > transform.localScale.x / 2 - scalingThreshold ||
            Mathf.Abs(playerTransform.position.z - transform.position.z) > transform.localScale.z / 2 - scalingThreshold)
        {
            // Determine the direction the player is moving.
            Vector3 playerMovement = playerTransform.position - transform.position;
            playerMovement.y = 0;
            playerMovement.Normalize();

            // Double the size of the plane game object in the direction the player is moving.
            Vector3 oldPlaneSize = planeSize;
            planeSize.x *= playerMovement.x > 0 ? 2 : 1;
            planeSize.z *= playerMovement.z > 0 ? 2 : 1;

            // Constrain the size of the plane game object to the maximum and minimum sizes.
            planeSize.x = Mathf.Clamp(planeSize.x, minPlaneSize.x, maxPlaneSize.x);
            planeSize.z = Mathf.Clamp(planeSize.z, minPlaneSize.z, maxPlaneSize.z);

            // Set the new scale of the plane game object.
            transform.localScale = planeSize;

            // Shift the contents of the plane to the new position.
            Vector3 shiftAmount = (oldPlaneSize - planeSize) / 2;
            foreach (Transform child in planeContents)
            {
                child.position -= shiftAmount;
            }

            // Reset the player's position to prevent clipping through the plane game object.
            playerTransform.position = new Vector3(
                Mathf.Clamp(playerTransform.position.x, transform.position.x - transform.localScale.x / 2, transform.position.x + transform.localScale.x / 2),
                playerTransform.position.y,
                Mathf.Clamp(playerTransform.position.z, transform.position.z - transform.localScale.z / 2, transform.position.z + transform.localScale.z / 2));
        }

        // Set the scale of the plane game object to the optimal size for performance.
        if (transform.localScale != optimalPlaneSize)
        {
            transform.localScale = optimalPlaneSize;
        }
    }
}

