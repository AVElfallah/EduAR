using Speechly.Example.NoiseGateTrigger;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellingValues : MonoBehaviour
{
    [SerializeField]
    public GameObject counter;
    public string[] words;
    public UseTextToSpeech speech;
    void Start()
    {

    }

    public void SpellingAllValues() => speech.StartSpelling(words, counter);
    // Update is called once per frame
    void Update()
    {

    }
}
