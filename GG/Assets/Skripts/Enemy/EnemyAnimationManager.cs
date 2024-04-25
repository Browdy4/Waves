using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimationManager : MonoBehaviour
{
    private Animator _animator;
    private EnemyAttack _enemyAttack;
    private Enemy _enemy;
    private NavMeshAgent _agent;

    private int _maxMovementAnimations = 6;
    private int _maxAttackanimations = 3;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemyAttack = GetComponent<EnemyAttack>();
        _enemy = GetComponent<Enemy>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        _enemyAttack.OnAttackStarted += AttackAnimationStart;
        _enemy.OnMovementStarted += MovementAnimationStart;
    }

    private void OnDisable()
    {
        _enemyAttack.OnAttackStarted -= AttackAnimationStart;
        _enemy.OnMovementStarted -= MovementAnimationStart;
    }

    private void AttackAnimationStart()
    {
        _animator.SetTrigger("Attack");
        _animator.SetInteger("AttackIndex",Random.Range(0,_maxAttackanimations));
    }

    private void MovementAnimationStart()
    {
        if(_enemy.EnemyType == EnemyType.Runner)
        {
            _agent.speed = _enemy.HighEnemySpeed;

            _animator.SetInteger("MovementIndex", Random.Range(0, 3));

            return;
        }
        else if(_enemy.EnemyType == EnemyType.Slow)
        {
            _agent.speed = _enemy.SlowEnemySpeed;

            _animator.SetInteger("MovementIndex", Random.Range(3, 6));

            return;
        }
        else
        {
            int movementIndex = Random.Range(1, _maxMovementAnimations);

            if (movementIndex >= 3)
                _agent.speed = _enemy.SlowEnemySpeed;
            else
                _agent.speed = _enemy.DefaultEnemySpeed;

            _animator.SetInteger("MovementIndex", movementIndex);
        }
    }
}
