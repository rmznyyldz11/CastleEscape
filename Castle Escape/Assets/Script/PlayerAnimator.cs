using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeedMultiplier;


    public void ManageAnimations(Vector3 moveVector)
    {
            if (moveVector.magnitude > 0 && !GameManager.instance.Fight)
            {
                animator.SetFloat("moveSpeed", moveVector.magnitude * moveSpeedMultiplier);
                PlayWalkAnim();
                animator.transform.forward = moveVector.normalized;
            }
          
            else
            {
                if(!GameManager.instance.Fight && !GameManager.instance.die && !GameManager.instance.attack)
                PlayIdleAnim();
            }
    }

    private void Update()
    {
        if(GameManager.instance.attack == true)
        {
            animator.SetLayerWeight(1, 1);
        }
        else if(GameManager.instance.attack == false)
        {
            animator.SetLayerWeight(1, 0);
        }
    }

    private void PlayWalkAnim()
    {
        animator.Play("MainCharWalking");
    }

    public void PlayIdleAnim()
    {
        animator.Play("MainCharIdle");
        
    }

    public void PlayDieAnim()
    {
        animator.SetBool("die",true);
    }

   


}
