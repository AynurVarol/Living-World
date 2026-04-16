using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public GameManager gameManager;
    public UIManager uIManager;
    public IdleSystem idleSystem;

    string saveKey = "GAME_ SAVE";

    private void Start()
    {
        Laod();
    }
    public void Save()
    {
        SaveData data = new SaveData();

        data.coins = gameManager.coins;
        data.speedLevel = uIManager.speedLevel;
        data.speedCost = uIManager.speedCost;
        data.tickInterval = idleSystem.tickInterval;

        string json = JsonUtility.ToJson(data);

        PlayerPrefs.SetString(saveKey, json);
        PlayerPrefs.Save();

        Debug.Log("Game Saved");

    }

    public void Laod()
    {
        if(!PlayerPrefs.HasKey(saveKey))
        {
            Debug.Log("No save found");
            return;
        }

        string json = PlayerPrefs.GetString(saveKey);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        gameManager.coins = data.coins;
        uIManager.speedLevel = data.speedLevel;
        uIManager.speedCost = data.speedCost;
        idleSystem.tickInterval = data.tickInterval;

        Debug.Log("Game Loaded");
    }

    public void ResetSave()
    {
        PlayerPrefs.DeleteKey("GAME_SAVE");
        PlayerPrefs.Save();
        Debug.Log("Save Reset");
    }
}
