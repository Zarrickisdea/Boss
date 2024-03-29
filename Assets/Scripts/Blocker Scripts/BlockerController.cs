using System.Collections.Generic;
using UnityEngine;

public class BlockerController : MonoBehaviour
{
    // add editor heading
    [Header("Path Travel Settings")]
    [SerializeField] private Transform[] pathPoints;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float rotationTime = 2f;

    [Header("Blocker Up and Down Settings")]
    [SerializeField] private float distance = 2f;
    [SerializeField] private float upTime = 1f;
    [SerializeField] private float downTime = 1f;
    [SerializeField] private float waitTime = 2f;

    private StateMachine blockerStateMachine;
    private BlockerUpState blockerUpState;
    private BlockerDownState blockerDownState;
    private BlockerPathTravelState blockerPathTravelState;

    private Dictionary<string, BaseState> blockerStates = new Dictionary<string, BaseState>();

    public BlockerBaseState GetState(string stateName)
    {
        return (BlockerBaseState)blockerStates[stateName];
    }

    public void ChangeState(string stateName)
    {
        blockerStateMachine.ChangeState(blockerStates[stateName]);
    }

    private void Awake()
    {
        blockerStateMachine = new StateMachine();

        blockerUpState = new BlockerUpState(blockerStateMachine, this, upTime, waitTime, distance);
        blockerDownState = new BlockerDownState(blockerStateMachine, this, downTime, waitTime, distance);
        blockerPathTravelState = new BlockerPathTravelState(blockerStateMachine, this, pathPoints, moveSpeed, rotationTime);


        blockerStates.Add("blockerUpState", blockerUpState);
        blockerStates.Add("blockerDownState", blockerDownState);
        blockerStates.Add("blockerPathTravelState", blockerPathTravelState);
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
