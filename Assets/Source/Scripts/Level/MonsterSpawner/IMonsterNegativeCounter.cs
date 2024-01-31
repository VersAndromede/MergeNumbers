public interface IMonsterNegativeCounter
{
    int AllCount { get; }
    int MaxAllCount { get; }
    int DividersCount { get; }
    int MaxDividersCount { get; }

    void OnMonsterSpawned(Monster monster, int power);
    void OnCounterRestartRequired();
}