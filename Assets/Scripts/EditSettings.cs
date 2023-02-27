using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EditSettings : MonoBehaviour
{
  
    private void Start()
    {
       
        
    }
    //set game sound on or of
    public void TurnOffGameSounds()
    {
        
        Debug.Log("isva:"+0);
        PlayerPrefs.SetInt("game_sound",0);
        PlayerPrefs.Save();

    } public void TurnOnGameSounds()
    {
        
        Debug.Log("isva:"+1);
        PlayerPrefs.SetInt("game_sound",1);
        PlayerPrefs.Save();

    }
}
