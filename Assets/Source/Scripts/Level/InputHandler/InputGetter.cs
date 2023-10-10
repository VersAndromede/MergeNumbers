using UnityEngine;

public class InputGetter : MonoBehaviour
{
    [SerializeField] private MobileInput _mobileInput;
    [SerializeField] private DesktopInput _desktopInput;

    public IInput GetInput()
    {
        if (Application.isMobilePlatform)
            return _mobileInput;
        else
            return _desktopInput;
    }
}
