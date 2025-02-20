using UnityEngine;

public class WalkState : IState
{
    private AnimationController animationControl;
    private Transform playerTransform;
    private float moveSpeed;
    

    public WalkState(AnimationController animationController,Transform transform, float speed)
    {
        animationControl = animationController;
        playerTransform = transform;
        moveSpeed = speed;
    }
    public void EnterState()
    {
        animationControl.PlayAnimation("Walk");
    }

    public void ExecuteState()
    {
        //Controles invertidos e input horizontal negativo para n�o haver troca de eixos
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        if(direction.magnitude > 0.1f)
        {           
            playerTransform.Translate(direction * moveSpeed * Time.deltaTime , Space.World);
            if(horizontal > 0) playerTransform.rotation = Quaternion.Euler(0, 90, 0); //face do jogador para frente
            else if(horizontal < 0) playerTransform.rotation = Quaternion.Euler(0 ,-90, 0); //face do jogador para tras
        }
    }

    public void ExitState()
    {
      
    }
}
