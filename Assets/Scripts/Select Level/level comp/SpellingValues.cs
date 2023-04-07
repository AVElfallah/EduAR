using Speechly.Example.NoiseGateTrigger;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellingValues : MonoBehaviour
{
    [SerializeField]
    public GamesRunner gamesRunner;
    public string[] words;
   // public UseTextToSpeech speech;
    public UsingTextSpeech speech;

  

    public void SpellingAllValues()
    {
        speech.StartSpilling(words, gamesRunner);

    }

    
    
}
