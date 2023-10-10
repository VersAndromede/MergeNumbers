using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossListData")]
public class BossListData : ScriptableObject
{
    [SerializeField] private List<BossData> _bossDatas;

    public IReadOnlyList<BossData> Datas => _bossDatas;
}
