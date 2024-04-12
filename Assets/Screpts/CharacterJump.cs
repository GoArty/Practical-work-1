using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJump : MonoBehaviour
{
    private Animator _animator;
    private bool _jump = false;
    
    protected void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    protected void Update()
    {
        var moveInput = new Vector3(-Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));
        
        if (_jump)
        {
            _jump = false;
            _animator.SetBool("Jump", _jump);
        }
        
        if (Input.GetKey(KeyCode.Space) && moveInput == Vector3.zero)
        {
            _jump = true;
            _animator.SetBool("Jump", _jump);
        }
    }
}
