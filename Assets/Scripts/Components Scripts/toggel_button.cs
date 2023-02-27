using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class toggel_button : MonoBehaviour
{
    [SerializeField]
    public GameObject onButton;
    public GameObject offButton;
    public AudioSource audioSource;
    private bool isClicked =true ;
   
    void Start()
    {
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
 
    public void ClickWithValue(bool val)
    {
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
    public void ClickToggle()
    {
        Debug.Log(" Clicked 2");
        audioSource.Play();
        if(isClicked)
        {
            onButton.SetActive(false);
            offButton.SetActive(true);
            isClicked =false;
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
