using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EditSettings : MonoBehaviour
{
    public AndroidNotifications androidNotifications;

    private void Start()
    {

    }
    public void TurnOffNotification()
    {
        Debug.Log("isva:" + 0);
        PlayerPrefs.SetInt("notification", 0);
        PlayerPrefs.Save();
        androidNotifications.ToggleNotifications();
    }
    public void TurnOnNotification()
    {
        Debug.Log("isva:" + 1);
        PlayerPrefs.SetInt("notification", 1);
        PlayerPrefs.Save();
        androidNotifications.ToggleNotifications();
    }
    //set game sound on or of
    public void TurnOffGameSounds()
    {

        Debug.Log("isva:" + 0);
        PlayerPrefs.SetInt("game_sound", 0);
        PlayerPrefs.Save();

    }
    public void TurnOnGameSounds()
    {

        Debug.Log("isva:" + 1);
        PlayerPrefs.SetInt("game_sound", 1);
        PlayerPrefs.Save();

    }
}
