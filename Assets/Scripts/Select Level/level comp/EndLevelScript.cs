
using System.Collections;
using System.Collections.Generic;
using Meta.WitAi.TTS.Utilities;
using UnityEngine.UI;
using UnityEngine;

public class EndLevelScript : MonoBehaviour
{
    public TTSSpeaker _speaker;
    public GameRouter gameRouter;
    public AudioClip clip;
    public AudioSource audioSource;
    public Text text;
    public int levelNumber;
    public void GoToEnd()
    {
        audioSource.PlayOneShot(clip);
    var xRoutine = StartCoroutine(WaitAndSpeak());
    }
    private IEnumerator WaitAndSpeak()
    {
        text.text = "Congratulations! You have completed the level. new level unlocked";

        _speaker.Speak("Congratulations! You have completed the level. new level unlocked");
        while (_speaker.IsSpeaking || _speaker.IsLoading)
        {
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        gameRouter.SaveCurrentLevelAndBackToLevelPage(levelNumber);
    }
}
