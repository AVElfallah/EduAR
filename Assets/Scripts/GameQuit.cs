using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameQuit : MonoBehaviour
{

    /// Closes the game and quits the game. Does not return until the game is closed or a timeout

    public void CloseTheGame()
    {
        Debug.Log("Game is closing");
        Application.Quit();
    }
}
