using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSettings : MonoBehaviour
{
    //to change toggel state depend on settings was loaded from player prefs
    // we use [toggel_button] to do this event
    public toggel_button toggel;
    private bool isSoundOn;

    // Start is called before the first frame update
    void Start()
    {
       
        try
        {
            checkAndSetSound();
            var tof = toggel.gameObject.GetComponent<toggel_button>();
            tof.ClickWithValue(isSoundOn);

        }
        catch (Exception e )
        {
            Debug.LogWarning( e.ToString() ); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        checkAndSetSound();
    }

    // this function is used to get game_sound satate or set it if not found

    private void checkAndSetSound()
    {
        var aud = this.GetComponent<AudioSource>();
        if (aud != null)
        {
            // check if game_sound key was setting before
            if (PlayerPrefs.HasKey("game_sound"))
            {
              isSoundOn = PlayerPrefs.GetInt("game_sound") == 1 ? true : false;
                
                if (isSoundOn && !aud.isPlaying) {
                    
                    aud.Play();
                   
                }
                else if(!isSoundOn && aud.isPlaying) {
                    aud.Pause();

                   
                }
            }
            //if game_sound not setting we reset it 
            else
            {
                PlayerPrefs.SetInt("game_sound", 1);
                PlayerPrefs.Save();
                Debug.Log("set_sound");
            }
        }
    }
}
