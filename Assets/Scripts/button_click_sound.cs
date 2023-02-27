using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button_click_sound : MonoBehaviour
{
    public AudioSource audioS;
    public void clickSound()
    {
        try
        {
            audioS.Play();
        }catch(System.Exception e)
        {
            Debug.Log(e);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
