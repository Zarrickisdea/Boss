using UnityEngine;

public class BlockerDownState : BlockerBaseState, IMoveInDirection
{
    private Vector3 direction;
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private float downOnTimer;
    private float downOffTimer;

    private float distance;

    public BlockerDownState(StateMachine stateMachine, BlockerController blockerController, float onTimer, float offTimer, float distance) : base(stateMachine, blockerController)
    {
        downOnTimer = onTimer;
        downOffTimer = offTimer;
        this.distance = distance;
    }

    public override void Enter()
    {
        base.Enter();
        setDirection(Vector3.down);
        initialPosition = blockerController.transform.position;
        targetPosition = initialPosition + direction * distance;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();
        if (stateOnTimer < downOnTimer)
        {
            stateOn(downOnTimer);
        }
        else
        {
            stateOff(downOffTimer);
        }
    }

    public void stateOn(float seconds)
    {
        stateOnTimer += Time.deltaTime;
        moveInDirection(targetPosition);
    }

    public void stateOff(float seconds)
    {
        stateOffTimer += Time.deltaTime;
        if (stateOffTimer >= seconds)
        {
            stateMachine.ChangeState(blockerController.GetState("blockerUpState"));
        }
    }

    public void setDirection(Vector3 direction)
    {
        this.direction = direction;
    }

    public void moveInDirection(Vector3 targetPosition)
    {
        blockerController.transform.position = Vector3.Lerp(initialPosition, targetPosition, stateOnTimer / downOnTimer);
    }
}
