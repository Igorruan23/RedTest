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
        animator.Play(Animation);
    }


}
