using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSoundTrackController : MonoBehaviour
{
    public AudioSource audio_src;
    // Start is called before the first frame update
    void Start()
    {
      bool  isSoundOn = PlayerPrefs.GetInt("game_sound") == 1 ? true : false;
        if (isSoundOn) {
            audio_src.Play();
        }
        else
        {
            audio_src.Pause();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
