using UnityEngine;

public class BlockerUpState : BlockerBaseState, IMoveInDirection
{
    private Vector3 direction;
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private float upOnTimer = 1f;
    private float upOffTimer = 2f;

    public BlockerUpState(StateMachine stateMachine, BlockerController blockerController) : base(stateMachine, blockerController)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered Up State at position: " + blockerController.transform.position);
        setDirection(Vector3.up);
        initialPosition = blockerController.transform.position;
        targetPosition = initialPosition + direction * 2f;
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

        if (stateOnTimer < upOnTimer)
        {
            stateOn(upOnTimer);
        }
        else
        {
            stateOff(upOffTimer);
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
            stateMachine.ChangeState(blockerController.GetState("blockerDownState"));
        }
    }

    public void setDirection(Vector3 direction)
    {
        this.direction = direction;
    }

    public void moveInDirection(Vector3 targetPosition)
    {
        Debug.Log("Moving Up");
        blockerController.transform.position = Vector3.Lerp(initialPosition, targetPosition, stateOnTimer / 2f);
    }
}
