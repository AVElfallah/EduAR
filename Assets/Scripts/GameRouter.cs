using System.Collections;
using System.Collections.Generic;
using Meta.WitAi.TTS.Utilities;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameRouter: MonoBehaviour
{
    
    public void GoSettingsPage()
    {
        SceneManager.LoadScene(1);

    }
  /*  */
  [SerializeField]
    public void SaveCurrentLevelAndBackToLevelPage(int levelNumber){
        

        int oldLevelNumber= PlayerPrefs.GetInt("c_level");
        if (oldLevelNumber ==levelNumber)
        {
            PlayerPrefs.SetInt("c_level", levelNumber+1);
            PlayerPrefs.Save();
        }
        SceneManager.LoadScene(2);
  
    }

    public void GoToCustomLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
    public void GoToMainPage() {

        SceneManager.LoadScene(0);
    }
    public void GoToSelectLevel()
    {
        SceneManager.LoadScene(2);  
    }

}
