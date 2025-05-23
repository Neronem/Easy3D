using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrap : MonoBehaviour
{
    public Transform rayOrigin; // Ray발사 위치
    public float rayLength = 3000f; // Ray 길이 
    public LayerMask playerLayer; // 닿으면 특정 로직 동작시켜야 하는 플레이어 레이어

    private Vector3 endPosition; // 레이저와 플레이어가 접촉 시 LineRenderer가 플레이어를 뚫고 지나가지 않음
    private LineRenderer lineRenderer; // 레이저 시각화 LineRenderer 컴포넌트
    
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        // 기본 스타일 설정
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // 간단한 머티리얼
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
    }

    private void Update()
    {
        Ray ray = new Ray(rayOrigin.position, rayOrigin.forward); // Ray 생성
        RaycastHit hit;
        
        endPosition = rayOrigin.position + (rayOrigin.forward * rayLength); // Ray 끝 위치 미리 담아놓음
        
        if (Physics.Raycast(ray, out hit, rayLength, playerLayer)) // 플레이어와 충돌 시
        {
            CharacterManager.Instance.Player.playerCondition.Die(); // 플레이어 사망 메소드 호출
            endPosition = hit.point; // LineRenderer 플레이어와 충돌한 부분까지만 렌더링
        }
        
        // LineRenderer 활성화
        lineRenderer.SetPosition(0, rayOrigin.position); 
        lineRenderer.SetPosition(1, endPosition);
    }
}
