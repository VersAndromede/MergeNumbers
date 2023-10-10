using System.Collections;
using UnityEngine;

public class MonsterParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private float _destroyedTime;

    public void Play()
    {
        transform.SetParent(null);
        _particleSystem.Play();
        StartCoroutine(Destroy());
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(_destroyedTime);
        Destroy(gameObject);
    }
}