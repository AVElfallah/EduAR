using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Speechly;
using UnityEngine.UI;
using Speechly.SLUClient;
using Meta.Conduit;
using Unity.VisualScripting;

namespace Speechly.Example.NoiseGateTrigger
{
    public class UseSpeechly : MonoBehaviour
    {
        public Slider SliderEnergy;
        public Slider SliderBaselineEnergy;

        public Text ButtonText;
        public Text TranscriptText;
        private SpeechlyClient speechlyClient;

        void Start()
        {



            var xr = StartCoroutine(testIt());
        }
        IEnumerator testIt()
        {

            speechlyClient = MicToSpeechly.Instance.SpeechlyClient;
            string xst = "";
            while (true)
            {
                SliderBaselineEnergy.value = speechlyClient.Output.NoiseLevelDb;
                SliderEnergy.value = speechlyClient.Output.NoiseLevelDb + speechlyClient.Output.SignalDb;


                speechlyClient.OnSegmentChange += (segment) =>
                {
                    xst = segment.ToString((v) => "", (a, c) => "", "").Trim(' ').ToLower();




                    TranscriptText.text = xst;
                };
                Debug.Log(xst);
                if (xst.ToLower() == "stop")
                {
                    Debug.Log("onStop");
                    speechlyClient.Stop();
                    speechlyClient.Shutdown();
                    break;
                }
                yield return new WaitForSeconds(.5f);
            }
            Debug.Log("googleit");
            yield return null;

        }

        void Update()
        {



        }

    }
}
