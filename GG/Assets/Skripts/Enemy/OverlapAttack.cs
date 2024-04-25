using UnityEngine;

public class OverlapAttack : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private OverlapConfig _enemyOverlapConfig;
    [SerializeField] private DrawGizmosType _drawGizmosType;

    private Transform _overlapStartPoint;
    private readonly Collider[] _overlapresults = new Collider[32];
    private int _overlapResultCount;

    private float _damage => _enemyOverlapConfig.Damage;
    private LayerMask _searchLayerMask => _enemyOverlapConfig.SearchLayerMask;
    private LayerMask _obstacleLayerMask => _enemyOverlapConfig.ObstacleLayerMask;
    private Vector3 _offset => _enemyOverlapConfig.Offset;
    private float _sphereRadius => _enemyOverlapConfig.SphereRadius;
    private bool _coniderObstacles => _enemyOverlapConfig.ConsiderObstacles;
    private Color _gizmosColor => _enemyOverlapConfig.GizmosColor;

    private void Start()
    {
        _overlapStartPoint = transform;
    }

    public void PerformAttack()
    {
        if (TryFindEnemies())
        {
            AttackEnemies();
        }
    }

    private bool TryFindEnemies()
    {
        Vector3 position = _overlapStartPoint.TransformPoint(_offset);

        _overlapResultCount = OverlapSphere(position);

        return _overlapResultCount > 0;
    }

    private int OverlapSphere(Vector3 position)
    {
        return Physics.OverlapSphereNonAlloc(position, _sphereRadius, _overlapresults, _searchLayerMask.value);
    }

    private void AttackEnemies()
    {
        for (int i = 0; i < _overlapResultCount; i++)
        {
            if (_overlapresults[i].TryGetComponent(out IDamageable damageable) == false)
            {
                continue;
            }

            if (_coniderObstacles)
            {
                Vector3 startPointPosition = _overlapStartPoint.position;
                Vector3 colliderPoisition = _overlapresults[i].transform.position;
                bool hasObstacle = Physics.Linecast(startPointPosition, colliderPoisition, _obstacleLayerMask.value);

                if (hasObstacle)
                    continue;
            }

            damageable.ApplyDamage(_damage);
        }
    }

    private void OnDrawGizmos()
    {
        TryDrawGizmos(DrawGizmosType.Always);
    }

    private void TryDrawGizmos(DrawGizmosType requaredType)
    {
        if (_drawGizmosType != requaredType)
            return;

        if (_overlapStartPoint == null)
            return;

        Gizmos.matrix = _overlapStartPoint.localToWorldMatrix;
        Gizmos.color = _gizmosColor;

        Gizmos.DrawSphere(_offset, _sphereRadius);
    }
}
