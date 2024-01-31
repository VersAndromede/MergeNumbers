using PowerSystem;

namespace MonsterSystem
{
    public class MonsterAdding : Monster
    {
        public override void SetEffect(Power target)
        {
            target.Add(PowerCount);
        }
    }
}