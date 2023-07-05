
using System.Collections;
using System.Collections.Generic;
using Meta.WitAi.TTS.Utilities;
using UnityEngine;
using UnityEngine.UI;

public class EndLevelScript : MonoBehaviour
{
    public TTSSpeaker _speaker;
    public int levelNumber;
    /// Play the clip and wait for it to end. This is useful for things like sounding a game
    public void GoToEnd()
    {
        audioSource.PlayOneShot(clip);
        var xRoutine = StartCoroutine(WaitAndSpeak());
    }
    public GameRouter gameRouter;
    public AudioClip clip;
    public AudioSource audioSource;
    public Text text;
    /// Waits and spawns the speaker. 
    /// This is a coroutine so we don't have to worry about looping in the coroutine.
    /// @return The coroutine that will wait and spawns the speaker.
    ///  This coroutine will return when the speak is done
    private IEnumerator WaitAndSpeak()
    {
        text.text = "Congratulations! You have completed the level. new level unlocked";
        _speaker.Speak("Congratulations! You have completed the level. new level unlocked");
        /// Yields the next sample from the speaker.
        while (_speaker.IsSpeaking || _speaker.IsLoading)
        {
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        gameRouter.SaveCurrentLevelAndBackToLevelPage(levelNumber);
    }
}
