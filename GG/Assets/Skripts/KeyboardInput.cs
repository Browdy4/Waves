using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    [SerializeField] private CharacterMovement _movement;
    private Animator _anim;
    private bool _reload;

    private void Start()
    {
        _anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            _anim.SetTrigger("reload1");
        }
    }
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        _anim.SetFloat("vertical", Mathf.Abs(vertical));
        _anim.SetFloat("horizontal", Mathf.Abs(horizontal));

        _movement.Move(new Vector3(horizontal, 0, vertical));
        _movement.Rotate(new Vector3(horizontal, 0, vertical));
    }
}
