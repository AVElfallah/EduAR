using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButtonScript : MonoBehaviour
{
    public GameObject enabledLevelBtn;
    public GameObject disabledLevelBtn;
    public Text enabeldText;
    public Text disabledText;

  public  string levelname;
    public string levelpath;
    public bool enabled;


    // Start is called before the first frame update
    void Start()
    {
        enabledLevelBtn.GetComponent<Button>().onClick.AddListener(delegate(){
            toLevel();
        }); 
        try
        {
            enabeldText.text = levelname;
            disabledText.text = levelname;

            enabledLevelBtn.SetActive(enabled);
            disabledLevelBtn.SetActive(!enabled);

        }
        catch(Exception ec)
        {
            Debug.LogError(ec);
        }
       
    }
    private void toLevel()
    {
        SceneManager.LoadScene(levelpath);
    }
  public void  changeLevelValues(bool enabled)
    {
        this.enabled = enabled;
        enabledLevelBtn.SetActive(enabled);
        disabledLevelBtn.SetActive(!enabled);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
