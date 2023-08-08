using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplateController : MonoBehaviour
{
   public void LevelComplates()
    {
        if(PlayerPrefs.GetInt("Level") <= SceneManager.GetActiveScene().buildIndex)
        {
            LevelManager.instance.SaveData();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }



}
