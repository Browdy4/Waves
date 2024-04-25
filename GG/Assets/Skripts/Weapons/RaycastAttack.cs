using UnityEngine;

public class RaycastAttack : MonoBehaviour
{
    [SerializeField]private WeaponConfig weaponConfig;

    [Header("Muzzles")]
    [SerializeField] private ParticleSystem _muzzleEffect;
    [SerializeField] private Transform _shootPosition;

    public float spreadFactorMin => weaponConfig.RayCastAttackConfig.SpreadFactorMin;
    public float spreadFactorMax => weaponConfig.RayCastAttackConfig.SpreadFactorMax;
    private float _damage => weaponConfig.Damage;
    private LayerMask _layerMask => weaponConfig.RayCastAttackConfig.LayerMask;
    private float _distance => weaponConfig.RayCastAttackConfig.Distance;
    private int _shotCount => weaponConfig.RayCastAttackConfig.ShotCount;
    private bool _useSpread => weaponConfig.RayCastAttackConfig.UseSpread;
    private ParticleSystem _hitEffectPrefab => weaponConfig.RayCastAttackConfig.HitEffectPrefab;
    private ParticleSystem _hitEffectEnemyPrefab => weaponConfig.RayCastAttackConfig.HitEffectEnemyPrefab;
    private float _hitEffectDestroyDelay => weaponConfig.RayCastAttackConfig.HitEffectDestroyDelay;

    [HideInInspector] public float spreadFactor;

    private void Start()
    {
        spreadFactor = spreadFactorMin;
    }

    public void PerformAttack()
    {
        for (int i = 0; i < _shotCount; i++)
        {
            PerformRayCast();
        }   
        _muzzleEffect.Play();
    }

    private void PerformRayCast()
    {
        Vector3 direction = _useSpread ? _shootPosition.up + CalculateSpread() : _shootPosition.up;//  forward - default
        Ray ray = new Ray(_shootPosition.position,direction);

        if (Physics.Raycast(ray,out RaycastHit hitInfo,_distance,_layerMask))
        {
            if (hitInfo.collider.TryGetComponent(out IDamageable damageable))
            {
                damageable.ApplyDamage(_damage);
                SpawnParticleEffectOnEnemyHit(hitInfo);
            }
            else
            {
                SpawnParticleEffectOnHit(hitInfo);
            }
        }
    }
    private void SpawnParticleEffectOnHit(RaycastHit hitInfo)
    {
        if (_hitEffectPrefab != null)
        {
            Quaternion hitEffectRotation = Quaternion.LookRotation(hitInfo.normal);
            ParticleSystem hitIEffect = Instantiate(_hitEffectPrefab, hitInfo.point, hitEffectRotation);

            Destroy(hitIEffect.gameObject, _hitEffectDestroyDelay);
        }
    }

    private void SpawnParticleEffectOnEnemyHit(RaycastHit hitInfo)
    {
        if (_hitEffectEnemyPrefab != null)
        {
            Quaternion hitEffectRotation = Quaternion.LookRotation(hitInfo.normal);
            ParticleSystem hitIEffect = Instantiate(_hitEffectEnemyPrefab, hitInfo.point, hitEffectRotation);

            Destroy(hitIEffect.gameObject, _hitEffectDestroyDelay);
        }
    }

    private Vector3 CalculateSpread()
    {
        return new Vector3
        {
            x = Random.Range(-spreadFactor, spreadFactor),
            y = Random.Range(-spreadFactor, spreadFactor),
            z = Random.Range(-spreadFactor, spreadFactor)
        };
    }

    private void OnDrawGizmos()
    {
        Vector3 direction = _useSpread ? _shootPosition.up + CalculateSpread() : _shootPosition.up;// forward - default
        Ray ray = new Ray(_shootPosition.position,direction);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, _distance, _layerMask))
        {
            DrawRay(ray, hitInfo.point, hitInfo.distance, Color.red);
        }
        else
        {
            var hitPosition = ray.origin + ray.direction * _distance;

            DrawRay(ray, hitPosition, _distance, Color.green);
        }
    }
    private void DrawRay(Ray ray, Vector3 hitPosition,float distance,Color color)
    {
        const float hitPointRadius = 0.15f;

        Debug.DrawRay(ray.origin, ray.direction * distance, color);

        Gizmos.color = color;
        Gizmos.DrawSphere(hitPosition,hitPointRadius);
    }
}
