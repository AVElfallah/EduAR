

using Meta.WitAi.TTS.Utilities;
using Speechly.Example.NoiseGateTrigger;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Android;

using UnityEngine.UI;

public class TalkerScript : MonoBehaviour
{
    [SerializeField] public TTSSpeaker _speaker;
    public GameObject _nextGameObject;
    public GameObject _text;
    public string[] text_array;
    private Task[] tasks = new Task[3];
    // Start is called before the first frame update


    Coroutine xcoroutine;


    void Start()
    {

        xcoroutine = StartCoroutine(welcoming());


    }

    IEnumerator welcoming()
    {
        for (int i = 0; i < tasks.Length; i++)
        {
            // speak the text while text is in the queue
            if(i<tasks.Length){
            _text.GetComponent<Text>().text = text_array[i];
            _speaker.Speak(text_array[i]);
            }
            // wait until the text is spoken
            while (_speaker.IsSpeaking || _speaker.IsLoading)
            {
                yield return null;
            }
            // wait for a bit to give the user time to read the text
            yield return new WaitForSeconds(.5f);
            // end the coroutine if the text is the last one
            if (i>= tasks.Length-1 )
            {
                Debug.Log("end welcoming");
                _nextGameObject.GetComponent<SpellingValues>().SpellingAllValues();
                StopCoroutine(xcoroutine);

                break;
            }

        }

    }


/// <summary>
/// This function is called when the MonoBehaviour will be destroyed.
/// </summary>
void OnDestroy()
{
   //StopAllCoroutines();
}

    
}
