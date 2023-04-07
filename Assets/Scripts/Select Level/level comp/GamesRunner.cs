using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamesRunner : MonoBehaviour
{
    private int _currentGame = -1;
    public FindHiddenObjects[] games;

    public void RunTheNewGame()
    {
        _currentGame++;
        if (_currentGame < games.Length)
        {
            StartCoroutine(games[_currentGame].PlayAndLearn());
        }
    }
}
