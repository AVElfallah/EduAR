using UnityEngine;

public class GamesRunner : MonoBehaviour
{
    private int _currentGame = -1;
    public FindHiddenObjects[] games;
    /// Runs the new game. If there are no games left in the game list this will do nothing.
    ///  Otherwise it will loop through the games and learn
    public void RunTheNewGame()
    {
        _currentGame++;
        /// Play and learn the game.
        if (_currentGame < games.Length)
        {
            StartCoroutine(games[_currentGame].PlayAndLearn());
        }
    }
}
