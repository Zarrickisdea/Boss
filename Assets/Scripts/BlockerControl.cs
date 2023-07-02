using UnityEngine;
using System.Collections;

public class BlockerControl : MonoBehaviour
{
    [SerializeField] private float upDistance = 2f;
    [SerializeField] private float upTime = 1f;
    [SerializeField] private float downTime = 1f;
    [SerializeField] private float waitTime = 2f;

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;

        // Start the movement coroutine
        StartCoroutine(MoveUpAndDownCoroutine());
    }

    private IEnumerator MoveUpAndDownCoroutine()
    {
        while (true)
        {
            // Move up
            yield return new WaitForSeconds(waitTime);
            yield return MoveObject(transform.position + Vector3.up * upDistance, upTime);

            // Move down
            yield return new WaitForSeconds(waitTime);
            yield return MoveObject(initialPosition, downTime);
        }
    }

    private IEnumerator MoveObject(Vector3 targetPosition, float duration)
    {
        float elapsedTime = 0f;
        Vector3 startingPosition = transform.position;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }
}
