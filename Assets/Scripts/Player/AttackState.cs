using System.Collections;
using Unity.IO.LowLevel.Unsafe;
using UnityEditorInternal;
using UnityEngine;

public class AttackState : IState
{
    public float attackTimer = 0f;
    private int attackCount = 0;
    private readonly float comboTimeout = 1f;
    private AnimationController animationControl;
    private bool isComboFinished = false;
    private float finalComboDelay = 0.2f;
    private float comboDelay = 0f;
    

    public AttackState (AnimationController animationController)
    {
        animationControl = animationController;
    
    }


    public void EnterState()
    {
        Debug.Log("Ataque 1");
        AnimationPunch();
        attackCount = 0;
        attackTimer = 0f;
        isComboFinished = false;
    }

    public void ExecuteState()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer > comboTimeout)
        {
            Debug.Log("passou o tempo e o combo não terminou");
            ReturnIdle();
            return;
        }
        if (Input.GetButtonDown("Fire1") && !isComboFinished)
        {
            attackTimer = 0f; 

            switch (attackCount)
            {
                case 0:
                    Debug.Log("Ataque 2 executado");
                    AnimationPunch();
                    attackCount++;
                    break;
                case 1:
                    Debug.Log("Ataque 3 executado");
                    AnimationPunch();
                    attackCount++;
                    comboDelay += Time.deltaTime;
                    if (comboDelay == finalComboDelay) ReturnIdle();
                    break;
            }
        }
    }

    public void ExitState()
    {
        //sair
    }

    public bool IsComboFinished() => isComboFinished;
    void AnimationPunch() => animationControl.PlayAnimation("Punch");
    void ReturnIdle() => isComboFinished = true;
}
