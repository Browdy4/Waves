using UnityEngine;

public class WeaponAudio : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private WeaponConfig weaponConfig;
    [SerializeField] private Weapon _weapon;

    [Header("AudioSource")]
    [SerializeField] private AudioSource _audioSource;

    private AudioClip _weaponShot => weaponConfig.AudioConfig.WeaponShot;
    private AudioClip _weaponClipEmpty => weaponConfig.AudioConfig.WeaponClipEmpty;

    private float _volumeAttackSound => weaponConfig.AudioConfig.VolumeAttackSound;
    private float _volumeEmptyClipSound => weaponConfig.AudioConfig.VolumeEmptyClipSound;
    private float _minPitch => weaponConfig.AudioConfig.MinPitch;
    private float _maxPitch => weaponConfig.AudioConfig.MinPitch;
    private void OnEnable()
    {
        Weapon.OnAttackStarted += PlayAttackSound;
        _weapon.OnEmptyClip += PlayEmptyClipSound;
    }

    private void OnDisable()
    {
        Weapon.OnAttackStarted -= PlayAttackSound;
        _weapon.OnEmptyClip -= PlayEmptyClipSound;
    }

    private void PlayAttackSound()
    {
        _audioSource.volume = _volumeAttackSound;
        _audioSource.pitch = Random.Range(_minPitch, _maxPitch);
        _audioSource.PlayOneShot(_weaponShot);
    }

    private void PlayEmptyClipSound()
    {
        _audioSource.volume = _volumeEmptyClipSound;
        _audioSource.PlayOneShot(_weaponClipEmpty);
    }
}
