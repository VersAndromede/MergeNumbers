using Scripts.Level.MonsterSystem;
using UnityEngine;

namespace Scripts.Level.GameInput
{
    public class MonsterFromRay
    {
        public bool TryGet(Vector3 origin, Vector3 direction, LayerMask layerMask, out Monster monster)
        {
            const float MaxDistance = 100;

            Vector3 offset = new Vector3(0, 0.25f, 0);
            Ray ray = new Ray(origin + offset, direction);

            if (Physics.Raycast(ray, out RaycastHit hit, MaxDistance, layerMask))
            {
                if (hit.collider.TryGetComponent(out Monster monster2))
                {
                    monster = monster2;
                    return true;
                }
            }

            monster = null;
            return false;
        }
    }
}