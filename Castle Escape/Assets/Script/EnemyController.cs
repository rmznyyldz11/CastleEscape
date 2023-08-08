using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;


public enum EnemyState { First, Second,Third,Fourth }
public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject[] Targets;
    NavMeshAgent agent;
    private float distance = 1.0f;
    private float distanceToTarget;
    public int numberOfTarget = 0;
    [SerializeField]public Animator animator;
    public EnemyState EnemyState;
    public int levelValue;
  
    void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();
       
    }


    private void Update()
    {
      TransactionByType();
    
    }

    
    private void ReachTarget()
    {
       
        distanceToTarget = Vector3.Distance(Targets[numberOfTarget].transform.position, this.transform.position);
      
        if(distance < distanceToTarget)
        {
            
            agent.SetDestination(Targets[numberOfTarget].transform.position);
           
        }
        if (distanceToTarget <= 1.5f)
        {
            numberOfTarget++;
         
           
            if(numberOfTarget >= 2)
            {
                numberOfTarget = 0;
            }
        }
    }
    private void ReachTarget2()
    {

        distanceToTarget = Vector3.Distance(Targets[numberOfTarget].transform.position, this.transform.position);

        if (distance < distanceToTarget)
        {
            agent.SetDestination(Targets[numberOfTarget].transform.position);

        }
        if (distanceToTarget <= 1.5f)
        {
            numberOfTarget++;


            if (numberOfTarget >= 8)
            {
                numberOfTarget = 0;
            }
        }
    }

    public void EnemyAttackStart()
    {
        animator.SetLayerWeight(1, 1);
    }

    public void EnemyIdleStart()
    {
        animator.Play("Idle");
    }

    private void Rotate()
    {
        animator.SetLayerWeight(2, 1);
    }

    


    public void TransactionByType()
    {
        switch (EnemyState)
        {
            //etrafta yürüyen (NavMeshi açýk) düþman
            case EnemyState.First:
                ReachTarget();
                break;

            //ýdle pozisyonunda dönme iþlemi yapan düþman
            case EnemyState.Second:
                EnemyIdleStart();
                Rotate();
                break;

            //sadece ýdle pozisyonu olan
            case EnemyState.Third:
                EnemyIdleStart();
                break;

            //birden fazla yerde navmesh yapan
            case EnemyState.Fourth:
                ReachTarget2();
                break;

        }
    }


    //for enemy collider
    private IEnumerator DestroyEnemy(GameObject clone)
    {
        yield return new WaitForSeconds(1f);
        clone.gameObject.GetComponent<SphereCollider>().radius = 0;
        clone.gameObject.GetComponent<SphereCollider>().isTrigger = false;
        for (int i = 0; i < 5; i++)
        {
          this.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (levelValue > GameManager.instance.MainCharacterLevel)
            {
                EnemyAttackStart();
                GameManager.instance.ManageCharactersLevel(EnemyState);
            }
            else if (levelValue < GameManager.instance.MainCharacterLevel)
            {
                GameManager.instance.attack = true;
            
                StartCoroutine(DestroyEnemy(this.gameObject));
                GameManager.instance.killedEnemyCount += 1;
            }
           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.attack = false;
        }

    }


}
