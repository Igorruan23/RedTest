using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
/// <summary>
/// Gerencia os diferentes estados do personagem, controlando animações, transições e interações.
/// Tambem lida com a ativação temporária da hitbox de ataque.
/// </summary>
public class StateMachine : MonoBehaviour
{
    private IState currentState;
    //uso da statemachine para gerir animações
    private AnimationController animationController;
    private Audiomanager audioManager;
    SpecialManager specialManager;

    #region
    [SerializeField] private GameObject attackHitbox;
    private Coroutine hitboxCoroutine;
    public float moveSpeed = 5f;
    private bool canUseSpecial = false;
    private Vector2 moveInput;
    #endregion

    private void Start()
    {
        animationController = GetComponent<AnimationController>();
        audioManager = FindFirstObjectByType<Audiomanager>();
        ChangeState(new IdleState(animationController));
        specialManager = FindFirstObjectByType<SpecialManager>();
        specialManager.onSpecialReady += () => canUseSpecial = true;
    }

    private void Update()
    {
        currentState?.ExecuteState();

        StateTransition();
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (moveInput != Vector2.zero && currentState is not WalkState)
        {
            ChangeState(new WalkState(animationController, transform, moveSpeed));
        }
        else if (moveInput == Vector2.zero && currentState is WalkState)
        {
            ChangeState(new IdleState(animationController));
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (currentState is AttackState attackState)
            {
                attackState.PerformAttack();
            }
            else
            {
                ChangeState(new AttackState(animationController, audioManager));
            }

            //Ativa o Hitbox de ataque
            ActivateHitbox();
        }
    }

    public void Special(InputAction.CallbackContext context)
    {
        if (context.performed && canUseSpecial && currentState is not SpecialState)
        {
            canUseSpecial = false;
            specialManager.UseSpecial();
            ChangeState(new SpecialState(animationController, transform, audioManager));
            ActivateHitbox();
        }
        
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
    }
    

    public void ChangeState(IState newState)
    {
        currentState?.ExitState(); 
        currentState = newState;
        currentState?.EnterState(); 
    }


    /// <summary>
    /// Ativa a hitbox do ataque e garante que ela será desativada após 0.5 segundos.
    /// Se um novo ataque for executado antes do tempo acabar, o tempo é reiniciado.
    /// </summary>
    private void ActivateHitbox()
    {
        attackHitbox.SetActive(true);
        if (hitboxCoroutine != null)
        {
            StopCoroutine(hitboxCoroutine);
        }

        hitboxCoroutine = StartCoroutine(DisableHitboxAfterDelay(0.5f));
    }


    /// <summary>
    /// Desativa a hitbox após um tempo determinado.
    /// </summary>
    private IEnumerator DisableHitboxAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        attackHitbox.SetActive(false);
    }
}
