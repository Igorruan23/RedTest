using UnityEngine;

public class IdleState : IState
{
    private AnimationController animationControl;

    public IdleState(AnimationController animationController)
    {
        this.animationControl = animationController;
    }
    public void EnterState()
    {

        animationControl.PlayAnimation("Idle");
    }

    public void ExecuteState()
    {
        
    }

    public void ExitState()
    {
        
    }
}
