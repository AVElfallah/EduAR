using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameRouter: MonoBehaviour
{
    
    public void GoSettingsPage()
    {
        SceneManager.LoadScene(1);

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
