using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Enemy List")]
    [SerializeField] private int[] _enemiesWave;

    [Header("Values")]
    [SerializeField] private float _waveChangeDelay;
    [SerializeField] private float _spawnEnemyDelay;

    [Header("Scripts")]
    [SerializeField] private OverlapSearchEnemies _overlapSearchEnemies;

    private bool _canChangeWave = true;

    private int _currentWave = -1;

    public event Action OnSpawnStarted;

    public event Action OnWaveStarted;
    public event Action OnWaveEnded;
    public event Action WinGame;

    public static event Action OnHalfEnemiesDead;

    private void Update()
    {
        if (Time.frameCount % 3 == 0)
        {
            if (_overlapSearchEnemies.TryFindEnemy())
            {
                if (_canChangeWave)
                    StartCoroutine(ChangeWave());
            }
        }
    }

    private IEnumerator StartWave()
    {
        for (int enemy = 0; enemy < _enemiesWave[_currentWave]; enemy++)
        {
            if (enemy == _enemiesWave[_currentWave] / 2)
                OnHalfEnemiesDead?.Invoke();

            OnSpawnStarted?.Invoke();
            yield return new WaitForSeconds(_spawnEnemyDelay);
        }
    }

    private IEnumerator ChangeWave()
    {
        OnWaveEnded?.Invoke();

        if (_currentWave > 5)
        {
            WinGame?.Invoke();

            yield break;
        }

        _canChangeWave = false;

        yield return new WaitForSeconds(_waveChangeDelay);

        _currentWave++;

        OnWaveStarted?.Invoke();

        StartCoroutine(StartWave());

        _canChangeWave = true;
    }
}
