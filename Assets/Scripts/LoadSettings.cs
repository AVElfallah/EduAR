using System;
using UnityEngine;

public class LoadSettings : MonoBehaviour
{

    public toggel_button toggel;
    public toggel_button toggle_notification;
    private bool isSoundOn;
    private bool isNotificationOn;

    /// Checks and sets sound and notification on / off. This is called when the game starts and it's safe to call this
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
            Debug.LogWarning(e.ToString());
        }
    }
    /// Updates the sounds and notifications. This is called every frame to ensure that we don't have stuck in an infinite loop
    void Update()
    {
        checkAndSetSound();
        checkAndSetNotification();
    }
    /// Checks if notification is on and sets it if not. This is called every time PlayerPrefs. Save
    private void checkAndSetNotification()
    {
        /// Set the notification flag to 1.
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

    /// Checks and sets sound on / off. This is called every time the game is loaded or unloaded
    private void checkAndSetSound()
    {
        var aud = this.GetComponent<AudioSource>();
        /// Set the sound on or off
        if (aud != null)
        {

            /// Set the sound on or off
            if (PlayerPrefs.HasKey("game_sound"))
            {
                isSoundOn = PlayerPrefs.GetInt("game_sound") == 1 ? true : false;

                /// Play or pause the audio if playing.
                if (isSoundOn && !aud.isPlaying)
                {
                    aud.Play();
                }
                /// Pause the audio if playing.
                else if (!isSoundOn && aud.isPlaying)
                {
                    aud.Pause();
                }
            }
            else
            {
                PlayerPrefs.SetInt("game_sound", 1);
                PlayerPrefs.Save();
                Debug.Log("set_sound");
            }
        }
    }
}
