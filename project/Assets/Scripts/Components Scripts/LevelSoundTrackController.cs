using UnityEngine;

public class LevelSoundTrackController : MonoBehaviour
{
    public AudioSource audio_src;
    /// Starts or pauses the audio source depending on game_sound.
    ///  This is called from Start () and should not be called directly
    void Start()
    {
        bool isSoundOn = PlayerPrefs.GetInt("game_sound") == 1 ? true : false;
        /// Play or pause sound on or pause
        if (isSoundOn)
        {
            audio_src.Play();
        }
        else
        {
            audio_src.Pause();
        }
    }
}
