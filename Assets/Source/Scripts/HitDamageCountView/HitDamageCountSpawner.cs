using BossSystem;
using UnityEngine;

namespace HitDamage
{
    public class HitDamageCountSpawner : MonoBehaviour
    {
        [SerializeField] private HitDamageCountText _hitDamageCountTextPrefab;
        [SerializeField] private Vector3 _downBoard;
        [SerializeField] private Vector3 _upBoard;

        private BossHealth _bossHealth;

        private void OnDestroy()
        {
            _bossHealth.DamageReceived -= OnBossDamageReceived;
        }

        public void Init(BossHealth bossHealth)
        {
            _bossHealth = bossHealth;
            _bossHealth.DamageReceived += OnBossDamageReceived;
        }

        private void OnBossDamageReceived(int damage)
        {
            HitDamageCountText hitDamageCountText = Instantiate(_hitDamageCountTextPrefab, transform);
            float xPosition = Random.Range(_downBoard.x, _upBoard.x);
            float yPosition = Random.Range(_downBoard.y, _upBoard.y);
            float zPosition = Random.Range(_downBoard.z, _upBoard.z);

            hitDamageCountText.transform.position = new Vector3(xPosition, yPosition, zPosition);
            hitDamageCountText.Init(damage);
        }
    }
}
