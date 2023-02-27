using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class change_level_page : MonoBehaviour
{
    [SerializeField]
    public GameObject[] panels = new GameObject[3];
    public int direction = -1;

    // Start is called before the first frame update

    private int currentPage = 0;
    void Start()
    {

    }
    public void ChangePageLevel() {

        try
        {
            currentPage += direction;
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
