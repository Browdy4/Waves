using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewOverlapConfig", menuName = "Overlap/OverlapConfig")]
public class OverlapConfig : ScriptableObject
{
    [Header("Common")]
    [Min(0f)] public float Damage;

    [Header("Masks")]
    public LayerMask SearchLayerMask;
    public LayerMask ObstacleLayerMask;

    [Header("Overlap Area")]
    [Min(0f)] public float SphereRadius;
    public Vector3 Offset;

    [Header("Obstacles")]
    public bool ConsiderObstacles;

    [Header("Gizmos")]
    public Color GizmosColor = Color.cyan;
}
