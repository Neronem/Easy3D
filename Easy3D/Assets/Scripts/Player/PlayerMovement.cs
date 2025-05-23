using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed; // 이동 속도
    public float jumpForce; // 점프 힘
    private int curJumpCount; // 현재 점프 횟수
    public int maxJumpCount = 1; // 최대 점프 횟수
    private Vector2 curMovement; // 현재 이동 입력값
    public LayerMask groundLayer; // 바닥 판정에 사용할 레이어

    [Header("Look")] 
    public Transform cameraContainer; // 카메라가 부착된 트랜스폼
    public float minXLook; // 아래로 볼 수 있는 최소 각도
    public float maxXLook; // 위로 볼 수 있는 최대 각도
    private float camCurXRot; // 현재 카메라 X축 회전 값
    public float lookSensitivity; // 마우스 감도
    private Vector2 mouseDelta; // 마우스 이동값

    private Animator animator; // 애니메이터 컴포넌트
    public Rigidbody _rigidbody; // 리지드바디 컴포넌트

    [Header("Wall Climbing")] 
    public Quaternion playerRotation; // 벽 타기용 초기 회전 저장
    public float wallCheckDistance = 2f; // 벽 감지 거리
    public float climbSpeed = 0.2f; // 벽 타기 속도
    public LayerMask climbableLayer; // 벽으로 인식할 레이어
    public bool isClimbing = false; // 현재 벽 타기 중인지 여부

    void Start()
    {
        animator = GetComponent<Animator>(); // 애니메이터 가져오기
        _rigidbody = GetComponent<Rigidbody>(); // 리지드바디 가져오기
        Cursor.lockState = CursorLockMode.Locked; // 마우스 커서 숨기고 고정
        playerRotation = transform.rotation; // 초기 회전 저장
    }

    void FixedUpdate()
    {
        if (CharacterManager.Instance.Player.playerCondition.playerDead == false)
        {
            if (isClimbing)
            {
                WallClimb(); // 벽 타기 상태이면 타기 로직 실행
            }
            else
            {
                Move(); // 일반 이동
            }
        }
    }

    private void LateUpdate() // 카메라 회전은 Update 다 일어나고 처리
    {
        // 플레이어가 살아있을 때만 카메라 회전 처리
        if (CharacterManager.Instance.Player.playerCondition.playerDead == false)
        {
            CameraLook();
        }
    }

    // 이동 처리
    void Move()
    {
        Vector3 dir = transform.forward * curMovement.y + transform.right * curMovement.x; // 입력 방향 계산
        dir *= moveSpeed; // 이동 속도 반영
        dir.y = _rigidbody.velocity.y; // 현재 y속도 유지 (중력 등)

        _rigidbody.velocity = dir; // 이동 적용
    }

    // 카메라 회전 처리
    void CameraLook()
    {
        // X축(상하) 회전 값 계산 및 제한
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0); // 상하 회전 적용

        // Y축(좌우) 회전
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    // 이동 입력 처리 (Input System)
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovement = context.ReadValue<Vector2>(); // 이동 입력 저장

            // 방향에 따라 애니메이션 파라미터 변경
            animator.SetBool("MoveForward", curMovement.y > 0);
            animator.SetBool("MoveBackward", curMovement.y < 0);
            animator.SetBool("MoveLeftOrRight", curMovement.y == 0 && curMovement.x != 0);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            // 이동이 끝나면 입력 초기화 및 애니메이션 리셋
            curMovement = Vector2.zero;
            animator.SetBool("MoveForward", false);
            animator.SetBool("MoveBackward", false);
            animator.SetBool("MoveLeftOrRight", false);
        }
    }

    // 마우스 입력 처리 (Input System)
    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>(); // 마우스 이동값 저장
    }

    // 점프 입력 처리 (Input System)
    public void OnJump(InputAction.CallbackContext context)
    {
        // 바닥에 닿았을 경우 점프 카운트 초기화
        if (IsGround())
        {
            curJumpCount = 0;
        }

        // 점프 키를 누르고, 최대 점프 수보다 적을 경우 점프
        if (context.phase == InputActionPhase.Performed && curJumpCount < maxJumpCount)
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z); // 수직 속도 초기화
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode.Impulse); // 점프 힘 적용
            curJumpCount++; // 점프 카운트 증가
        }
    }

    // 바닥에 닿았는지 확인하는 함수 (책상다리 : 네 개의 ray로 확인)
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
                return true; // 하나라도 바닥과 닿으면 true
            }
        }
        return false;
    }

    // 벽 타기 처리
    void WallClimb()
    {
        _rigidbody.velocity = new Vector3(0, 0, 0); // 벽 타는 동안 속도 초기화 (밀리지 않도록)

        // W 키를 누르면 위로 이동
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, climbSpeed, 0);
        }
    }
}
