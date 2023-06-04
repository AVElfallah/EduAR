using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using Meta.WitAi.TTS.Utilities;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameRouter : MonoBehaviour
{
    private void Start()
    {
        Application.runInBackground = true;
        
    }

    public void RestartCurrentLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoSettingsPage()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    [SerializeField]
    public void SaveCurrentLevelAndBackToLevelPage(int levelNumber)
    {
        int oldLevelNumber = PlayerPrefs.GetInt("c_level");
        if (oldLevelNumber == levelNumber)
        {
            PlayerPrefs.SetInt("c_level", levelNumber + 1);
            PlayerPrefs.Save();
        }
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }

    public void GoToCustomLevel(string levelName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(levelName);
    }

    public void GoToMainPage()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void GoToSelectLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }
}
