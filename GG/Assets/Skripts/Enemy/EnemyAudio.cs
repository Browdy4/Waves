using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private EnemyAttack _enemyAttack;

    [Header("AudioSource")]
    [SerializeField] private AudioSource _audioSource;

    [Header("ChaseAudioClips")]
    [SerializeField] private AudioClip[] _chaseAudioClips;
    [Header("AttackAudioClips")]
    [SerializeField] private AudioClip[] _attackAudioClips;

    private void OnEnable()
    {
        _enemy.OnMovementStarted += PlayChaseAudioClips;
        _enemyAttack.OnAttackStarted += PlayAttackSound;
    }

    private void OnDisable()
    {
        _enemy.OnMovementStarted -= PlayChaseAudioClips;
        _enemyAttack.OnAttackStarted -= PlayAttackSound;
    }

    private void PlayChaseAudioClips()
    {
        int value = Random.Range(0, _chaseAudioClips.Length);
        _audioSource.clip = _chaseAudioClips[value];

        _audioSource.Play();
    }

    private void PlayAttackSound()
    {
        int value = Random.Range(0, _attackAudioClips.Length);
        _audioSource.PlayOneShot(_attackAudioClips[value]);
    }
}
