using UnityEngine;
using System.Collections;

public class BlockerControl : MonoBehaviour
{
    [SerializeField] private float upDistance = 2f;
    [SerializeField] private float upTime = 1f;
    [SerializeField] private float downTime = 1f;
    [SerializeField] private float waitTime = 2f;

    private Vector3 initialPosition;
    private bool isWorking = true;
    private Coroutine moveCoroutine;

    public float WaitTime
    {
        get => waitTime;
        set => waitTime = value;
    }

    private void Start()
    {
        initialPosition = transform.position;

        moveCoroutine = StartCoroutine(MoveUpAndDownCoroutine());
    }

    private IEnumerator MoveUpAndDownCoroutine()
    {
        while (isWorking)
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

    private void OnDisable()
    {
        isWorking = false;
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
            moveCoroutine = null;
        }
        GetComponent<BlockerVertexPath>().enabled = true;
    }
}
