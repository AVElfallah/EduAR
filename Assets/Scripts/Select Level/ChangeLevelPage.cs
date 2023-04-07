using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ChangeLevelPage : MonoBehaviour
{
    [SerializeField]
    public GameObject[] panels = new GameObject[3];
    public GameObject[] levels= new GameObject[28];



    private int currentPage = 0;
    void Start()
    {
        if (PlayerPrefs.HasKey("c_level")) {
            Debug.Log("c_isFound");
            int cl = PlayerPrefs.GetInt("c_level");
            Debug.Log(cl);
            for (int i=0;i<=cl;i++)
            {
                var xi= levels[i].GetComponent<LevelButtonScript>();
                xi.changeLevelValues(true);
                
            
                Debug.Log(xi.name);
            }

        }
        else
        {
            Debug.Log("not_isFound");
            PlayerPrefs.SetInt("c_level", 0);
            PlayerPrefs.Save();
            var xi = levels[0].GetComponent<LevelButtonScript>();
            xi.changeLevelValues(true);
        }


    }
    public void leftChangePage()
    {
        changePage(-1);
    } public void rieghtChangePage()
    {
        changePage(1);
    }

    private void changePage(int dir)
    {
        try
        {
            currentPage += dir;
            currentPage = currentPage > 2 ? 0 : currentPage;
            currentPage = currentPage < 0 ? 2 : currentPage;
            for (int i = 0; i < panels.Length; i++)
            {
                if (i == currentPage)
                {
                    panels[i].SetActive(true);
                }
                else
                {
                    panels[i].SetActive(false);
                }
            }
        }
        finally
        {

        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
