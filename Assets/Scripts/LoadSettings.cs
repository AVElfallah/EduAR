using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSettings : MonoBehaviour
{
    //to change toggel state depend on settings was loaded from player prefs
    // we use [toggel_button] to do this event
    public toggel_button toggel;
    public toggel_button toggle_notification;
    private bool isSoundOn;
    private bool isNotificationOn;

    // Start is called before the first frame update
    void Start()
    {

        try
        {
            checkAndSetSound();
            checkAndSetNotification();
            var tof = toggel.gameObject.GetComponent<toggel_button>();
            var tof_notification = toggle_notification.gameObject.GetComponent<toggel_button>();
            tof.ClickWithValue(isSoundOn);
            tof_notification.ClickWithValue(isNotificationOn);

        }
        catch (Exception e)
        {
            Debug.LogWarning(
                e.ToString()
                );
        }
    }

    // Update is called once per frame
    void Update()
    {
        checkAndSetSound();
        checkAndSetNotification();
    }

    private void checkAndSetNotification()
    {
        if (PlayerPrefs.HasKey("notification"))
        {
            isNotificationOn = PlayerPrefs.GetInt("notification") == 1 ? true : false;
        }
        else
        {
            PlayerPrefs.SetInt("notification", 1);
            PlayerPrefs.Save();
            isNotificationOn = true;
        }
    }


    private void checkAndSetSound()
    {
        var aud = this.GetComponent<AudioSource>();
        if (aud != null)
        {
            // check if game_sound key was setting before
            if (PlayerPrefs.HasKey("game_sound"))
            {
                isSoundOn = PlayerPrefs.GetInt("game_sound") == 1 ? true : false;

                if (isSoundOn && !aud.isPlaying)
                {

                    aud.Play();

                }
                else if (!isSoundOn && aud.isPlaying)
                {
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
