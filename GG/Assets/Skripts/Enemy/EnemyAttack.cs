using System;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private EnemyAttackConfig _enemyAttackConfig;
    [SerializeField] private OverlapAttack _overlapAttack;

    private float _distanceToTarget;

    private bool _inAttack = false;
    private Transform _player;

    private int intervalUpdate => _enemyAttackConfig.IntervalUpdate;
    private float _delayPrepareAttack => _enemyAttackConfig.DelayPrepareAttack;
    private float _cooldownAttack => _enemyAttackConfig.CoolDownAttack;
    private float _maxAttackDistantion => _enemyAttackConfig.MaxAttackDistantion;

    private bool _isPaused => Time.timeScale == 0f;

    public event Action OnAttackStarted;
    void Start()
    {
        _player = FindObjectOfType<Player>().transform;
    }

    private void Update()
    {
        if (Time.frameCount % intervalUpdate == 0)
        {
            if (_isPaused)
                return;

            _distanceToTarget = Vector3.Distance(transform.position, _player.position);

            if (_distanceToTarget < _maxAttackDistantion)
            {
                StartAttack();
            }
        }
    }

    private async void StartAttack()
    {
        if (_inAttack) return;

        AttackStarted();

        await PrepareAttack();

        _overlapAttack.PerformAttack();

        await CoolDownAttack();
    }

    private void AttackStarted()
    {
        OnAttackStarted?.Invoke();
    }

    private async Task PrepareAttack()
    {
        _inAttack = true;
        await Task.Delay(TimeSpan.FromSeconds(_delayPrepareAttack));
        Debug.Log("StartAttcak");
    }

    private async Task CoolDownAttack()
    {
        await Task.Delay(TimeSpan.FromSeconds(_cooldownAttack));
        _inAttack = false;
    }
}
