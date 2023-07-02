
using System.Collections;
using System.Threading.Tasks;
using Meta.WitAi.TTS.Utilities;
using UnityEngine;
using UnityEngine.UI;
public class TalkerScript : MonoBehaviour
{
    [SerializeField] public TTSSpeaker _speaker;
    public GameObject _nextGameObject;
    public GameObject _text;
    public string[] text_array;
    private Task[] tasks = new Task[3];
    Coroutine xcoroutine;
    /// Start welcoming the game. This is called by Unity's StartCoroutine (...
    void Start()
    {
        xcoroutine = StartCoroutine(welcoming());
    }
    /// Coroutine to speak and read the text. This coroutine is used as a background task for the welcoming game.
    /// 
    /// 
    /// @return The coroutine to wait for the text to be spoken and read from the queue or null if the text is the last
    IEnumerator welcoming()
    {
        /// Yields a coroutine to read the text from the queue.
        for (int i = 0; i < tasks.Length; i++)
        {
            // speak the text while text is in the queue
            /// Speak the text of the task.
            if (i < tasks.Length)
            {
                _text.GetComponent<Text>().text = text_array[i];
                _speaker.Speak(text_array[i]);
            }
            // wait until the text is spoken
            /// Yields the next sample from the speaker.
            while (_speaker.IsSpeaking || _speaker.IsLoading)
            {
                yield return null;
            }
            // wait for a bit to give the user time to read the text
            yield return new WaitForSeconds(.5f);
            // end the coroutine if the text is the last one
            /// This method is called when the next task is finished.
            if (i >= tasks.Length - 1)
            {
                Debug.Log("end welcoming");
                _nextGameObject.GetComponent<SpellingValues>().SpellingAllValues();
                StopCoroutine(xcoroutine);
                break;
            }
        }
    }
}
