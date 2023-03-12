using Meta.WitAi.TTS.Utilities;
using Oculus.Voice.Demo;
using Speechly.SLUClient;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpellingPref : MonoBehaviour
{
    [SerializeField]
    public TTSSpeaker _speaker;

    public GameObject nextStep;
    public Text botText;
    public Text playerText;
    public Slider SliderEnergy;
    public Slider SliderBaselineEnergy;
    public AudioClip tryAgain;
    public AudioClip verygood;
    public AudioSource audioSource;
    public string[] commands = new string[3];



    private SpeechlyClient speechlyClient;



    // Start is called before the first frame update
    void Start()
    {
       
        Coroutine xcoroutine = StartCoroutine(InteractiveWithUser());

    }

    IEnumerator InteractiveWithUser()
    {
        string xst = "";
        for (int i = 0; i < commands.Length; i++)
        {
            string keyword = commands[i];
            speechlyClient = MicToSpeechly.Instance.SpeechlyClient;
          
            _speaker.Speak(keyword);

            botText.text =keyword;
            while (true)
            {

                SliderBaselineEnergy.value = speechlyClient.Output.NoiseLevelDb;
                SliderEnergy.value = speechlyClient.Output.NoiseLevelDb + speechlyClient.Output.SignalDb;


                

                speechlyClient.OnSegmentChange += (segment) =>
                {
                    Debug.Log("inS");
                    xst = segment.ToString((v) => "", (a, c) => "", "").Trim(' ').ToLower();
              
                    playerText.text = xst;
                };
                Debug.Log(xst);


                if (xst.ToLower() == keyword)
                {
                    audioSource.PlayOneShot(verygood);
                    yield return new WaitForSeconds(2.5f);
                    Debug.Log("onStop");
                    speechlyClient.Stop();
                    speechlyClient.Shutdown();
                    break;
                }

                yield return new WaitForSeconds(.5f);
            }

            botText.text = "you are brilliant but now i've another request";
            _speaker.Speak("you are brilliant but now i've another request");
            yield return new WaitForSeconds(6f);
            //    nextStep.SetActive(true);

        }



        // Update is called once per frame
        void Update()
        {

        }
    }
}
