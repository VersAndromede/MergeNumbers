using System.Collections.Generic;
using UnityEngine;

public class MonsterObserver : MonoBehaviour
{
    private List<Monster> _monsters = new List<Monster>();

    public IReadOnlyList<Monster> Monsters => _monsters;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Monster monster))
            _monsters.Add(monster);
    }

    public void Clear()
    {
        _monsters = new List<Monster>();
    }
}
