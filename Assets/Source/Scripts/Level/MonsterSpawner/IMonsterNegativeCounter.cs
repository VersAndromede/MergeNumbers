public interface IMonsterNegativeCounter
{
    int Count { get; }
    int MaxCount { get; }

    void OnMonsterSpawned(MonsterType type, int power);
    void Restart();
}