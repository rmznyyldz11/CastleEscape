using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using DG.Tweening;

public enum GameState { Normal,NextLevel,GameOver}


public class GameManager : MonoBehaviour
{
    private GameState gameState;
    public static Action<GameState> GameType;
    public static GameManager instance;
 
    [Header("Levels")]
    [SerializeField] public int MainCharacterLevel;
    public int killedEnemyCount;
    public bool Fight,die,attack,hide,deleteText = false;
    [SerializeField] public GameObject LevelOneCharacter, LevelThreeOrFiveCharacter, LevelNineCharacter;
 

    [Header("KeyAndBook")]
    [SerializeField] public List<GameObject> ObstaclesForKey = new List<GameObject>();
    [SerializeField] public List<GameObject> ObstaclesForKey2 = new List<GameObject>();
    [SerializeField] public List<GameObject> ObstaclesForLevelEnemy = new List<GameObject>();
    [SerializeField] public GameObject ObstaclesForLock;
    [SerializeField] private GameObject Key;
    [SerializeField] private GameObject Key2;
    [SerializeField] public GameObject KeyImage;
  //  [SerializeField] private GameObject Book;
 


    void Start()
    {
        instance = this;
        GameTypeArrange(GameState.Normal);
        AboutKey();
    }

 
    void Update()
    {
        CharacterTypeToLevel();
        ObstacleForLevelEnemy();

    }

    public void ManageCharactersLevel(EnemyState enemyState)
    {
        if (enemyState == EnemyState.First)
        {
            GameTypeArrange(GameState.GameOver);
            die = true; 
        }
        else if (enemyState == EnemyState.Second)
        {
            GameTypeArrange(GameState.GameOver);
            die = true;
        }
        else if (enemyState == EnemyState.Third)
        {
            GameTypeArrange(GameState.GameOver);
            die = true;
        }
        else if (enemyState == EnemyState.Fourth)
        {
            GameTypeArrange(GameState.GameOver);
            die = true;
        }

    }


    private void AboutKey()
    {
        if (Key.activeInHierarchy)
        {
            foreach (var item in ObstaclesForKey)
            {
                item.gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (var item in ObstaclesForKey)
            {
                item.gameObject.SetActive(false);
            }
        }
        if (Key2.activeInHierarchy)
        {
            foreach (var item in ObstaclesForKey2)
            {
               item.gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (var item in ObstaclesForKey2)
            {
               item.gameObject.SetActive(false);
            }
        }
        
    }

    //öldürülne düþman sayýsýna göre kapýda ki engel açýlacak
    private void ObstacleForLevelEnemy()
    {
        if(killedEnemyCount == 1)
        {
            for (int i = 0; i < 2; i++)
            {
                ObstaclesForLevelEnemy[i].gameObject.SetActive(false);
            }
        }
        else if (killedEnemyCount == 2)
        {
            ObstaclesForLevelEnemy[2].gameObject.SetActive(false);
        }
        
    }


    private void CharacterTypeToLevel()
    {
        if (MainCharacterLevel == 1)
        {
            LevelOneCharacter.gameObject.SetActive(true);
            LevelThreeOrFiveCharacter.gameObject.SetActive(false);
            LevelNineCharacter.gameObject.SetActive(false);
        }
        else if (MainCharacterLevel == 3 || MainCharacterLevel == 5 || MainCharacterLevel == 6)
        {
            LevelOneCharacter.gameObject.SetActive(false);
            LevelThreeOrFiveCharacter.gameObject.SetActive(true);
            LevelThreeOrFiveCharacter.transform.position = LevelOneCharacter.transform.position;
        }
        else if (MainCharacterLevel == 7 || MainCharacterLevel == 9 || MainCharacterLevel == 10)
        {
            LevelOneCharacter.gameObject.SetActive(false);
            LevelThreeOrFiveCharacter.gameObject.SetActive(false);
            LevelNineCharacter.gameObject.SetActive(true);
            LevelNineCharacter.transform.position = LevelThreeOrFiveCharacter.transform.position;
        }

    }


    //karakter kapýdan çýktýðunda(collidera girdiðinde)
    //LevelComplate canvasu çýkcak ve ekrandaki diðer yazýlar kaybolcak
    //yani birden fazla iþlem yapýlcak
    public void GameTypeArrange(GameState gameState)
    {
        GameType?.Invoke(gameState);
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(1);
    }

}
