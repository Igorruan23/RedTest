using System.Collections;
using Unity.IO.LowLevel.Unsafe;
using UnityEditorInternal;
using UnityEngine;

public class AttackState : IState
{
    private AnimationController animationControl;
    private Audiomanager audioManager;
    #region
    public float attackTimer = 0f;
    private int attackCount = 0;
    private readonly float comboTimeout = 1f;
    private bool isComboFinished = false;
    private float finalComboDelay = 0.2f;
    private float comboDelay = 0f;
    #endregion


    public AttackState (AnimationController animationController, Audiomanager audio)
    {
        animationControl = animationController;
        audioManager = audio;
    }


    public void EnterState()
    {
        AnimationPunch(attackCount.ToString());
        attackCount = 0;
        attackTimer = 0f;
        isComboFinished = false;
    }

    public void ExecuteState()
    {
        
        attackTimer += Time.deltaTime;
        if (attackTimer > comboTimeout)
        {
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
                    attackCount++;
                    AnimationPunch(attackCount.ToString());
                    break;
                case 1:
                    Debug.Log("Ataque 3 executado");
                    attackCount++;
                    AnimationPunch(attackCount.ToString());
                    comboDelay += Time.deltaTime;
                    if (comboDelay == finalComboDelay) ReturnIdle();
                    break;
            }
        }
    }

    public void ExitState()
    {
        
    }

    public bool IsComboFinished() => isComboFinished;
    void ReturnIdle() => isComboFinished = true;


    void AnimationPunch(string punchNumber)
    {
        animationControl.PlayAnimation($"Punch{punchNumber}");
        audioManager.PlaySfx(audioManager.PunchAudio);
    }
}
