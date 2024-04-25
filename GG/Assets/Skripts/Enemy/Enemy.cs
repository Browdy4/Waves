using System;
using UnityEngine;
using UnityEngine.AI;
using NTC.Pool;

public class Enemy : MonoBehaviour, IDamageable, IPoolable
{
    [Header("Rigidbody")]
    [SerializeField] private Rigidbody _rigidbody;
    
    [Header("Scripts")]
    [SerializeField] private EnemyConfig enemyConfig;
    [SerializeField] private RagdollOperations _ragdollOperations;
    [SerializeField] private EnemyAttack _enemyAttack;

    [Header("Health")]
    [SerializeField] private float _maxHealth;

    [Header("EnemyType")]
    [SerializeField] private EnemyType _enemyType;

    private float _defaultEnemySpeed = 12;
    private float _highEnemySpeed = 14;
    private float _slowEnemySpeed = 8;

    public float DefaultEnemySpeed => _defaultEnemySpeed;
    public float HighEnemySpeed => _highEnemySpeed;
    public float SlowEnemySpeed => _slowEnemySpeed;

    public EnemyType EnemyType => _enemyType;
    private float _destroyEnemyDelay => enemyConfig.DestroyEnemyDelay;

    private bool _isPaused => Time.timeScale == 0f;

    private NavMeshAgent _agent;
    private SkinnedMeshRenderer _skinnedMeshRenderer;
    private Transform _player;
    private CapsuleCollider _capsuleCollider;
    private float _currentHealth;
    private float _force = 40f;
    private Animator _animator;

    private float elapsed = 0f;
    private NavMeshHit hit;

    public event Action OnMovementStarted;
    public void OnSpawn()
    {
        OnMovementStarted?.Invoke();
    }

    public void OnDespawn()
    {
        DespawnAction();
    }

    void Start()
    {
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _animator = GetComponent<Animator>();
        _player = FindObjectOfType<Player>().transform;
        _skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        _agent = GetComponent<NavMeshAgent>();
        _currentHealth = _maxHealth;

        _agent.enabled = true;
    }

    public void ApplyDamage(float damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            Death();
        }
    }

    private void Update()
    {
        if (_isPaused)
            return;

        elapsed += Time.deltaTime;
        if (elapsed >= 0.2f) 
        {
            elapsed = elapsed % 0.1f;

            _agent.SetDestination(_player.position);

            //if (IsNearEdge())
            //{
            //    _rigidbody.isKinematic = true;
            //    _rigidbody.useGravity = false;
            //}
            //else
            //{
            //    _rigidbody.isKinematic = false;
            //    _rigidbody.useGravity = true;
            //}
        }
    }

    private bool IsNearEdge()
    {
        if (NavMesh.SamplePosition(transform.position, out hit, 1f, NavMesh.AllAreas))
        {
            Vector3 closestPoint = hit.position;
            float distanceToEdge = Vector3.Distance(transform.position, closestPoint);
            return distanceToEdge < 10f;
        }
        return false;
    }
    private void Death()
    {
        DeathAction();

        NightPool.Despawn(gameObject, _destroyEnemyDelay);
    }

    private void DeathAction()
    {
        Vector3 direction = transform.position - _player.position;

        _animator.enabled = false;
        _capsuleCollider.enabled = false;
        _ragdollOperations.EnableRagdoll();
        _skinnedMeshRenderer.updateWhenOffscreen = true;
        _agent.isStopped = true;
        _enemyAttack.enabled = false;

        _ragdollOperations.AddForce(direction.normalized, _force);
    }

    private void DespawnAction()
    {
        _agent.isStopped = false;
        _currentHealth = _maxHealth;
        _animator.enabled = true;
        _capsuleCollider.enabled = true;
        _ragdollOperations.DisableRagdoll();
        _skinnedMeshRenderer.updateWhenOffscreen = false;
        _enemyAttack.enabled = true;
    }
}
