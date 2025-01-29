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
    }

    public void PerformAttack()
    {
        if (isComboFinished) return;

        attackTimer = 0f;

        if(attackCount < 2)
        {
            attackCount++;
            AnimationPunch(attackCount.ToString());
        }
        else
        {
            ReturnIdle();
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
