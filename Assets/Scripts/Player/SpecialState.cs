using UnityEngine;

public class SpecialState : IState
{
    private AnimationController animationControl;
    private StateMachine stateMachine;
    private Transform playerTransform;
    private Audiomanager audiomanager;

    #region
    private bool isSpecialFinished = false;
    private float specialDuration = 2f; 
    private float timer = 0f;
    private Vector3 initialPosition; //manter a posição inicial do jogador statica
    #endregion

    public SpecialState(AnimationController animationController, Transform transform, Audiomanager audio)
    {
        Debug.Log("entrando especial");
        animationControl = animationController;
        playerTransform = transform;
        audiomanager = audio;

        
        initialPosition = playerTransform.position;
    }

    public void EnterState()
    {
        animationControl.PlayAnimation("Especial");
        audiomanager.PlaySfx(audiomanager.SpecialAudio);
        isSpecialFinished = false;
        timer = 0f;
    }

    public void ExecuteState()
    {
        timer += Time.deltaTime;

        playerTransform.position = initialPosition;

        if (timer >= specialDuration)
        {
            ReturnIdle();
        }

    }

    public void ExitState()
    {

    }

    public bool IsSpecialFinished() => isSpecialFinished;
    void ReturnIdle() => isSpecialFinished = true;
}
