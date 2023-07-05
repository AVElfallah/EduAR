using UnityEngine;

public class SpellingValues : MonoBehaviour
{
    [SerializeField]
    public GamesRunner gamesRunner;
    public string[] words;
    // public UseTextToSpeech speech;
    public UsingTextSpeech speech;
    /// Spells all values in the text. This is called when the user presses the spell button
    public void SpellingAllValues()
    {
        speech.StartSpilling(words, gamesRunner);
    }
}
