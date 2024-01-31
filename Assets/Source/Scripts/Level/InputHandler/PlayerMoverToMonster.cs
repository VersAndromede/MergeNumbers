using UnityEngine;

public class PlayerMoverToMonster : MonoBehaviour
{
    [SerializeField] private MonsterObserver _monsterObserver;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerRotation _playerRotation;

    public void Move(Monster monster)
    {
        if (CheckContentsMonster(monster) == false)
            return;

        if (_playerMovement.gameObject.activeSelf)
        {
            _playerMovement.MoveToMonster(monster.transform.position);
            StartPlayerRotation(monster.transform.position);
        }
    }

    private void StartPlayerRotation(Vector3 monsterPosition)
    {
        Vector3 playerPosition = _playerMovement.transform.position;
        Vector3 rotation = new Vector3(monsterPosition.x - playerPosition.x, 0, monsterPosition.z - playerPosition.z);
        _playerRotation.StartRotationJob(rotation);
    }

    private bool CheckContentsMonster(Monster wanted)
    {
        if (_monsterObserver == null || wanted == null)
            return false;

        foreach (Monster monster in _monsterObserver.Monsters)
            if (monster == wanted)
                return true;

        return false;
    }
}