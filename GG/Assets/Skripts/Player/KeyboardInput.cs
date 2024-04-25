using System;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    [SerializeField] private CharacterMovement _movement;

    [SerializeField] private Animator _animator;
    private Vector3 _movementDir;

    private bool _isPaused => Time.timeScale == 0f;

    public event Action OnReloadStarted;
    public event Action OnAimStarted;
    public event Action OnAimEnded;
    public event Action OnThrowGrenade;

    private void Update()
    {
        if (_isPaused)
            return;

        Movement();

        if (Input.GetMouseButton(1))
        {
            _movement.moveSpeed = _movement.minSpeed;
            OnAimStarted?.Invoke();
        }
        if (Input.GetMouseButtonUp(1))
        {
            _movement.moveSpeed = _movement.maxSpeed;
            OnAimEnded?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.R))
            OnReloadStarted?.Invoke();
        if(Input.GetKeyDown(KeyCode.G))
        {
            OnThrowGrenade?.Invoke();
        }
    }

    private void Movement()
    {
        _movementDir = _movement.RotateBody();

        _movement.Move(_movementDir);
        _movement.MouseRotate();

        float hor = Vector3.Dot(_movementDir, transform.right);
        float ver = Vector3.Dot(_movementDir, transform.forward);

        _animator.SetFloat("horizontal", hor);
        _animator.SetFloat("vertical", ver);
    }
}
