/*using Discord;
using System;
using UnityEngine;

public class DController : MonoBehaviour
{
    public static Discord.Discord discord;
    public static ActivityManager activityManager;

    void Start()
    {
        discord = new Discord.Discord(677993610330898433,
            (UInt64)CreateFlags.NoRequireDiscord);
        activityManager = discord.GetActivityManager();

#if UNITY_EDITOR
        discord.SetLogHook(LogLevel.Debug, LogProblems);
#endif
        DontDestroyOnLoad(this.gameObject);
        UpdateFloors();
    }

    public static Activity activity = new Activity
    {
        State = "Floor: ...",
        Details = "In the Main Menu.",
        Timestamps = new ActivityTimestamps
        {
            Start = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()
        }
    };

    public static void UpdateFloors()
    {
        //activityManager.UpdateActivity(activity, (result) =>
        {
            if (result == Discord.Result.Ok)
            {
                activity.State = "Floor: " + Globals.playerFloor;
            }
            else
            {
                Debug.LogError("Failed to update presence.");
            }
        });
    }

    [RuntimeInitializeOnLoadMethod]
    static void Autorun()
    {
        Application.quitting += disposeSDK;
    }

    static void disposeSDK()
    {
        Debug.Log("Disposing Discord SDK...");
        discord.Dispose();
    }

#if UNITY_EDITOR
    void LogProblems(Discord.LogLevel level, string msg)
    {
        Debug.LogWarningFormat("Discord: {0} - {1}", level, msg);
    }
#endif

    void Update()
    {
        discord.RunCallbacks();
    }
}
*/