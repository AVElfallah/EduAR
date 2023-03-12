using Meta.WitAi.TTS.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class SpellingPref : MonoBehaviour
{
    [SerializeField]
  public  TTSSpeaker _speaker;
  public  Text childText;
  public  Text botText;
  public  AudioClip tryAgain;
  public  AudioClip verygood;
  public  AudioSource audioSource;
    public string[] commands= new string[3];


    private int counter=0;

    private ConfidenceLevel confidence = ConfidenceLevel.Medium;
    private PhraseRecognizer recognizer;
    // Start is called before the first frame update
    void Start()
    {
        Coroutine xcoroutine = StartCoroutine(InteractiveWithUser());
       
    }
    string word = "";
    bool isSpellingEnd = false;
    IEnumerator InteractiveWithUser()
    {
        for (int i = 0; i < commands.Length; i++)
        {
           isSpellingEnd = false;
             startSprlling:
            counter = i;
           botText.text = commands[i];
            _speaker.Speak(commands[i]);
       

            string[] ar = { commands[i].ToUpper() };
            if(recognizer != null)
            {
              
            }
            {
                recognizer = new KeywordRecognizer(ar, ConfidenceLevel.Low);
            }
          
            recognizer.OnPhraseRecognized += listenToChild;
            recognizer.Start();
            yield return new WaitForSeconds(10f);

            if (!isSpellingEnd)
            {
                goto startSprlling;
            }

            yield return new WaitForSeconds(2f);
            if (i == commands.Length - 1)
            {
               
                // Instantiate(_nextGameObject).SetActive(true);
            }
            /*  */
        }
    }
    void listenToChild(PhraseRecognizedEventArgs args)
    {
        word = args.text;
        childText.text = word;
        if (word.ToLower() == commands[counter].ToLower())
        {
            Debug.Log("done!");
            Debug.Log(args.text);
            recognizer.Stop();
            
            audioSource.PlayOneShot(verygood);
            isSpellingEnd=true;
            recognizer=null;    
           

        }
        else
        {
            Debug.Log("error!!!!");
            Debug.Log(args.text);
            audioSource.PlayOneShot(tryAgain);
        }
       
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
