using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 2f;              // 플랫폼이 이동하는 속도
    public float distance = 3f;           // 이동할 거리의 절반 (총 이동 범위는 distance * 2)

    private Vector3 startPos;             // 시작 위치를 저장
    private Vector3 lastPos;              // 직전 프레임의 위치 (플레이어 이동을 위해 계산)

    public bool PlayerOnPlatform = false; // 플레이어가 플랫폼 위에 있는지 여부
    public GameObject player;             // 플랫폼 위에 있는 플레이어 오브젝트 참조

    void Start()
    {
        startPos = transform.position;    // 시작 위치 저장
        lastPos = startPos;              // 초기값 설정
    }

    void Update()
    {
        // 플랫폼이 PingPong으로 좌우로 움직이도록 위치 계산
        float x = Mathf.PingPong(Time.time * speed, distance * 2) - distance;
        Vector3 newPos = startPos + new Vector3(x, 0f, 0f); // 새로운 위치 계산
        transform.position = newPos;      // 플랫폼 위치 이동

        Vector3 delta = transform.position - lastPos; // 이번 프레임에서 이동한 거리 계산

        if (PlayerOnPlatform)
        {
            // 플레이어가 플랫폼 위에 있으면, 같이 이동시킴
            player.transform.position += delta;
        }

        lastPos = transform.position;     // 마지막 위치 갱신
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        // 플레이어가 플랫폼에 올라탔을 때
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerOnPlatform = true;
            player = collision.gameObject;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // 플레이어가 플랫폼에서 내려갔을 때
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerOnPlatform = false;
            player = null;
        }
    }
}