using UnityEngine;

public class BlockerDownState : BlockerBaseState, IMoveInDirection
{
    private Vector3 direction;
    private float moveSpeed;
    private float rotationTime;

    public BlockerDownState(StateMachine stateMachine, BlockerController blockerController) : base(stateMachine, blockerController)
    {
    }

    public override void Enter()
    {
        base.Enter();
        setDirection(Vector3.down);
        moveIndDirection();
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
        waitForSeconds(2f);
        //blockerController.transform.position = Vector3.Lerp(blockerController.transform.position, blockerController.transform.position + direction * 2f, 1f);
    }

    public void setDirection(Vector3 direction)
    {
        this.direction = direction;
    }

    public void waitForSeconds(float seconds)
    {
        Debug.Log("stateTimer: " + stateTimer + " at Down State");
        stateTimer += Time.deltaTime;
        if (stateTimer >= seconds)
        {
            stateMachine.ChangeState(blockerController.GetState("blockerUpState"));
        }
    }

    public void moveIndDirection()
    {
        Debug.Log("Moving Down");
        blockerController.transform.position = Vector3.Lerp(blockerController.transform.position, blockerController.transform.position + direction * 2f, 1f);
    }
}
