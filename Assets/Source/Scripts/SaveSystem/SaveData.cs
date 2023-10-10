using System;
using System.Collections.Generic;
using UnityEngine.Scripting;

[Serializable]
public class SaveData
{
    [field: Preserve]
    public int Coins;
    [field: Preserve]
    public int BossDataIndex;
    [field: Preserve]
    public UpgradeData[] UpgradeDatas = new UpgradeData[3];
    [field: Preserve]
    public List<BossAward> BossAwards = new List<BossAward>();
    [field: Preserve]
    public float BossMapContentYPosition;
}
