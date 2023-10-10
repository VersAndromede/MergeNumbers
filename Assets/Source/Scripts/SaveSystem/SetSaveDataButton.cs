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
        PlayerAccount.SetPlayerData(_inputField.text);
    }
}