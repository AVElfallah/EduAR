using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Meta.WitAi.TTS.Utilities;
using Speechly.SLUClient;
using UnityEngine;
using UnityEngine.UI;

public class UsingTextSpeech : MonoBehaviour
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

    public void StartSpilling(string[] words, GamesRunner gamesRunner) => StartCoroutine(_StartSpilling(words, gamesRunner));
    public IEnumerator _StartSpilling(string[] words, GamesRunner gamesRunner)
    {
        audioSource = this.GetComponentInChildren<AudioSource>();
        var speechlyGameObject = MicToSpeechly.Instance;
        speechlyClient = MicToSpeechly.Instance.SpeechlyClient;
        speechlyGameObject.SetActive(false);

        for (int i = 0; i < words.Length; i++)
        {
            string currentWord = words[i].ToLower();
            BotText.text = currentWord;
            _speaker.Speak(currentWord);
            //wait for the speaker to finish speaking
            Debug.Log("Waiting for speaker to finish speaking");
            while (_speaker.IsSpeaking || _speaker.IsLoading)
            {


                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(.3f);
            //chenge [isRunning] to true to start listening for the segment

            //initialize the filtered text
            string filteredText = "";
            // check for the segment coming from the user
            #region SegmentChange
            //initialize the speechly client




            isRunning = true;
            speechlyGameObject.SetActive(true);
            //if(!speechlyClient.IsReady)yield return speechlyClient.Start();
            //start speechly segment
            // speechlyClient.OnTranscriptChange += (transcript) => TranscriptChangeSe(transcript,ref filteredText);
            Speechly.Types.SegmentChangeDelegate segmentionChange = (segment) => SegmentChangeSe(
                segment, ref filteredText
                , ref currentWord
                , ref i
                , ref words);
            speechlyClient.OnSegmentChange += segmentionChange;
            speechlyClient.AdjustAudioProcessor();
            #endregion
            // to make the application wait for user spilling and didn't go to the next word
            //this condition is used to check if the filtered text
            // is equal to the current word or not
            // and we enhcane the condition to check if the filtered text contains the current word
            // this is used to check if the user spilling the word correctly or not
            // for example if the current word is "very good" and the user spilling "very good job"
            // the condition will be true
            // and the bot will say "very good"
            Debug.Log("Waiting for user to spill the word");
            while (!(filteredText == currentWord || filteredText.Contains(currentWord)))
            {

                yield return waitForSomeTime(0.1f);
            }


            speechlyClient.OnSegmentChange -= segmentionChange;
            speechlyClient.Update();

            Debug.Log("out of " + speechlyClient.IsActive);
            yield return waitForSomeTime(1f);
            //Debug.Log("out of while loop = " + x);
            isRunning = false;
            audioSource.PlayOneShot(verygodClip);
            Debug.LogWarning("Playing audio");
            //to make application wait for the audio to finish playing
            while (audioSource.isPlaying)
            {
                yield return waitForSomeTime(1f);
            }
            ChildText.text = "- - -";
            speechlyGameObject.SetActive(false);
        }
        Debug.Log("out of for loop ");
        ChildText.text = "- - -";
        //run hidin object script and show the counter

        isRunning = false;


        gamesRunner.RunTheNewGame();

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

    private void SegmentChangeSe(Segment segment, ref string filteredText, ref string currentWord, ref int i, ref string[] words)
    {
        try
        {
            //filter the text coming from the user

            if (words.Length > 0 && i < words.Length && segment.isFinal)
            {
                filteredText = segment.ToString(
                (v) => ""
                , (a, c) => ""
                , "").Trim(' '
                ).ToLower();
                //check if the filtered text is equal to the current word
                // if it is not equal, then display the text as listening on the bot's text
                // to let user know that the bot is listening
                bool isEquals = filteredText == currentWord;
                bool isContains = filteredText.Contains(currentWord);
                if ((isEquals) || (isContains))
                {
                    // display the filtered text on the child's text
                    ChildText.text = currentWord;
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
    }
    private void OnDestroy()
    {
        if (speechlyClient != null)
        {
            speechlyClient.Stop();
            speechlyClient.StopStream();
            speechlyClient = null;
        }
        StopAllCoroutines();
    }
}

