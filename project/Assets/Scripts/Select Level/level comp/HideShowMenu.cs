using UnityEngine;
public class HideShowMenu : MonoBehaviour
{
    public GameObject menu;
    bool isPuased = false;
    /// Hides or shows the menu depending on whether the pupuased is true or false.
    ///  This is called by Unity
    public void HideOrShowMenu()
    {
        /// Set the menu to be paused
        if (isPuased)
        {
            menu.SetActive(false);
            Time.timeScale = 1f;
            isPuased = false;
        }
        else
        {
            menu.SetActive(true);
            Time.timeScale = 0f;
            isPuased = true;
        }
    }
}
