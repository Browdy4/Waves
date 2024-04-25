using UnityEngine;
using UnityEngine.Audio;

public class AudioSettings : MonoBehaviour
{
    [Header("MasterMixerGroup")]
    [SerializeField] private AudioMixerGroup _masterMixerGroup;
    [Header("EnvironmentMixerGroup")]
    [SerializeField] private AudioMixerGroup _environmentMixerGroup;
    [Header("EnemyMixerGroup")]
    [SerializeField] private AudioMixerGroup _enemyMixerGroup;
    [Header("WeaponMixerGroup")]
    [SerializeField] private AudioMixerGroup _weaponMixerGroup;

    public void ChangeMasterMixerVolume(float volume)
    {
        _masterMixerGroup.audioMixer.SetFloat("MasterVolume", Mathf.Log(volume) * 20);
    }
    public void ChangeEnvironmentMixerVolume(float volume)
    {
        _masterMixerGroup.audioMixer.SetFloat("EnvironmentVolume", Mathf.Log(volume) * 20);
    }
    public void ChangeEnemyMixerVolume(float volume)
    {
        _masterMixerGroup.audioMixer.SetFloat("EnemyVolume", Mathf.Log(volume) * 20);
    }
    public void ChangeWeaponMixerVolume(float volume)
    {
        _masterMixerGroup.audioMixer.SetFloat("WeaponVolume", Mathf.Log(volume) * 20);
    }
}
