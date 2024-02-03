using UnityEngine;

public abstract class BaseState
{
    protected StateMachine stateMachine;
    protected float stateTimer;

    public BaseState(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }

    public virtual void FixedUpdate()
    {
    }

    public virtual void Update()
    {
    }
}
