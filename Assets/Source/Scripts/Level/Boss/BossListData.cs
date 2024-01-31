using System.Collections.Generic;
using UnityEngine;

namespace BossSystem
{
    [CreateAssetMenu(fileName = "BossListData")]
    public class BossListData : ScriptableObject
    {
        [SerializeField] private List<BossData> _bossDatas;

        public IReadOnlyList<BossData> Datas => _bossDatas;
    }
}