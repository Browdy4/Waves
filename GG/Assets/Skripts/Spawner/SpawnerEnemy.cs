using System.Collections.Generic;
using UnityEngine;
using NTC.Pool;
public class SpawnerEnemy : MonoBehaviour
{
    [SerializeField] private WaveManager _waveManager;

    [Header("Entity List")]
    [SerializeField] private List<GameObject> _enemies;
    [SerializeField] private List<Transform> _spawnersPoint;

    [Header("TestSpawn")]
    [SerializeField] GameObject _gameObjectPrefab;
    [SerializeField] private Transform _testSpawner;

    private int _numberEnemy;
    private int _numberSpawner;
    private bool _canSpawnEnemy = true;

    private void OnEnable()
    {
        _waveManager.OnSpawnStarted += SpawnEnemies;
    }

    private void OnDisable()
    {
        _waveManager.OnSpawnStarted -= SpawnEnemies;
    }

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    TestSpawn();
        //}
    }

    private void SpawnEnemies()
    {
        if (!_canSpawnEnemy) return;

        _numberEnemy = UnityEngine.Random.Range(0, _enemies.Count);
        _numberSpawner = UnityEngine.Random.Range(0, _spawnersPoint.Count);

        NightPool.Spawn(_enemies[_numberEnemy], _spawnersPoint[_numberSpawner].position, Quaternion.identity);
    }

    private void TestSpawn()
    {
        NightPool.Spawn(_gameObjectPrefab, _testSpawner.position, Quaternion.identity);
    }
}
