using UnityEngine;

namespace Scripts.UI.Menu.BossAchievements
{
    public class BossMapScroll : MonoBehaviour
    {
        [SerializeField] private Transform _contentTransform;

        public float YPosition => _contentTransform.position.y;

        public void Init(float yPosition)
        {
            float startYPosition = _contentTransform.position.y;
            _contentTransform.position = new Vector2(_contentTransform.position.x, yPosition);

            if (_contentTransform.position.y == 0)
                _contentTransform.position = new Vector2(_contentTransform.position.x, startYPosition);
        }
    }
}