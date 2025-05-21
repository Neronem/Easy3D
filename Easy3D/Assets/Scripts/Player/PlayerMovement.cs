using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float jumpForce;
    private Vector2 curMovement;
    public LayerMask groundLayer;
    
    [Header("Look")] 
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;
    private Vector2 mouseDelta;
    
    private Animator animator;
    private Rigidbody _rigidbody;

    void Start()
    {
        animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        CameraLook();
    }

    void Move()
    {
        Vector3 dir = transform.forward * curMovement.y + transform.right * curMovement.x;
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y;
        
        _rigidbody.velocity = dir;
    }

    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);
        
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovement = context.ReadValue<Vector2>();
            
            animator.SetBool("MoveForward", curMovement.y > 0);
            animator.SetBool("MoveBackward", curMovement.y < 0);
            animator.SetBool("MoveLeftOrRight", curMovement.y == 0 && curMovement.x != 0);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovement = Vector2.zero;
            animator.SetBool("MoveForward", false);
            animator.SetBool("MoveBackward", false);
            animator.SetBool("MoveLeftOrRight", false);
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGround())
        {
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
        }
    }

    bool IsGround()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayer))
            {
                return true;
            }
        }
        return false;
    }
}
