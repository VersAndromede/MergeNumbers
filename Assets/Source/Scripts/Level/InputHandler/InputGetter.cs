using UnityEngine;

public class InputGetter : MonoBehaviour
{
    [SerializeField] private MobileInput _mobileInput;
    [SerializeField] private DesktopInput _desktopInput;

    public IInput Get()
    {
        if (Application.isMobilePlatform)
            return _mobileInput;
        else
            return _desktopInput;
    }
}
