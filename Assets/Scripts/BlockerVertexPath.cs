using UnityEngine;

public class BlockerVertexPath : MonoBehaviour
{
    [SerializeField] private Transform[] pathPoints;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float rotationTime = 2f;

    private int currentPointIndex = 0;
    private Quaternion targetRotation;
    private bool isMoving = false;

    private void Start()
    {
        if (pathPoints.Length > 0)
        {
            isMoving = true;
        }
    }

    private void Update()
    {
        if (isMoving)
        {
            // Move towards the current path point
            transform.position = Vector3.MoveTowards(transform.position, pathPoints[currentPointIndex].position, moveSpeed * Time.deltaTime);

            // Rotate towards the target rotation
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 90f / rotationTime * Time.deltaTime);

            // Check if the blocker has reached the current point
            if (Vector3.Distance(transform.position, pathPoints[currentPointIndex].position) < 0.01f)
            {
                // Move to the next point in the path
                currentPointIndex = (currentPointIndex + 1) % pathPoints.Length;

                // Set the target rotation for the next point
                SetTargetRotation();
            }
        }
    }

    private void SetTargetRotation()
    {
        targetRotation = Quaternion.Euler(0f, 0f, pathPoints[currentPointIndex].rotation.eulerAngles.z);
    }
}
