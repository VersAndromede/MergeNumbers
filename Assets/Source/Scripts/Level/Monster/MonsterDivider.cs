public class MonsterDivider : Monster
{
    public override void SetEffect(Power target)
    {
        target.Divide(PowerCount);
    }
}
