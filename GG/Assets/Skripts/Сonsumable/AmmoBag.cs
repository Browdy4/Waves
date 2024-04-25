using System;
using UnityEngine;

public class AmmoBag : MonoBehaviour
{
    [Header("Sound")]
    [SerializeField] private AudioClip _audioClip;

    private Player _player;
    private AudioSource _audioSource;

    public static event Action OnAmmoSelected;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _audioSource = _player.GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == _player.gameObject.layer)
        {
            _audioSource.PlayOneShot(_audioClip);

            gameObject.SetActive(false);

            ApplyAmmo();
        }
    }

    private void ApplyAmmo()
    {
        OnAmmoSelected?.Invoke();

        Destroy(gameObject, _audioClip.length);
    }
}
