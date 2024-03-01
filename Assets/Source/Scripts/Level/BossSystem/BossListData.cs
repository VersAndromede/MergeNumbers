using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Level.BossSystem
{
    [CreateAssetMenu(fileName = "BossListData")]
    public class BossListData : ScriptableObject
    {
        [SerializeField] private List<BossData> _bossDatas;

        public IReadOnlyList<BossData> Datas => _bossDatas;
    }
}