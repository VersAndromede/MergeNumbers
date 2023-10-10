using System;
using UnityEngine;

public abstract class Upgrade : MonoBehaviour
{
    [SerializeField] private int _id;

    [field: SerializeField] public int MaxLevel { get; private set; }
    [field: SerializeField] public int Price { get; private set; }
    [field: SerializeField] public int BonusValue { get; private set; }

    public event Action LevelChanged;

    private const int MinLevel = 1;

    public int Level { get; private set; } = MinLevel;
    public bool CanImprove => Level < MaxLevel;

    private void OnValidate()
    {
        const int MaxId = 2;

        if (_id < 0 || _id > MaxId)
        {
            _id = Mathf.Clamp(_id, 0, MaxId);
            Debug.LogWarning($"The upgrade ID cannot exceed the last index ({MaxId}) of the array of stored upgrades or be lower than 0.");
        }
    }

    public abstract int AffectValue();
    public abstract int AffectPrice();

    public void Init(int level, int price, int bonusValue)
    {
        if (level < MinLevel)
            return;

        Level = level;
        Price = price;
        BonusValue = bonusValue;
    }
     
    public void Improve()
    {
        BonusValue = AffectValue();
        Price = AffectPrice();
        Level++;
        LevelChanged?.Invoke();

        if (Level > MaxLevel)
            throw new ArgumentOutOfRangeException(); 
    }
}