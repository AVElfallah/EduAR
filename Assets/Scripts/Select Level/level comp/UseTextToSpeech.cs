using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Speechly;
using UnityEngine.UI;
using Speechly.SLUClient;
using Meta.Conduit;
using Unity.VisualScripting;
using Meta.WitAi.TTS.Utilities;
using System.Linq;
using System;

namespace Speechly.Example.NoiseGateTrigger
{
    public class UseTextToSpeech : MonoBehaviour
    {
        /// The SliderEnergy is used to set the energy of the slider. 
        public Slider SliderEnergy;
        /// Creates and initializes a base sliders wrapper. This is used to set the baseline
        public Slider SliderBaselineEnergy;

        /// Gets the bot text. This is used to display the bot's text
        public Text BotText;
        /// Gets the child text. This is used to display the child's text
        public Text ChildText;


        /// Called when the speaker is added to the game. This is where you can set the speakers
        public TTSSpeaker _speaker;
        /// Creates and returns a verygod clip. This is used to play the verygod clip
        public AudioClip verygodClip;

        private SpeechlyClient speechlyClient;
        private AudioSource audioSource;
        bool isRunning = false;

        void Start()
        {

        }
        /// Starts spelling the specified words. This is a coroutine and can be called multiple times
        /// 
        /// @param words - The words to spell.
        /// @param gameObject - The gameobject that will set active at the end of the loop
        public IEnumerator StartSpelling(string[] words)
        {
            isRunning = true;
            audioSource = this.GetComponentInChildren<AudioSource>();
            //     animateACustomLetter.animateALetter("A");
            var xr = StartCoroutine(speechlyGetter(words));
            yield return null;
        }
        /// Gets the speech ly. This is used to listen for changes to the MicToSpeechly.
        /// 
        /// @param words - The words that are to be speaked
        /// @param gameObject - The game object that will be set active at the end of the loop
        IEnumerator speechlyGetter(string[] words)
        {
            /// Returns the number of words in the log file.
            /// This is a generator that runs the speechly client.
            for (int i = 0; i < words.Length; i++)
            {
                var cword = words[i];
                var lisWords = words.ToList().GetRange(0, i);

                ChildText.text = "- - -";

                //    animateACustomLetter.animateALetter(lisWords.Contains(cword) ? cword + "_" : cword);
                BotText.text = cword;
                _speaker.Speak(cword);

                yield return new WaitForSeconds(3f);
                //    animateACustomLetter.animateALetter(lisWords.Contains(cword) ? cword + "_" : cword);
                speechlyClient = MicToSpeechly.Instance.SpeechlyClient;
                string xst = "";
                speechlyClient.OnSegmentChange += isRunning ? (segment) =>
                {
                    try
                    {
                        xst = segment.ToString((v) => "", (a, c) => "", "").Trim(' ').ToLower();
                        /// Set child text to listen..
                        if (words.Length > 0 && i < words.Length)
                        {
                            // make the mic listen to the word and detect only the specified word
                            ChildText.text = xst.ToLower() == cword.ToLower() ? xst : "Listening..";
                        }
                        // print the word that is being detected in the log..
                        Debug.Log("onChange: " + xst);
                    }
                    finally
                    {

                    }

                }
                :
                // close the mic by the end of the level by setting the speechly client to null
                null;



                /// Yields a block of time until the word is in the words array.
                while (true)
                {





                    /// Stop stream and play one shot
                    if (xst.ToLower() == cword.ToLower())
                    {
                        // speechlyClient.StopStream();
                        speechlyClient = null;
                        audioSource.PlayOneShot(verygodClip);

                        yield return new WaitForSeconds(2f);

                        break;
                    }
                    yield return new WaitForSeconds(.3f);

                }


                speechlyClient = null;
                yield return new WaitForSeconds(2.5f);

            }
            Debug.Log("onExit");


            ChildText.text = "- - -";

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
                    Debug.Log(e);
                }
            }
        }

    }
}
