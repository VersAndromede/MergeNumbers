using MonsterSystem;
using UnityEngine;

namespace MonsterSpawnerSystem
{
    public class MonsterFabric : MonoBehaviour
    {
        [SerializeField] private MonsterAdding _monsterAddingPrefab;
        [SerializeField] private MonsterSubtractive _monsterSubtractivePrefab;
        [SerializeField] private MonsterDivider _monsterDividerPrefab;

        public Monster GetAdding(int power, Vector3 monsterStartPosition)
        {
            return CreateMonster(_monsterAddingPrefab, power, monsterStartPosition);
        }

        public Monster GetSubtractive(int power, Vector3 monsterStartPosition)
        {
            return CreateMonster(_monsterSubtractivePrefab, power, monsterStartPosition);
        }

        public Monster GetDivider(int power, Vector3 monsterStartPosition)
        {
            return CreateMonster(_monsterDividerPrefab, power, monsterStartPosition);
        }

        private Monster CreateMonster(Monster prefab, int power, Vector3 monsterStartPosition)
        {
            Monster monster = Instantiate(prefab, transform);
            monster.Init(power);
            monster.transform.position = monsterStartPosition;
            return monster;
        }
    }
}