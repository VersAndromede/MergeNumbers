using UnityEngine;

namespace Scripts.CameraSystem
{
    public class OrientationGetter : MonoBehaviour
    {
        [SerializeField] private Vector2 _aspectRatio;

        public Orientation Get()
        {
            if (Screen.width / (float)Screen.height * _aspectRatio.x <= _aspectRatio.y)
                return Orientation.Portrait;
            else if (Screen.height / (float)Screen.width * _aspectRatio.x <= _aspectRatio.y)
                return Orientation.Landscape;
            else
                return Orientation.Default;
        }
    }
}