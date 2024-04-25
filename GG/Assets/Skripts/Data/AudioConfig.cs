using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAudioConfig", menuName = "WeaponConfig/AudioConfig")]
public class AudioConfig : ScriptableObject
{
    public AudioClip WeaponShot;
    public AudioClip WeaponClipEmpty;

    [Min(0)] public float VolumeAttackSound;
    [Min(0)] public float VolumeEmptyClipSound;
    [Min(0)] public float MinPitch;
    [Min(0)] public float MaxPitch;
}
