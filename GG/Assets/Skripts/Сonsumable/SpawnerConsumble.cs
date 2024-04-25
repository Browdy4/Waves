using System.Collections.Generic;
using UnityEngine;

public class SpawnerConsumble : MonoBehaviour
{
    [Header("SpawnPoints")]
    [SerializeField] private List<Transform> _spawnPoints;

    [Header("Consumbles")]
    [SerializeField] private GameObject _healthKitPrefab;
    [SerializeField] private GameObject _ammoBagPrefab;

    private GameObject _healthKit;
    private GameObject _ammoBag;

    private int _healthKitIndexSpawnPoint;
    private int _ammoBagIndexSpawnPoint;

    private int _currentHealthKitIndex;
    private int _currentAmmoBagIndex;

    private bool _canSpawn;

    private void Start()
    {
        _healthKit = Instantiate(_healthKitPrefab, _spawnPoints[0].position, Quaternion.identity);

        _ammoBag = Instantiate(_ammoBagPrefab, _spawnPoints[2].position, Quaternion.identity);
    }

    private void OnEnable()
    {
        WaveManager.OnHalfEnemiesDead += SpawnConsumbles;
    }

    private void OnDisable()
    {
        WaveManager.OnHalfEnemiesDead -= SpawnConsumbles;
    }

    private void SpawnConsumbles()
    {
        _canSpawn = true;

        if(_healthKit != null && _ammoBag != null)
            return;

        _healthKitIndexSpawnPoint = Random.Range(0, _spawnPoints.Count);

        _ammoBagIndexSpawnPoint = Random.Range(0, _spawnPoints.Count);

        while (_canSpawn)
        {
            if (_currentHealthKitIndex == _ammoBagIndexSpawnPoint || _healthKitIndexSpawnPoint == _ammoBagIndexSpawnPoint)
                _ammoBagIndexSpawnPoint = Random.Range(0, _spawnPoints.Count);
            else if (_currentAmmoBagIndex == _healthKitIndexSpawnPoint || _ammoBagIndexSpawnPoint == _healthKitIndexSpawnPoint)
                _healthKitIndexSpawnPoint = Random.Range(0, _spawnPoints.Count);
            else
                _canSpawn = false;
        }
        if (_healthKit == null)
        {
            _currentHealthKitIndex = _healthKitIndexSpawnPoint;
            _healthKit = Instantiate(_healthKitPrefab, _spawnPoints[_healthKitIndexSpawnPoint].position, Quaternion.identity);
        }
        if (_ammoBag == null)
        {
            _currentAmmoBagIndex = _ammoBagIndexSpawnPoint;
            _ammoBag = Instantiate(_ammoBagPrefab, _spawnPoints[_ammoBagIndexSpawnPoint].position, Quaternion.identity);
        }
    }
}
