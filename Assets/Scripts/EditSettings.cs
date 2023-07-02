
using UnityEngine;

public class EditSettings : MonoBehaviour
{
    public AndroidNotifications androidNotifications;
    /// Turns off notifications and saves preferences to prevent it from disappearing. Notifications are disabled when the player is in the middle of a
    public void TurnOffNotification()
    {
        Debug.Log("isva:" + 0);
        PlayerPrefs.SetInt("notification", 0);
        PlayerPrefs.Save();
        androidNotifications.ToggleNotifications();
    }
    /// Turns on notifications and saves preferences to prevent notifications from disappearing in future. This is useful for debugging
    public void TurnOnNotification()
    {
        Debug.Log("isva:" + 1);
        PlayerPrefs.SetInt("notification", 1);
        PlayerPrefs.Save();
        androidNotifications.ToggleNotifications();
    }
    /// Turns off sounds for the game. This is called when the game is started but not when we want to play the
    public void TurnOffGameSounds()
    {
        Debug.Log("isva:" + 0);
        PlayerPrefs.SetInt("game_sound", 0);
        PlayerPrefs.Save();
    }
    /// Turns on sounds for the game. This is called when the player presses the button to turn on
    public void TurnOnGameSounds()
    {
        Debug.Log("isva:" + 1);
        PlayerPrefs.SetInt("game_sound", 1);
        PlayerPrefs.Save();
    }
}
