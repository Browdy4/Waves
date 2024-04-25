using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemy/Enemy")]
public class EnemyConfig : ScriptableObject
{
    [Header("Health")]
    public float Health;

    [Header("Delay")]
    public float DestroyEnemyDelay;
}
