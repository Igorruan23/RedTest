using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private IState currentState;
    //uso da statemachine para gerir animações
    private AnimationController animationController;
    SpecialManager specialManager;

    public float moveSpeed = 5f;
    private bool canUseSpecial = false;

    private void Start()
    {
        animationController = GetComponent<AnimationController>();
        ChangeState(new IdleState(animationController));
        specialManager = FindFirstObjectByType<SpecialManager>();
        specialManager.onSpecialReady += () => canUseSpecial = true;
    }

    private void Update()
    {
        currentState?.ExecuteState();

        StateTransition();
    }

    void StateTransition()
    {
        if (currentState is AttackState attackState && attackState.IsComboFinished())
        {
            ChangeState(new IdleState(animationController));
            return;
        }
        else if (currentState is SpecialState specialState && specialState.IsSpecialFinished())
        {
            ChangeState(new IdleState(animationController));
            return;
        }


        if (currentState is not AttackState && currentState is not SpecialState)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                ChangeState(new AttackState(animationController));
            }
            else if (Input.GetButtonDown("Fire2") && canUseSpecial)
            {
                canUseSpecial = false;
                specialManager.UseSpecial();
                ChangeState(new SpecialState(animationController, transform));
            }
            else if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                ChangeState(new WalkState(animationController, transform, moveSpeed));
            }
            else if (currentState is not IdleState)
            {
                ChangeState(new IdleState(animationController));
            }
        }
    }

    public void ChangeState(IState newState)
    {
        currentState?.ExitState(); 
        currentState = newState;
        currentState?.EnterState(); 
    }
}
