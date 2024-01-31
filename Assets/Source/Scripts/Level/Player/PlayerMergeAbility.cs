using UnityEngine;

public class PlayerMergeAbility : MonoBehaviour
{
    private Power _power;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Monster monster))
            Merge(monster);
    }

    public void Init(Power power)
    {
        _power = power;
    }

    private void Merge(Monster monster)
    {
        monster.Die();
        monster.SetEffect(_power);
    }
}
