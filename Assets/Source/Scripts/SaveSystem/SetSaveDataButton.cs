using UnityEngine;
using TMPro;
using Agava.YandexGames;

public class SetSaveDataButton : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;

    public void Init(SaveData data)
    {
        _inputField.text = JsonUtility.ToJson(data, true);
    }

    public void SetSaveData()
    {
        if (Application.isEditor)
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(_inputField.text);
            string saveDataJson = JsonUtility.ToJson(saveData);
            PlayerPrefs.SetString("SaveDataPrefsKey", saveDataJson);
            PlayerPrefs.Save();
            return;
        }

        PlayerAccount.SetPlayerData(_inputField.text);
    }
}