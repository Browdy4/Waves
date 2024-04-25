using System;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GrenadeAttack : MonoBehaviour
{
    [Header("Common")]
    [SerializeField] private OverlapAttack _overlapAttack;
    [SerializeField, Min(0f)] private float _delayExplosion;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private MeshRenderer _meshRenderer;

    [Header("Rigidbody")]
    [SerializeField] private Rigidbody _rigidbody;

    [Header("Effects")]
    [SerializeField] private ParticleSystem _explosionEffectPrefab;
    [SerializeField,Min(0f)] private float _explosionEffectLifetime;

    public Rigidbody Rigidbody => _rigidbody;
    private async void Start()
    {
        await PrepareAttack();

        _overlapAttack.PerformAttack();

        _meshRenderer.enabled = false;

        PlayExplosionSound();

        SpawnEffectOnDestroy();

        Destroy(gameObject, _audioSource.clip.length);
    }

    private async Task PrepareAttack()
    {
        await Task.Delay(TimeSpan.FromSeconds(_delayExplosion));
    }

    private void SpawnEffectOnDestroy()
    {
        var effect = Instantiate(_explosionEffectPrefab, transform.position, Quaternion.Euler(Vector3.up));

        Destroy(effect.gameObject, _explosionEffectLifetime);
    }

    private void PlayExplosionSound()
    {
        _audioSource.Play();
    }
}
