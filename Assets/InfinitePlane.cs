using UnityEngine;

public class InfinitePlane : MonoBehaviour
{
    // Plane size settings
    private Vector3 planeSize = new Vector3(100, 1, 100);
    private float scalingThreshold = 10;
    private Vector3 maxPlaneSize = new Vector3(3000, 1, 3000);
    private Vector3 minPlaneSize = new Vector3(100, 1, 100);
    private Vector3 optimalPlaneSize = new Vector3(30, 1, 30);

    // Player and plane contents
    private Transform playerTransform;
    private Transform planeContents;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        transform.localScale = planeSize;
        planeContents = new GameObject("Plane Contents").transform;
        planeContents.parent = transform;
    }

    private void Update()
    {
        if (Mathf.Abs(playerTransform.position.x - transform.position.x) > transform.localScale.x / 2 - scalingThreshold ||
            Mathf.Abs(playerTransform.position.z - transform.position.z) > transform.localScale.z / 2 - scalingThreshold)
        {
            Vector3 playerMovement = playerTransform.position - transform.position;
            playerMovement.y = 0;
            playerMovement.Normalize();

            Vector3 oldPlaneSize = planeSize;
            planeSize.x *= playerMovement.x > 0 ? 2 : 1;
            planeSize.z *= playerMovement.z > 0 ? 2 : 1;

            planeSize.x = Mathf.Clamp(planeSize.x, minPlaneSize.x, maxPlaneSize.x);
            planeSize.z = Mathf.Clamp(planeSize.z, minPlaneSize.z, maxPlaneSize.z);

            transform.localScale = planeSize;

            Vector3 shiftAmount = (oldPlaneSize - planeSize) / 2;
            foreach (Transform child in planeContents)
            {
                child.position -= shiftAmount;
            }

            playerTransform.position = new Vector3(
                Mathf.Clamp(playerTransform.position.x, transform.position.x - transform.localScale.x / 2, transform.position.x + transform.localScale.x / 2),
                playerTransform.position.y,
                Mathf.Clamp(playerTransform.position.z, transform.position.z - transform.localScale.z / 2, transform.position.z + transform.localScale.z / 2));
        }

        if (transform.localScale != optimalPlaneSize)
        {
            transform.localScale = optimalPlaneSize;
        }
    }
}
