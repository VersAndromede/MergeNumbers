using UnityEngine;

namespace Scripts.Level.PowerSystem
{
    [CreateAssetMenu(fileName = "Power Colors Config", menuName = "Data/Power Colors Config")]
    public class PowerColorsConfig : ScriptableObject
    {
        [field: SerializeField] public Color Positive { get; private set; }
        [field: SerializeField] public Color Negative { get; private set; }
        [field: SerializeField] public Color Neutral { get; private set; }
    }
}