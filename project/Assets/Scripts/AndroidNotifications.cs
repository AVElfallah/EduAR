using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;

public class AndroidNotifications : MonoBehaviour
{
    public int notificationInterval = 3;

    /// Starts the notification service. Registers the notification channel and schedules the notification to be sent to the notification
    void Start()
    {
        var channel = new AndroidNotificationChannel()
        {
            Id = "default_channel",
            Name = "Default Channel",
            Importance = Importance.Default,
            Description = "Default notification channel"
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);

        ScheduleNotification();
    }

    /// Schedules a notification to be sent every notificationInterval seconds. This is used to prevent flickering when there is a game
    void ScheduleNotification()
    {
        /// This method is called when the user clicks on the notification button.
        if (PlayerPrefs.HasKey("notification") == false)
        {
            PlayerPrefs.SetInt("notification", 1);
            PlayerPrefs.Save();
        }
        bool playerNotificationSettings = PlayerPrefs.GetInt("notification", 1) == 1;

        /// Send a notification to the AndroidNotificationCenter.
        if (playerNotificationSettings)
        {
            var notification = new AndroidNotification();
            notification.Title = "We miss you!🥺";
            notification.Text = "Come to complete your learning journey!❤️";
            notification.FireTime = System.DateTime.Now.AddSeconds(notificationInterval);

            AndroidNotificationCenter.SendNotification(notification, "default_channel");
        }
    }

    /// Cancels all notifications. This is called when the user clicks the Cancel button in the notification center to cancel all
    void CancelNotifications()
    {
        AndroidNotificationCenter.CancelAllNotifications();
    }

    /// Toggles notifications on and off. Notifications will be sent to the player when they are added or removed
    public void ToggleNotifications()
    {
        bool playerNotificationSettings = PlayerPrefs.GetInt("notification", 1) == 1;


        /// Schedule or cancel notifications if player notification settings are set.
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