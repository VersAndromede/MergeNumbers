using System.Collections.Generic;
using UnityEngine;

public class BossMap : MonoBehaviour
{
    [SerializeField] private BossListData _bossDatas;
    [SerializeField] private BossMapItem _bossMapItemPrefab;
    [SerializeField] private Transform _container;

    public void Fill(List<BossAward> bossAwards, int bossDataIndex, Wallet wallet)
    {
        if (bossAwards.Count == 0)
            InitBossAwards(bossAwards);

        for (int i = 0; i < _bossDatas.Datas.Count; i++)
        {
            BossMapItem bossMapItem = Instantiate(_bossMapItemPrefab, _container);
            bossMapItem.Init(_bossDatas.Datas[i], wallet, bossAwards[i]);

            if (bossDataIndex < i)
                bossMapItem.Lock();
        }
    }

    private void InitBossAwards(List<BossAward> bossAwards)
    {
        for (int i = 0; i < _bossDatas.Datas.Count; i++)
        {
            BossAward bossAward = new BossAward(_bossDatas.Datas[i].Award, i);
            bossAwards.Add(bossAward);
        }
    }
}
