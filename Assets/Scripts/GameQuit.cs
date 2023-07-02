using UnityEngine;

public class GameQuit : MonoBehaviour
{
    public void CloseTheGame()
    {
        Debug.Log("Game is closing");
        Application.Quit();
    }
}
