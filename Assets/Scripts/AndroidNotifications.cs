using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;

public class AndroidNotifications : MonoBehaviour
{


    // Time between each notification in seconds (3 hours = 3 * 60 * 60)
    public int notificationInterval = 3;

    // Start is called before the first frame update
    void Start()
    {
        // Create Android notification channel
        var channel = new AndroidNotificationChannel()
        {
            Id = "default_channel",
            Name = "Default Channel",
            Importance = Importance.Default,
            Description = "Default notification channel"
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);

        // Schedule first notification
        ScheduleNotification();
    }

    // Schedule a new notification
    void ScheduleNotification()
    {
        if (PlayerPrefs.HasKey("notification") == false)
        {
            PlayerPrefs.SetInt("notification", 1);
            PlayerPrefs.Save();
        }
        bool playerNotificationSettings = PlayerPrefs.GetInt("notification", 1) == 1;

        if (playerNotificationSettings)
        {
            var notification = new AndroidNotification();
            notification.Title = "We miss you!🥺";
            notification.Text = "Come to complete your learning journey!❤️";
            notification.FireTime = System.DateTime.Now.AddSeconds(notificationInterval);

            AndroidNotificationCenter.SendNotification(notification, "default_channel");
        }
    }

    // Cancel all pending notifications
    void CancelNotifications()
    {
        AndroidNotificationCenter.CancelAllNotifications();
    }

    // Toggle notifications on/off
    public void ToggleNotifications()
    {
        bool playerNotificationSettings = PlayerPrefs.GetInt("notification", 1) == 1;


        if (playerNotificationSettings)
        {
            ScheduleNotification();
        }
        else
        {
            CancelNotifications();
        }
    }
}