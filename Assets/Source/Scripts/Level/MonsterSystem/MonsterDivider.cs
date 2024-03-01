using Scripts.Level.PowerSystem;

namespace Scripts.Level.MonsterSystem
{
    public class MonsterDivider : Monster
    {
        public override void SetEffect(Power target)
        {
            target.Divide(PowerCount);
        }
    }
}