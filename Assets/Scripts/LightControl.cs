using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightControl : MonoBehaviour
{
    [SerializeField] private float raycastDistance;
    [SerializeField] private Vector3 direction;
    private Light2D light2D;

    private void Awake()
    {
        light2D = GetComponent<Light2D>();
    }

    // very expensive, but near perfect simulation of a search light in 2D environment
    private void FixedUpdate()
    {
        Vector3 raycastDirection = transform.TransformDirection(direction);
        float upAngle = light2D.pointLightOuterAngle / 2;
        float downAngle = -upAngle;

        // only good for smaller cones, for bigger ones use more than 3 rays, angles will have to be calculated based on the light
        RaycastHit2D[] hits = new RaycastHit2D[3];
        float[] angles = { 0f, upAngle, downAngle };

        for (int i = 0; i < angles.Length; i++)
        {
            Vector3 direction = Quaternion.Euler(0f, 0f, angles[i]) * raycastDirection;
            hits[i] = Physics2D.Raycast(transform.position, direction, raycastDistance);

            if (hits[i].collider != null && hits[i].collider.CompareTag("Player"))
            {
                PlayerController playerController = hits[i].collider.GetComponent<PlayerController>();
                if (playerController != null)
                {
                    playerController.Burn(); // Call the desired function on the player controller
                }
            }
        }



        //Debug.DrawRay(transform.position, raycastDirection * raycastDistance, Color.red);
        //Debug.DrawRay(transform.position, upDirection * raycastDistance, Color.green);
        //Debug.DrawRay(transform.position, downDirection * raycastDistance, Color.blue);
    }
}




