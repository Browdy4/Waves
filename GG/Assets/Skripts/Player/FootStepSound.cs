using System.Collections.Generic;
using UnityEngine;

public class FootStepSound : MonoBehaviour
{
    [SerializeField] private KeyboardInput _keyboardInput;

    [Header("Audio")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<AudioClip> _grassAudioClips;
    [SerializeField] private List<AudioClip> _rockAudioClips;

    [Header("AudioPitch")]
    [SerializeField] private float _minPitch;
    [SerializeField] private float _maxPitch;

    [Header("Volume")]
    [SerializeField] private float _volumeFootStepGrassSound;
    [SerializeField] private float _volumeFootStepStoneSound;

    [SerializeField] private float _stepRateRun;
    [SerializeField] private float _stepRateAim;

    private float _stepRate;
    private float _stepCoolDown;

    private void OnEnable()
    {
        _keyboardInput.OnAimStarted += ChangeStepRateAim;
        _keyboardInput.OnAimEnded += ChangeStepRateRun;
    }

    private void OnDisable()
    {
        _keyboardInput.OnAimStarted -= ChangeStepRateAim;
        _keyboardInput.OnAimEnded -= ChangeStepRateRun;
    }

    private void Awake()
    {
        _stepRate = _stepRateRun;
    }

    private void Update()
    {
        _stepCoolDown -= Time.deltaTime;

        if((Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f) && _stepCoolDown <= 0f)
        {
            _audioSource.pitch = Random.Range(_minPitch, _maxPitch);
            PlayFootStepSound();
            _stepCoolDown = _stepRate;
        }
    }

    private void PlayFootStepSound()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo, 6f))
        {
            if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                _audioSource.volume = _volumeFootStepGrassSound;
                _audioSource.PlayOneShot(_grassAudioClips[Random.Range(0,_grassAudioClips.Count)]);
            }
            if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("StoneSurface"))
            {
                _audioSource.volume = _volumeFootStepStoneSound;
                _audioSource.PlayOneShot(_rockAudioClips[Random.Range(0, _rockAudioClips.Count)]);
            }
        }
    }

    private void ChangeStepRateRun()
    {
        _stepRate = _stepRateRun;
    }

    private void ChangeStepRateAim()
    {
        _stepRate = _stepRateAim;
    }
}
