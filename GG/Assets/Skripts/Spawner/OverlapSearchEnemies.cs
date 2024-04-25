using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapSearchEnemies : MonoBehaviour
{
    [Header("Overlap")]
    [SerializeField] private LayerMask _searchLayerMask;
    [SerializeField] private Vector3 _boxSize = Vector3.one;
    [SerializeField] private Transform _overlapStartPoint;

    private readonly Collider[] _overlapresults = new Collider[100];

    private int _overlapResultCount;

    public bool TryFindEnemy()
    {
        _overlapResultCount = OverlapBox();
        return _overlapResultCount <= 0;
    }

    private int OverlapBox()
    {
        return Physics.OverlapBoxNonAlloc(_overlapStartPoint.position, _boxSize / 2, _overlapresults, _overlapStartPoint.rotation, _searchLayerMask.value);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawCube(_overlapStartPoint.position, _boxSize / 2);
    }
}
