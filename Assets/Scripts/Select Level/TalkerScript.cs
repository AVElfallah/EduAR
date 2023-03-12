
using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using Meta.WitAi.TTS.Utilities;
using UnityEngine.UI;
using UnityEngine;
using Meta.WitAi.TTS.Data;


using System;
using System.Threading.Tasks;
using UnityEngine.Windows.Speech;
using UnityEditor;

public class TalkerScript : MonoBehaviour
{
    [SerializeField] public TTSSpeaker _speaker;
    
    public GameObject _text;
    public GameObject _nextGameObject;
/*    public Text _testChild;*/
    public string[] text_array;
    private Task[] tasks =new Task[3]; 
 

    // Start is called before the first frame update

    Coroutine xcoroutine;
    private PhraseRecognizer recognizer;

    void Start()
    {

       
         xcoroutine = StartCoroutine(te());
      
      
 

        
    }

/*    private string word = "";
     int iC = 0;*/
   IEnumerator  te()
    {
        for (int i=0 ; i < tasks.Length; i++)
        {
          /*  iC = i;*/
            _text.GetComponent<Text>().text = text_array[i];
            _speaker.Speak(text_array[i]);
            yield return new WaitForSeconds(2f);

            yield return new WaitForSeconds(2f);
            if (i==tasks.Length-1)
            {
                _nextGameObject.SetActive(true);
               // Instantiate(_nextGameObject).SetActive(true);
            }
         /*   string[] ar = { text_array[i] };
            recognizer = new KeywordRecognizer(ar, ConfidenceLevel.Medium);
            recognizer.OnPhraseRecognized += userSpeak;
            recognizer.Start();
            yield return new WaitForSeconds(10f);*/
        }

    }

/*    void userSpeak(PhraseRecognizedEventArgs args)
    {
        word = args.text;
        _text.GetComponent<Text>().text=word;
        if (word == text_array[iC])
        {
            Debug.Log("done in sc");
            
        }
        else
        {
            Debug.Log("fuck");
        }
        _testChild.text = word;
    }*/
  
    // Update is called once per frame
    void Update()
    {

    }

    private void OnDisable()
    {
        
            if (xcoroutine != null)
            {
                StopCoroutine(xcoroutine);
            }
        
    }
}
