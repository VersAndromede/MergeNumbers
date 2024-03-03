using Scripts.Level.HealthSystems;
using System;
using UnityEngine;

namespace Scripts.Level.BossSystem
{
    public class BossLoader : MonoBehaviour
    {
        [SerializeField] private BossListData _bossDatas;
        [SerializeField] private Transform _container;
        [SerializeField] private HealthSetup _bossHealthSetup;

        public event Action<Boss> Loaded;

        public int BossDataIndex { get; private set; }

        public Boss CurrentBoss { get; private set; }

        private void OnDestroy()
        {
            CurrentBoss.BossHealth.Died -= OnBossDied;
        }

        public void Init(int bossDataIndex)
        {
            BossDataIndex = bossDataIndex;

            if (BossDataIndex >= _bossDatas.Datas.Count)
                BossDataIndex = _bossDatas.Datas.Count - 1;

            CurrentBoss = Instantiate(_bossDatas.Datas[BossDataIndex].Prefab, _container);
            CurrentBoss.Init(_bossHealthSetup, _bossDatas.Datas[BossDataIndex]);
            CurrentBoss.BossHealth.Died += OnBossDied;
            Loaded?.Invoke(CurrentBoss);
        }

        private void OnBossDied()
        {
            BossDataIndex++;
        }
    }
}