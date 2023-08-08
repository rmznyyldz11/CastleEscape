using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerAnimator))]
public class PlayerController : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private MobileJoystick Joystick;
    private PlayerAnimator playerAnimator;
    private CharacterController characterController;
    [SerializeField] public float moveSpeed;
   

    private void Awake()
    {
      
        characterController = GetComponent<CharacterController>();
        playerAnimator = GetComponent<PlayerAnimator>();
      
    }


    void Update()
    {
        ManageMovement();
        CharacterSituation();
    }

    private void CharacterSituation()
    {
        if (GameManager.instance.die)
        {
            playerAnimator.PlayDieAnim();
            moveSpeed = 0;
        }
    }
   

    private void ManageMovement()
    {
        Vector3 moveVector = Joystick.GetMoveVector() * moveSpeed * Time.deltaTime / Screen.width;
        moveVector.z = moveVector.y;
        moveVector.y = 0;
        characterController.Move(moveVector);
        playerAnimator.ManageAnimations(moveVector);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FinishGate"))
        {
            GameManager.instance.GameTypeArrange(GameState.NextLevel);
            playerAnimator.PlayIdleAnim();
            moveSpeed = 0;
        }
        else if (other.CompareTag("Key"))
        {
            if(GameManager.instance.MainCharacterLevel == 1)
            {
                GameManager.instance.deleteText = true;
                GameManager.instance.MainCharacterLevel += 1;
                other.gameObject.SetActive(false);
                GameManager.instance.KeyImage.SetActive(true);
                foreach (var item in GameManager.instance.ObstaclesForKey)
                {
                    Destroy(item);
                }
            }
            else
            {
                GameManager.instance.deleteText = true;
                other.gameObject.SetActive(false);
                GameManager.instance.KeyImage.SetActive(true);
                foreach (var item in GameManager.instance.ObstaclesForKey2)
                {
                    Destroy(item);
                }
            }
          
        }
        else if (other.CompareTag("Book"))
        {
            other.gameObject.SetActive(false);
            GameManager.instance.MainCharacterLevel += 2;
            GameManager.instance.deleteText = true;
        }
      /*  else if (other.CompareTag("Enemy"))
        {
            EnemyController enemyControllers = other.gameObject.GetComponent<EnemyController>();
            if (GameManager.instance.MainCharacterLevel > enemyControllers.levels)
            {
                GameManager.instance.attack = true;
                FieldOfView fieldOfView = other.gameObject.GetComponent<FieldOfView>();
                fieldOfView.viewRadius = 0;
                fieldOfView.viewAngle = 0;
                StartCoroutine(DestroyEnemy(other.gameObject));
                for (int i = 0; i < 5; i++)
                {
                    enemyController[0].transform.GetChild(i).gameObject.SetActive(false);
                }
                GameManager.instance.killedEnemyCount += 1;
            }
            else if (GameManager.instance.MainCharacterLevel > enemyControllers.levels)
            {
                GameManager.instance.attack = true;
                FieldOfView fieldOfView = other.gameObject.GetComponent<FieldOfView>();
                fieldOfView.viewRadius = 0;
                fieldOfView.viewAngle = 0;
                StartCoroutine(DestroyEnemy(other.gameObject));
                for (int i = 0; i < 5; i++)
                {
                    other.gameObject.transform.GetChild(i).gameObject.SetActive(false);
                }
                GameManager.instance.killedEnemyCount += 1;
            }
        }*/
        else if (other.CompareTag("Hide"))
        {
            this.transform.GetChild(0).gameObject.SetActive(false);
            this.transform.GetChild(1).GetComponent<CapsuleCollider>().enabled = false;
          //  GameManager.instance.LevelOneCharacter.layer = default;
            moveSpeed = 55;
            GameManager.instance.deleteText = true;
        }

        else if (other.CompareTag("Lock"))
        {
            other.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            other.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            Animator anim = GameManager.instance.ObstaclesForLock.GetComponent<Animator>();
            anim.Play("ObstacleForLockAnim");
            anim.Play("Obs");
            GameManager.instance.deleteText = true;
        }
        else if (other.CompareTag("Lock2"))
        {
            other.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            other.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            other.gameObject.transform.GetChild(2).gameObject.SetActive(false);
            other.gameObject.transform.GetChild(3).gameObject.SetActive(false);
        }
    }
  
    private void OnTriggerExit(Collider other)
    {
           if (other.CompareTag("Hide"))
          {
            this.transform.GetChild(0).gameObject.SetActive(true);
            this.transform.GetChild(1).GetComponent<CapsuleCollider>().enabled = true;
            // GameManager.instance.LevelOneCharacter.layer = LayerMask.NameToLayer("Targets");
            moveSpeed = 30;
          }
    }  
}

/*   AllEnemy[0].transform.GetComponent<EnemyController>().EnemyAttackStart();
                  playerAnimator.PlayDieAnim();
                  moveSpeed = 0;
                  GameManager.instance.GameTypeArrange(GameState.GameOver);
                  GameManager.instance.die = true;*/