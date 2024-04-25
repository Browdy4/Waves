using System;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [Header("Health")]
    [SerializeField,Min(0f)] private float _maxHealth;

    private float _currentHealth;

    public float MaxHealth => _maxHealth;
    public float CurrentHealth => _currentHealth;

    public static event Action<float> OnUpdatePlayerHealth;

    public event Action OnPlayerDeath;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    private void Update()
    {
        //]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]

        if(Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("4343433434");
            ApplyDamage(50f);
        }
    }

    public void ApplyDamage(float damage)
    {
        if(damage >= 0)
        {
            _currentHealth -= damage;

            Debug.Log(_currentHealth + "осталось здоровья");

            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                OnPlayerDeath?.Invoke();
            }
            UpdatePlayerHealth();
        }
    }

    public void ApplyHealth(float health)
    {
        if(health >= 0)
        {
            if (_currentHealth < _maxHealth)
            {
                _currentHealth += health;

                if (_currentHealth > _maxHealth)
                    _currentHealth = _maxHealth;

                UpdatePlayerHealth();

                Debug.Log(_currentHealth + "осталось здоровья");
            }
        }
    }

    private void UpdatePlayerHealth()
    {
        OnUpdatePlayerHealth?.Invoke(_currentHealth);
    }
}
