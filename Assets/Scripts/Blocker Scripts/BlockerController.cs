using System.Collections.Generic;
using UnityEngine;

public class BlockerController : MonoBehaviour
{
    [SerializeField] private Transform[] pathPoints;

    [SerializeField] private float upDistance = 2f;
    [SerializeField] private float upTime = 1f;
    [SerializeField] private float downTime = 1f;
    [SerializeField] private float waitTime = 2f;

    private StateMachine blockerStateMachine;
    private BlockerUpState blockerUpState;
    private BlockerDownState blockerDownState;

    private Dictionary<string, BaseState> blockerStates = new Dictionary<string, BaseState>();

    public BlockerBaseState GetState(string stateName)
    {
        return (BlockerBaseState)blockerStates[stateName];
    }

    private void Awake()
    {
        blockerStateMachine = new StateMachine();

        blockerUpState = new BlockerUpState(blockerStateMachine, this);
        blockerDownState = new BlockerDownState(blockerStateMachine, this);

        blockerStates.Add("blockerUpState", blockerUpState);
        blockerStates.Add("blockerDownState", blockerDownState);
    }

    private void Start()
    {
        blockerStateMachine.Initialize(blockerUpState);
    }

    private void Update()
    {
        blockerStateMachine.Update();
    }

    private void FixedUpdate()
    {
        blockerStateMachine.FixedUpdate();
    }
}
