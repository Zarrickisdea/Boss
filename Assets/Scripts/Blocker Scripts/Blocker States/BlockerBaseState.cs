using UnityEngine;

public class BlockerBaseState : BaseState
{
    protected BlockerController blockerController;
    public BlockerBaseState(StateMachine stateMachine, BlockerController blockerController) : base(stateMachine)
    {
        this.blockerController = blockerController;
    }

    public override void Enter()
    {
        base.Enter();
        stateOnTimer = 0;
        stateOffTimer = 0;
    }

    public override void Exit()
    {
        base.Exit();
        stateOnTimer = 0;
        stateOffTimer = 0;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();
    }
}
