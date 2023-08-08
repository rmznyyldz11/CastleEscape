using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackAnim : MonoBehaviour
{

    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }


    public void EnemyAttackStart()
    {
        animator.SetLayerWeight(1, 1);
    }

    void Update()
    {
        
    }
}
