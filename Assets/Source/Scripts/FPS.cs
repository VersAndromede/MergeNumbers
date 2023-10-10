using UnityEngine;

public class FPS : MonoBehaviour
{
    [SerializeField] private uint _value;

    private void Awake()
    {
        Application.targetFrameRate = (int)_value;
    }
}
