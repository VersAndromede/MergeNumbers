using UnityEngine;
using DG.Tweening;

public class Watter : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private float _speed;

    private Vector2 _movementDirection;

    private void Start()
    {
        int x = 0;
        int y = 0;

        if (Randomizer.GetBool())
            x = InitVectorRandomPoint();
        else
            y = InitVectorRandomPoint();

        _movementDirection = new Vector2(x, y);
    }

    private void Update()
    {
        _meshRenderer.material.mainTextureOffset += Time.deltaTime * _speed * _movementDirection;
    }

    private int InitVectorRandomPoint()
    {
        if (Randomizer.GetBool())
            return 1;
        else
            return -1;
    }
}
