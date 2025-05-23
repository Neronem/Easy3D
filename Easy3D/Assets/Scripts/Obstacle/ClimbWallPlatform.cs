using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbWallPlatform : MonoBehaviour
{   
    public LayerMask playerLayer; // 닿으면 특정 로직 동작시켜야 하는 플레이어 레이어
    public int rayCount = 7; // Ray 갯수
    public float spacing = 1.5f; // Ray끼리의 간격
    public float rayLength = 2f; // Ray 길이
    
    void Update()
    {
        Vector3 rayOrigin = new Vector3(transform.position.x, transform.position.y - (transform.position.y / 2), transform.position.z); // 벽 객체의 제일 아래쪽부터 ray위치 시작

        for (int i = 1; i <= rayCount; i++) // Ray 갯수만큼 생성 시작
        {
            Vector3 offset = -transform.forward * i * spacing; // Ray를 위쪽으로 여러개 생성하기 위해 간격 설정
            Vector3 origin = rayOrigin + offset; // 간격 반영한 위치 설정
            
            Ray ray = new Ray(origin, transform.up); // <- 방향으로 발사하는 Lay 생성 
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, rayLength)) // Ray 감지
            {
                if ((playerLayer.value | 1 << hit.collider.gameObject.layer) == playerLayer.value) // 충돌한 오브젝트의 레이어가 플레이어 레이어 마스크에 포함되는지 검사
                {
                    CharacterManager.Instance.Player.playerMovement.isClimbing = true; // player 상태 Climbing으로 만듬
                }
                else
                {
                    CharacterManager.Instance.Player.playerMovement.isClimbing = false; // 플레이어 레이어 아니면 Climbing 모드 해제
                }
            }
            else
            {
                CharacterManager.Instance.Player.playerMovement.isClimbing = false; // Ray감지 안되면 Climbing 모드 해제
            }
            
        }
    }
}
