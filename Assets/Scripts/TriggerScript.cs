using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    [SerializeField] private GameObject[] blockers;

    private BoxCollider2D triggerSwitch;

    private void Awake()
    {
        triggerSwitch = GetComponent<BoxCollider2D>();
    }

    public void SwitchBlockers()
    {
        for (int i = 0; i < blockers.Length; i++)
        {
            if (blockers[i] != null)
            {
                BlockerControl blockerControl = blockers[i].GetComponent<BlockerControl>();

                if (blockerControl != null && blockerControl.enabled)
                {
                    blockerControl.enabled = false;
                }
            }
        }
        triggerSwitch.enabled = false;
    }

    public void ErraticBlockers()
    {
        for (int i = 0;i < blockers.Length;i++)
        {
            if (blockers[i] != null)
            {
                BlockerControl blockerControl = blockers[i].GetComponent<BlockerControl> ();

                if (blockerControl != null && blockerControl.enabled)
                {
                    blockerControl.WaitTime = Random.Range(0f, 1f);
                }
            }
        }
        triggerSwitch.enabled = false;
    }
}
