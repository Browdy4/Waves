using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRauCastAttackConfig", menuName = "WeaponConfig/RayCastAttackConfig")]
public class RayCastAttackConfig : ScriptableObject
{
    [Header("Ray")]
    public LayerMask LayerMask;
    [Min(0)]public float Distance;
    [Min(0)]public int ShotCount;

    [Header("Spread")]
    public bool UseSpread;
    [Min(0)] public float SpreadFactorMin;
    [Min(0)] public float SpreadFactorMax;

    [Header("Paricles")]
    //public ParticleSystem MuzzleEffect;
    public ParticleSystem HitEffectPrefab;
    public ParticleSystem HitEffectEnemyPrefab;
    [Min(0)] public float HitEffectDestroyDelay;
}
