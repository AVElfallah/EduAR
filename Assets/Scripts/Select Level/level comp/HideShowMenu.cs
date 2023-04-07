using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideShowMenu : MonoBehaviour
{
    public GameObject menu;
    bool isPuased = false;

    public void HideOrShowMenu()
    {
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
