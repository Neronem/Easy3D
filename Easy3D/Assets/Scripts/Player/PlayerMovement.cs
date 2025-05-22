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
    private int curJumpCount;
    public int maxJumpCount = 1;
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
    public Rigidbody _rigidbody;

    void Start()
    {
        animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    private void FixedUpdate()
    {
        if (CharacterManager.Instance.Player.playerCondition.playerDead == false)
        {
            Move();
        }
    }

    private void LateUpdate()
    {
        if (CharacterManager.Instance.Player.playerCondition.playerDead == false)
        {
            CameraLook();
        }
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
        if (IsGround())
        {
            curJumpCount = 0;
        }
        
        if (context.phase == InputActionPhase.Performed && curJumpCount < maxJumpCount)
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z); // 현재 y의 가속도 초기화
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
            curJumpCount++;
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
            if (Physics.Raycast(rays[i], 0.05f, groundLayer))
            {
                return true;
            }
        }
        return false;
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.parent = collision.transform;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.parent = null;
        }
    }
}
