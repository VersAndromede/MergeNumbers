using DG.Tweening;
using TMPro;
using UnityEngine;

public class HitDamageCountText : MonoBehaviour
{
    [SerializeField] private TextMeshPro _text;
    [SerializeField] private float _destroyDaley;

    public void Init(int damage)
    {
        _text.text = $"-{damage}";
        transform.DOScale(0, _destroyDaley).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}