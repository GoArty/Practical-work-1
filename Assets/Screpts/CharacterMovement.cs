using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private const float Gravity = 9.8f;
    
    [SerializeField] 
    private float _speed = 5f;
    [SerializeField] 
    private float _rotateSpeed = 10f;
    
    private CharacterController _characterController;
    private Animator _animator;
    private Camera _mainCamera;
    
    protected void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _mainCamera = Camera.main;
    }

    protected void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        var forward = _mainCamera.transform.forward;
        var right = _mainCamera.transform.right;
        forward.y = 0;
        right.y = 0;

        forward = forward.normalized;
        right = right.normalized;

        var rightRelativeInput = horizontalInput * right;
        var forwardRelativeInput = verticalInput * forward;

        var cameraRelativeMovement = forwardRelativeInput + rightRelativeInput;
        
        Rotate(cameraRelativeMovement);
        Move(cameraRelativeMovement);
        Animate(cameraRelativeMovement);
    }
    
    private void Rotate(Vector3 moveDirection)
    {
        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(moveDirection),
                Time.deltaTime * _rotateSpeed);
        }
    }

    private void Move(Vector3 moveDirection)
    {
        moveDirection *= _speed;
        moveDirection.y = -Gravity;
        _characterController.Move(moveDirection * Time.deltaTime);
    }

    private void Animate(Vector3 moveDirection)
    {
        float vertical = Vector3.Dot(moveDirection.normalized, transform.forward);
        float horizontal = Vector3.Dot(moveDirection.normalized, transform.right);

        _animator.SetFloat("Vertical", vertical);
        _animator.SetFloat("Horizontal", horizontal);
    }
}
