using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class OfflineSystem : MonoBehaviour
{
    private const string LastTimeKey = "LAST_TIME";
    public IdleSystem idleSystem;

    private void Start()
    {
        LoadOfflineProgress();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
            SaveTime();
    }

    private void OnApplicationQuit()
    {
        SaveTime();
    }

    void SaveTime()
    {
        PlayerPrefs.SetString(LastTimeKey, DateTime.UtcNow.ToString());
    }

    void LoadOfflineProgress()
    {
        if (!PlayerPrefs.HasKey(LastTimeKey)) return;

        DateTime lastTime = DateTime.Parse(PlayerPrefs.GetString(LastTimeKey));
        TimeSpan diff = DateTime.UtcNow - lastTime;

        float seconds = (float)diff.TotalSeconds;
        SimulateOffline(seconds);
    }

    void SimulateOffline(float seconds)
    {
        int ticks = Mathf.FloorToInt(seconds / idleSystem.tickInterval);

        for(int i = 0; i < ticks; i++)
        {
            idleSystem.SendMessage("Tick");
        }
    }
}
