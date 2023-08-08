using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject[] levels;
    public int levelIndex = 0;
    public static LevelManager instance;

    private void Awake()
    {
        instance = this;
        LoadData();
        SpawnLevel();
        GameManager.GameType += CreateLevels;
    }

    void Start()
    {
      
    }

   
    void Update()
    {
        
    }

    private void SpawnLevel()
    {
        if (levelIndex >= levels.Length)
            levelIndex = 0;

        GameObject levelInstance = Instantiate(levels[levelIndex]);
        StartCoroutine(EnableLevelCoroutine());

        IEnumerator EnableLevelCoroutine()
        {
            yield return new WaitForSeconds(0.05f);
            levelInstance.SetActive(true);

        }


    }


    public void CreateLevels(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.NextLevel:
                levelIndex++;
                SaveData();
                break;
          
        }
    }


    private void LoadData()
    {
        levelIndex = PlayerPrefs.GetInt("Level");
    }


    public void SaveData()
    {
        PlayerPrefs.SetInt("Level",levelIndex);
    }

}
