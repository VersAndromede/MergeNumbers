using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private InputGetter _inputGetter;
    [SerializeField] private Image _panel;
    [SerializeField] private GameMoves _gameMoves;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Camera _camera;
    [SerializeField] private Player _player;

    private IInput _input;
    private bool _isLockedMovement;

    private void Awake()
    {
        _input = _inputGetter.GetInput();
    }

    private void OnEnable()
    {
        _input.Received += Handle;
        _gameMoves.Ended += LockMovement;
    }

    private void OnDisable()
    {
        _input.Received -= Handle;
        _gameMoves.Ended -= LockMovement;
    }

    public void LockMovement()
    {
        _isLockedMovement = true;
        _panel.raycastTarget = false;
    }

    private void Handle(Direction direction)
    {
        if (_isLockedMovement)
            return;

        Vector3 newDirection;

        if (direction == Direction.Left)
            newDirection = Vector3.left;
        else if (direction == Direction.Right)
            newDirection = Vector3.right;
        else if (direction == Direction.Down)
            newDirection = -Vector3.forward;
        else
            newDirection = Vector3.forward;

        if (TrytGetComponentFromRay(newDirection, out Monster monster))
            TryMoveToMonster(monster);
    }

    private void TryMoveToMonster(Monster monster)
    {
        if (CheckContentsMonster(_player, monster) == false)
            return;

        if (_player.gameObject.activeSelf)
        {
            _player.Movement.MoveToMonster(monster.transform.position);
            StartPlayerRotation(monster.transform.position);
        }

        _panel.raycastTarget = false;
    }

    private bool TrytGetComponentFromRay<T>(Vector3 direction, out T component) where T : MonoBehaviour
    {
        const float MaxDistance = 100;

        Vector3 offset = new Vector3(0, 0.25f, 0);
        Ray ray = new Ray(_player.transform.position + offset, direction);

        if (Physics.Raycast(ray, out RaycastHit hit, MaxDistance, _layerMask))
        {
            if (hit.collider.TryGetComponent(out T t))
            {
                component = t;
                return true;
            }
        }

        component = default;
        return false;
    }

    private bool CheckContentsMonster(Player checker, Monster wanted)
    {
        if (checker == null || wanted == null)
            return false;

        foreach (Monster monster in checker.Observer.Monsters)
            if (monster == wanted)
                return true;

        return false;
    }

    private void StartPlayerRotation(Vector3 monsterPosition)
    {
        Vector3 playerPosition = _player.transform.position;
        Vector3 rotation = new Vector3(monsterPosition.x - playerPosition.x, 0, monsterPosition.z - playerPosition.z);
        _player.Rotation.StartRotationJob(rotation);
    }
}
