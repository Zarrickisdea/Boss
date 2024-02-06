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

                BlockerController blockerController = blockers[i].GetComponent<BlockerController>();

                if (blockerController != null && blockerController.enabled)
                {
                    blockerController.ChangeState("blockerPathTravelState");
                }
            }
        }
        triggerSwitch.enabled = false;
    }

    public void ErraticBlockers()
    {
        for (int i = 0; i < blockers.Length; i++)
        {
            if (blockers[i] != null)
            {
                BlockerController blockerController = blockers[i].GetComponent<BlockerController>();

                if (blockerController != null && blockerController.enabled)
                {
                    blockerController.ChangeState("blockerUpState");
                }
            }
        }
    }
}
