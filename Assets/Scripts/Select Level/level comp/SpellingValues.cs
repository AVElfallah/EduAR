using Speechly.Example.NoiseGateTrigger;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellingValues : MonoBehaviour
{
    [SerializeField]
    public GameObject counter;
    public string[] words;
   // public UseTextToSpeech speech;
    public TestUsingText speech;

    void Start()
    {

    }

    public void SpellingAllValues()
    {
        speech.StartSpilling(words, counter);

    }

    private IEnumerator nextStep()
    {
        yield return null;
        counter.SetActive(true);

    }
    // Update is called once per frame
    void Update()
    {

    }
}
