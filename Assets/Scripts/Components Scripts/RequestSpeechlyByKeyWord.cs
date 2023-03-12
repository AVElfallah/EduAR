using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Speechly.SLUClient;
using UnityEngine;
using System.Reflection.Emit;
using UnityEditor;

public class RequestSpeechlyByKeyWord : MonoBehaviour
{
    //   public Slider SliderEnergy;
    //   public Slider SliderBaselineEnergy;
    private SpeechlyClient speechlyClient;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");
        var x = SliceWord("hello").GetAwaiter().GetResult();
        Debug.Log("back: " + x);
    }
    public async Task<bool> SliceWord(string word)
    { 
        string xStr = "";
        speechlyClient = MicToSpeechly.Instance.SpeechlyClient;
        speechlyClient.OnSegmentChange += async (segment) =>
        {
            xStr = segment.ToString((v) => "", (a, c) => "", "").Trim(' ').ToLower();
            Debug.Log("xStr: in change:" + xStr);
            if (xStr.ToLower() == word.ToLower())
            {
                Debug.Log("xStr: " + xStr);
                Debug.Log("word: " + word);
                await speechlyClient.Shutdown();
                speechlyClient = null;
                //   speechlyClient=null;
            }
        };
       
 
      
    

        return xStr.ToLower() == word.ToLower();
    }

    void Update()
    {

    }
}
