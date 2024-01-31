using PowerSystem;

namespace MonsterSystem
{
    public class MonsterSubtractive : Monster
    {
        public override void SetEffect(Power target)
        {
            target.Add(-PowerCount);
        }
    }
}