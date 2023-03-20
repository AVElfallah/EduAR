using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Meta.WitAi.TTS.Utilities;
using Speechly.SLUClient;

public class TestUsingText : MonoBehaviour
{
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

    public void StartSpilling(string[] words, GameObject counterText) => StartCoroutine(_StartSpilling(words, counterText));
    public IEnumerator _StartSpilling(string[] words, GameObject counterText)
    {
        audioSource = this.GetComponentInChildren<AudioSource>();
        for (int i = 0; i < words.Length; i++)
        {
            string currentWord = words[i];
            BotText.text = currentWord;
            _speaker.Speak(currentWord);
            //wait for the speaker to finish speaking
            while (_speaker.IsSpeaking || _speaker.IsLoading)
            {
                Debug.Log("Waiting for speaker to finish speaking");

                yield return waitForSomeTime(3f);
            }
            //initialize the speechly client
            speechlyClient = MicToSpeechly.Instance.SpeechlyClient;
            Debug.Log("start listening");
            //chenge [isRunning] to true to start listening for the segment
            isRunning = true;
            //initialize the filtered text
            string filteredText = "";
            // check for the segment coming from the user
            #region SegmentChange
            speechlyClient.OnSegmentChange += (segment) =>
            {
                try
                {
                    //filter the text coming from the user
                    filteredText = segment.ToString((v) => "", (a, c) => "", "").Trim(' ').ToLower();

                    if (words.Length > 0 && i < words.Length)
                    {
                        //check if the filtered text is equal to the current word
                        // if it is not equal, then display the text as listening on the bot's text
                        // to let user know that the bot is listening

                        if (filteredText == currentWord.ToLower())
                        {
                            // display the filtered text on the child's text
                            ChildText.text = filteredText;
                            // stop listening for the segment
                        }
                        else
                        {
                            // if it is not equal, then display the text as listening on the bot's text
                            ChildText.text = "Lestening...";
                        }
                    }
                    // this line is used to display the current word on the console 
                    // to check if the current word is equal to the filtered text
                    Debug.Log("Current word: " + filteredText);
                }
                finally { }
            };
            #endregion
            // to make the application wait for user spilling and didn't go to the next word


            while (!(filteredText.ToLower() == currentWord.ToLower()))
            {

                yield return waitForSomeTime(0.3f);
            }
            Debug.Log("out of waiting");
            // speechlyClient.Shutdown().GetAwaiter().GetResult();
            speechlyClient.StopStream();
            speechlyClient = null;
            isRunning = false;
            audioSource.PlayOneShot(verygodClip);
            //to make application wait for the audio to finish playing
            while (audioSource.isPlaying)
            {
                yield return waitForSomeTime(1f);
            }
        }
        Debug.Log("out of for loop");
        ChildText.text = "- - -";
        //run hidin object script and show the counter
        counterText.SetActive(true);
        isRunning = false;
        //        speechlyClient.Stop();
        //        speechlyClient.StopStream();
        // yield return speechlyClient.Shutdown().GetAwaiter();
        speechlyClient = null;
        yield return null;
    }



    IEnumerator waitForSomeTime(float timeout)
    {
        yield return new WaitForSeconds(timeout);
    }

    private void Update()
    {
        if (isRunning)
        {
            try
            {

                SliderBaselineEnergy.value = speechlyClient.Output.NoiseLevelDb;
                SliderEnergy.value = speechlyClient.Output.NoiseLevelDb + speechlyClient.Output.SignalDb;
            }
            finally
            {

            }
        }
    }
}

