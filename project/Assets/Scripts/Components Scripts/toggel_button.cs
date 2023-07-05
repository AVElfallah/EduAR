using UnityEngine;
public class toggel_button : MonoBehaviour
{
    [SerializeField]
    public GameObject onButton;
    public GameObject offButton;
    public AudioSource audioSource;
    private bool isClicked = true;
    /// Called when the button is clicked. Activates or deactivates the on / off buttons depending on whether the button is clicked
    void Start()
    {
        /// Set the active state of the on and off buttons.
        if (isClicked)
        {
            onButton.SetActive(true);
            offButton.SetActive(false);
        }
        else
        {
            onButton.SetActive(false);
            offButton.SetActive(true);
        }
    }
    /// Clicks the on / off buttons with the value passed. This is useful for toggling the state of the mouse
    /// 
    /// @param val - True to click on false to
    public void ClickWithValue(bool val)
    {
        /// Set the active state of the button
        if (val)
        {
            onButton.SetActive(false);
            offButton.SetActive(true);
            isClicked = val;
            Debug.Log("value from ctrl");
        }
        else
        {
            onButton.SetActive(true);
            offButton.SetActive(false);
            isClicked = val;
            Debug.Log("value from ctrl 2");
        }
    }
    /// Toggles the on / off buttons and plays the audio source. This is called by the click handler
    public void ClickToggle()
    {
        Debug.Log(" Clicked 2");
        audioSource.Play();
        /// Set the active state of the on and off buttons.
        if (isClicked)
        {
            onButton.SetActive(false);
            offButton.SetActive(true);
            isClicked = false;
            Debug.Log(" Clicked 1");
        }
        else
        {
            onButton.SetActive(true);
            offButton.SetActive(false);
            isClicked = true;
            Debug.Log(" Clicked 2");
        }
    }
}
