using UnityEngine;

public class BlockerPathTravelState : BlockerBaseState, IMoveInPath
{
    private Transform[] pathPoints;
    private int currentPointIndex;
    private float speed;
    private float rotationTime;

    private Quaternion targetRotaion;

    public BlockerPathTravelState(StateMachine stateMachine, BlockerController blockerController, Transform[] pathPoints, float moveSpeed, float rotTime) : base(stateMachine, blockerController)
    {
        this.pathPoints = pathPoints;
        speed = moveSpeed;
        rotationTime = rotTime;

    }

    public override void Enter()
    {
        base.Enter();
        currentPointIndex = 0;
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

        moveInPath();
        rotateAtDestination();
    }

    public void moveInPath()
    {
        blockerController.transform.position = Vector3.MoveTowards(blockerController.transform.position, pathPoints[currentPointIndex].position, speed * Time.deltaTime);
        blockerController.transform.rotation = Quaternion.RotateTowards(blockerController.transform.rotation, targetRotaion, 90f / rotationTime * Time.deltaTime);
    }

    public void rotateAtDestination()
    {
        if (Vector3.Distance(blockerController.transform.position, pathPoints[currentPointIndex].position) < 0.01f)
        {
            currentPointIndex = (currentPointIndex + 1) % pathPoints.Length;
            SetTargetRotation();
        }
    }

    private void SetTargetRotation()
    {
        targetRotaion = Quaternion.Euler(0f, 0f, pathPoints[currentPointIndex].rotation.eulerAngles.z);
    }
}
