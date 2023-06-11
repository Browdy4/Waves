using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    [SerializeField] private Transform _targetPos;
    [SerializeField] private float _lerpRate;

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _targetPos.position, Time.fixedDeltaTime * _lerpRate);
    }
}
