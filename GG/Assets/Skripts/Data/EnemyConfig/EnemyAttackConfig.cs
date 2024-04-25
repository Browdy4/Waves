using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyAttack", menuName = "Enemy/EnemyAttack")]
public class EnemyAttackConfig : ScriptableObject
{
    [Header("Common")]
    [Min(0f)] public float MaxAttackDistantion;

    [Header("Delays")]
    [Min(0)] public int IntervalUpdate;
    [Min(0f)] public float DelayPrepareAttack;
    [Min(0f)] public float CoolDownAttack;
}
