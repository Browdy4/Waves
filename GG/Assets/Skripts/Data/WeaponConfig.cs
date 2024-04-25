using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "WeaponConfig/Weapon")]
public class WeaponConfig : ScriptableObject
{
    [Header("Data")]
    public Sprite Icon;

    [Header("Primary Attack")]
    public bool SemiAuto;
    public float Damage;
    public float CoolDownAttack;
    public RayCastAttackConfig RayCastAttackConfig;

    [Header("Ammo")]
    public int ClipSize;
    public int MaxAmmo;
    public float CoolDownReload;

    [Header("Audio")]
    public AudioConfig AudioConfig;
}
