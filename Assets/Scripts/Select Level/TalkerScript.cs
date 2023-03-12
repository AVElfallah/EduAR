

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

            _text.GetComponent<Text>().text = text_array[i];
            _speaker.Speak(text_array[i]);
            yield return new WaitForSeconds(1.25f);

            yield return new WaitForSeconds(1.55f);
            if (i == tasks.Length - 1)
            {
                Debug.Log("end welcoming");
                _nextGameObject.GetComponent<SpellingValues>().SpellingAllValues();

            //    _nextGameObject.GetComponent<UseTextToSpeech>().StartSpelling();

                break;
            }

        }

    }



    void Update()
    {

    }

    private void OnDisable()
    {

      
    }
}
