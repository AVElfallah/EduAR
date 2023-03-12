using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Speechly;
using UnityEngine.UI;
using Speechly.SLUClient;
using Meta.Conduit;
using Unity.VisualScripting;
using Meta.WitAi.TTS.Utilities;
using System;

namespace Speechly.Example.NoiseGateTrigger
{
    public class UseTextToSpeech : MonoBehaviour
    {
        public Slider SliderEnergy;
        public Slider SliderBaselineEnergy;

        public Text BotText;
        public Text ChildText;

     //   public string[] words = new string[3];

        public TTSSpeaker _speaker;
        public AudioClip verygodClip;
     //   public GameObject nextStep;

        private SpeechlyClient speechlyClient;
        private AudioSource audioSource;
        bool isRunning=false;

        void Start()
        {

        }
        public void StartSpelling(string[] words,GameObject gameObject)
        {
            isRunning = true;
            audioSource = this.GetComponentInChildren<AudioSource>();


            var xr = StartCoroutine(speechlyGetter(words,gameObject));
        }
        IEnumerator speechlyGetter(string[] words, GameObject gameObject)
        {
            for (int i = 0; i < words.Length; i++)
            {
                ChildText.text = "- - -";
                BotText.text = words[i];
                _speaker.Speak(words[i]);
                yield return new WaitForSeconds(3f);
                speechlyClient = MicToSpeechly.Instance.SpeechlyClient;
                string xst = "";
                speechlyClient.OnSegmentChange +=isRunning? (segment) =>
                {
                    try
                    {
                        xst = segment.ToString((v) => "", (a, c) => "", "").Trim(' ').ToLower();
                        if (words.Length > 0 && i < words.Length)
                        {
                            ChildText.text = xst.ToLower() == words[i].ToLower() ? xst : "Listening..";
                        }
                      
                        Debug.Log("onChange: " + xst);
                    }
                    finally
                    {
            
                    }

                }:null;



                while (true)
                {





                    if (xst.ToLower() == words[i].ToLower())
                    {
                        // speechlyClient.StopStream();


                        audioSource.PlayOneShot(verygodClip);

                        yield return new WaitForSeconds(2f);
                       

                        break;
                    }
                    yield return new WaitForSeconds(.3f);

                }

                
              //  speechlyClient.Shutdown().GetAwaiter().GetResult();
                speechlyClient = null;
                yield return new WaitForSeconds(2.5f);

            }
            Debug.Log("onExit");


            ChildText.text = "- - -";
            gameObject.SetActive(true);
            isRunning = false;
            yield return null;

        }

        void Update()
        {
            if (isRunning)
            {
                try
                {

                    SliderBaselineEnergy.value = speechlyClient.Output.NoiseLevelDb;
                    SliderEnergy.value = speechlyClient.Output.NoiseLevelDb + speechlyClient.Output.SignalDb;
                }
                catch (Exception e)
                {

                }
            }
        }

    }
}
