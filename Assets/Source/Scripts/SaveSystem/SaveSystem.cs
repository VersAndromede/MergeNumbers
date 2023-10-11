using Agava.YandexGames;
using System;
using UnityEngine;

public static class SaveSystem
{
    private const string SaveDataPrefsKey = "SaveDataPrefsKey";

    public static void Save(Action<SaveData> callback)
    {
        if (Application.isEditor || PlayerAccount.IsAuthorized == false)
        {
            SaveToPrefs(callback);
            return;
        }

        PlayerAccount.GetPlayerData(data =>
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(data);
            callback?.Invoke(saveData);
            PlayerAccount.SetPlayerData(JsonUtility.ToJson(saveData));
        });
    }

    public static void Load(Action<SaveData> callback)
    {
        if (Application.isEditor || PlayerAccount.IsAuthorized == false)
        {
            callback?.Invoke(LoadFromPrefs());
            return;
        }

        PlayerAccount.GetPlayerData(data =>
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(data);
            callback?.Invoke(saveData);
        });
    }

    private static void SaveToPrefs(Action<SaveData> callback)
    {
        SaveData saveData = LoadFromPrefs();
        callback?.Invoke(saveData);
        string saveDataJson = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(SaveDataPrefsKey, saveDataJson);
        PlayerPrefs.Save();
    }

    private static SaveData LoadFromPrefs()
    {
        if (PlayerPrefs.HasKey(SaveDataPrefsKey))
        {
            string saveDataJson = PlayerPrefs.GetString(SaveDataPrefsKey);
            return JsonUtility.FromJson<SaveData>(saveDataJson);
        }

        return new SaveData();
    }
}