using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator;

    private void Awake()
    {   
        animator = GetComponent<Animator>();       
    }

    public void PlayAnimation(string Animation)
    {
        if (Animation == "Punch")
        {
            //garantir que a animaçao de soco seja resetada
            animator.Play(Animation, -1, 0);
        }
        else
        {
            animator.Play(Animation);
        }
    }


}
