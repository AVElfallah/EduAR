
using UnityEngine;

public class button_click_sound : MonoBehaviour
{
    public AudioSource audioS;
    /// Play sound when mouse click on button Sound is played in main window and not in window's
    public void clickSound()
    {
        try
        {
            audioS.Play();
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
    }
}
