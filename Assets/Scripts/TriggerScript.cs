using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    [SerializeField] private GameObject[] blockers;

    public void SwitchBlockers()
    {
        for (int i = 0; i < blockers.Length; i++)
        {
            if (blockers[i] != null)
            {
                blockers[i].GetComponent<BlockerControl>().enabled = false;
            }
        }
    }
}
