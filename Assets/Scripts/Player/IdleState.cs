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
        Debug.Log("Idle");
        animationControl.PlayAnimation("Idle");
    }

    public void ExecuteState()
    {
        //executar
    }

    public void ExitState()
    {
        //sair
    }
}
