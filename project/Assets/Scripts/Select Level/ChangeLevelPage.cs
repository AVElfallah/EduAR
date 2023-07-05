
using UnityEngine;

public class ChangeLevelPage : MonoBehaviour
{
    [SerializeField]
    public GameObject[] panels = new GameObject[3];
    public GameObject[] levels = new GameObject[28];
    public GameObject[] rewards = new GameObject[3];
    private int currentPage = 0;
    /// Called when player starts. This is where you change the values of level and rewards in PlayerPre
    void Start()
    {
        /// This method is called by the Player s main level.
        if (PlayerPrefs.HasKey("c_level"))
        {
            Debug.Log("c_isFound");
            int cl = PlayerPrefs.GetInt("c_level");
            Debug.Log(cl);
            /// This function is called by the LevelButtonScript. changeLevelValues method.
            for (int i = 0; i <= cl; i++)
            {
                var xi = levels[i].GetComponent<LevelButtonScript>();
                xi.changeLevelValues(true);
                /// Change the level values of the rewards
                if (i == 6)
                {
                    var re = rewards[0].GetComponent<LevelButtonScript>();
                    re.changeLevelValues(true);
                }
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
    /// Changes the page to the left. This is equivalent to pressing the left button on the page and then releasing the
    public void leftChangePage()
    {
        changePage(-1);
    }
    /// Rieght einer Konfiguration ausgefuehrt wird. Diese Methode gesetzt
    public void rieghtChangePage()
    {
        changePage(1);
    }

    /// Changes the current page. This is called when the user clicks the page button. It will change the current page and the panels that are shown on the page
    /// 
    /// @param dir - The direction to change
    private void changePage(int dir)
    {
        try
        {
            currentPage += dir;
            currentPage = currentPage > 2 ? 0 : currentPage;
            currentPage = currentPage < 0 ? 2 : currentPage;
            /// Set the active state of all panels.
            for (int i = 0; i < panels.Length; i++)
            {
                /// Set the active state of the panel.
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
        finally { }
    }
}
