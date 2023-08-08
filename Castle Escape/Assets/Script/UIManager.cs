using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [Header("ComplateOrRestartGameUI")]
    [SerializeField] GameObject LevelComplateUI;
    [SerializeField] GameObject RestartLevelUI;

   


    private void Awake()
    {
        GameManager.GameType += GameText;
    }


    private void OnDestroy()
    {
        GameManager.GameType -= GameText;
    }


  
    public void GameText(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Normal:
                LevelComplateUI.SetActive(false);
                RestartLevelUI.SetActive(false);
                break;

            case GameState.NextLevel:
                LevelComplateUI.SetActive(true);
                break;

            case GameState.GameOver:
                RestartLevelUI.SetActive(true);
                break;
        }
    }


    //Restart Buton Ýçin
    public void RetryButton()
    {
        GameManager.instance.RetryGame();
    }


    public void NextLevelButton()
    {
        GameManager.instance.NextLevel();
    }

}
