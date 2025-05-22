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

    [Header("Wall Climbing")] 
    public Quaternion playerRotation;
    public float wallCheckDistance = 2f;
    public float climbSpeed = 0.2f;
    public LayerMask climbableLayer;
    private bool isClimbing = false;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        playerRotation = transform.rotation;
    }
    
    void FixedUpdate()
    {
        if (CharacterManager.Instance.Player.playerCondition.playerDead == false)
        {
            CheckWallClimb(); // 벽 타기 검사 추가
            if (!isClimbing)
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

    void CheckWallClimb()
    {
        Vector3 rayOrigin = transform.position + Vector3.up * 1.0f; // 캐릭터의 중심(1미터 위)

        Ray ray;
        if (isClimbing)
        {
            ray = new Ray(rayOrigin, -transform.up);
        }
        else
        {
            ray = new Ray(rayOrigin, transform.forward);
        }

        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * wallCheckDistance, Color.green);
        if (Physics.Raycast(ray, out hit, wallCheckDistance, climbableLayer))
        {
            
            Debug.Log("Wall Climb!");
            isClimbing = true;
            _rigidbody.velocity = new Vector3(0, 0, 0);
            
            Quaternion climbRotation = Quaternion.LookRotation(Vector3.up, hit.normal);
            transform.rotation = Quaternion.Lerp(transform.rotation, climbRotation, 10f * Time.deltaTime);
            
            if (Input.GetKey(KeyCode.W)) // 앞으로 가려는 의지 있을 때만    
            {
                transform.position += new Vector3(0, climbSpeed, 0);
            }
        }
        else
        {
            isClimbing = false;
            _rigidbody.useGravity = true;
        }
    }
}
