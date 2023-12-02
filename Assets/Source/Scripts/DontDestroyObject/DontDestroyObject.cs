using UnityEngine;

public class DontDestroyObject : MonoBehaviour
{
    private static DontDestroyObject _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            DontDestroyOnLoad(gameObject);
            _instance = this;
            return;
        }

        Destroy(gameObject);
    }
}
