using UnityEngine;

public class HealthKit : MonoBehaviour
{
    [Header("Common")]
    [SerializeField] private float _healthToGive;

    [Header("Sound")]
    [SerializeField] private AudioClip _audioClip;

    private AudioSource _audioSource;
    private Player _player;

    private void Start()
    {
        _player = FindObjectOfType<Player>(); 
        _audioSource = _player.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.layer == _player.gameObject.layer)
        {
            Debug.Log("fdsfdfs");
            if(_player.CurrentHealth < _player.MaxHealth)
            {
                _audioSource.PlayOneShot(_audioClip);

                gameObject.SetActive(false);

                GiveHealth();
            }    
        }
    }
    private void GiveHealth()
    {
        _player.ApplyHealth(_healthToGive);

        Destroy(gameObject, _audioClip.length);
    }
}
