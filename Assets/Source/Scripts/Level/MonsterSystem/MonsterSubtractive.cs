using Scripts.Level.PowerSystem;

namespace Scripts.Level.MonsterSystem
{
    public class MonsterSubtractive : Monster
    {
        public override void SetEffect(Power target)
        {
            target.Add(-PowerCount);
        }
    }
}