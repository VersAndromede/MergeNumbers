using UnityEngine;

public class FPS : MonoBehaviour
{
    [SerializeField] private int _value;

    private void Awake()
    {
        Application.targetFrameRate = _value;
    }
}
