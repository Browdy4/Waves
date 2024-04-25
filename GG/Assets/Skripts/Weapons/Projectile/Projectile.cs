using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Projectile : MonoBehaviour
{
    //[Header("Common")]
    //[SerializeField,Min(0f)] private float _damage = 150f;
    //[SerializeField] private ProjectileDisposeType _disposeType = ProjectileDisposeType.Manual;

    //[Header("Rigidbody")]
    //[SerializeField] private Rigidbody _projectileRigidbody;

    //[Header("EffectOnDestroy")]
    //[SerializeField] private ParticleSystem _effectDestroyPrefab;
    //[SerializeField, Min(0f)] private float _effectOnDestroyLifeTime = 2f;

    //private bool _isProjectileDisposed;

    //public float Damage => _damage;
    //public ProjectileDisposeType DisposeType => _disposeType;
    //public Rigidbody Rigidbody => _projectileRigidbody;

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (_isProjectileDisposed)
    //        return;
    //}
}
