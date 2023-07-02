
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRouter : MonoBehaviour
{
    /// Starts the application. This is called by Unity when the application is started but not on the main
    private void Start()
    {
        Application.runInBackground = true;

    }

    /// Restarts the current level. This is useful when you want to change the level to a different one
    public void RestartCurrentLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    /// Loads the settings page and sets time scale to 1f. This is a hack to work around the bug in Game
    public void GoSettingsPage()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
    /// Saves the current level and back to the level page. This is used to prevent accidental changes in level settings
    /// 
    /// @param levelNumber - The current level number
    [SerializeField]
    public void SaveCurrentLevelAndBackToLevelPage(int levelNumber)
    {
        int oldLevelNumber = PlayerPrefs.GetInt("c_level");
        /// Set the level number to the new level number.
        if (oldLevelNumber == levelNumber)
        {
            PlayerPrefs.SetInt("c_level", levelNumber + 1);
            PlayerPrefs.Save();
        }
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }

    /// Go to a custom level. This is useful for loading scenes that don't have a level name
    /// 
    /// @param levelName - The name of the
    public void GoToCustomLevel(string levelName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(levelName);
    }
    /// Loads the main page and sets time scale to 1. This is called when the user clicks the main
    public void GoToMainPage()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    /// Loads the level and sets timeScale to 1. Used for testing purposes to see if we need to go to a level
    public void GoToSelectLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }
}
