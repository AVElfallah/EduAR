using System.Collections;
using System.Collections.Generic;
using Meta.WitAi.TTS.Utilities;
using UnityEngine.UI;
using UnityEngine;

public class TestLevelScript : MonoBehaviour
{
    [SerializeField] public TTSSpeaker _speaker;
    public const string CONSTANT_NAME = "Welcome little fellow.\nIn the game.\nSelect level to start entertain.";
    
    // Start is called before the first frame update
    void Start()
    {
            _speaker.Speak(CONSTANT_NAME);
    }

  private void SpeakClick() => _speaker.Speak(CONSTANT_NAME);
    // Update is called once per frame
    void Update()
    {
        
    }
}
