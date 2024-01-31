using System.Collections;
using UnityEngine;

public class MonsterParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private float _destroyedTime;

    private WaitForSeconds _waitDestroyedTime;

    private void Start()
    {
        _waitDestroyedTime = new WaitForSeconds(_destroyedTime);
    }

    public void Play()
    {
        transform.SetParent(null);
        _particleSystem.Play();
        StartCoroutine(Destroy());
    }

    private IEnumerator Destroy()
    {
        yield return _waitDestroyedTime;
        Destroy(gameObject);
    }
}