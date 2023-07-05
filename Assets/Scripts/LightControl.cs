using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightControl : MonoBehaviour
{
    [SerializeField] private float raycastDistance;
    [SerializeField] private Vector3 direction;
    [SerializeField] private float detectionTime = 2.5f;
    [SerializeField] private PlayerController player;
    [SerializeField] private Color initialColor;
    [SerializeField] private Color targetColor;
    private Light2D light2D;
    private float burnTimer;
    private bool isTimerRunning;

    private void Awake()
    {
        light2D = GetComponent<Light2D>();
    }

    private void Start()
    {
        light2D.color = initialColor;
    }

    private void FixedUpdate()
    {
        Vector3 raycastDirection = transform.TransformDirection(direction);
        float upAngle = light2D.pointLightOuterAngle / 2;
        float downAngle = -upAngle;

        RaycastHit2D[] hits = new RaycastHit2D[3];
        float[] angles = { 0f, upAngle, downAngle };

        bool playerDetected = false; // Flag to indicate if the player is detected

        for (int i = 0; i < angles.Length; i++)
        {
            Vector3 direction = Quaternion.Euler(0f, 0f, angles[i]) * raycastDirection;
            hits[i] = Physics2D.Raycast(transform.position, direction, raycastDistance);

            if (hits[i].collider != null && hits[i].collider.CompareTag("Player"))
            {
                playerDetected = true;
                break;
            }
        }

        if (playerDetected)
        {
            if (!isTimerRunning)
            {
                burnTimer = 0f;
                isTimerRunning = true;
            }
            else
            {
                burnTimer += Time.fixedDeltaTime;

                if (burnTimer >= detectionTime)
                {
                    player.Burn();
                    burnTimer = 0f;
                    isTimerRunning = false;
                }

                float t = Mathf.Clamp01(burnTimer / detectionTime);
                Color currentColor = Color.Lerp(initialColor, targetColor, t);

                light2D.color = currentColor;
            }

            AudioManager.Instance.PlayEffect(AudioManager.Effects.Detect);
        }
        else
        {
            burnTimer = 0f;
            isTimerRunning = false;

            light2D.color = initialColor;
        }
    }
}