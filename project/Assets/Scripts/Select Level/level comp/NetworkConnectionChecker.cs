using UnityEngine;
using System.Collections;

public class NetworkConnectionChecker : MonoBehaviour
{
    public float checkInterval = 1.5f; // Interval for checking network status in seconds

    private bool isConnectionLost = false; // Flag to track connection status
    public GameObject menu;
    public GameObject screenComponents;

    private void Start()
    {
        StartCoroutine(CheckNetworkStatus()); // Start checking network status
    }

    IEnumerator CheckNetworkStatus()
    {
        while (true)
        {
            // Check for network connection
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                // No network connections
                if (!isConnectionLost)
                {
                    // Perform event when connection is lost (only once)
                    PerformEventOnConnectionLost();
                    Time.timeScale=0f;
                    isConnectionLost = true;
                    menu.SetActive(true);
                    screenComponents.SetActive(false);
                }
            }
            else
            {
                // Network connection available
                if (isConnectionLost)
                {
                    // Perform event when connection is restored (only once)
                    PerformEventOnConnectionRestored();
                    isConnectionLost = false;
                    Time.timeScale=1f;
                    menu.SetActive(false);
                    screenComponents.SetActive(true);
                }
            }

            yield return new WaitForSeconds(checkInterval);
        }
    }

    void PerformEventOnConnectionLost()
    {
        // Perform event when connection is lost
        Debug.Log("Network connection lost!");
    }

    void PerformEventOnConnectionRestored()
    {
        // Perform event when connection is restored
        Debug.Log("Network connection restored!");
    }
}
