using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 2f;
    public float distance = 3f;

    private Vector3 startPos;
    private Vector3 lastPos;

    // 플레이어가 올라갔는지 체크할 플래그
    public bool PlayerOnPlatform = false;
    public GameObject player;
    void Start()
    {
        startPos = transform.position;
        lastPos = startPos;
    }

    void Update()
    {
        float x = Mathf.PingPong(Time.time * speed, distance * 2) - distance;
        Vector3 newPos = startPos + new Vector3(x, 0f, 0f);
        transform.position = newPos;

        Vector3 delta = transform.position - lastPos;
        
        if (PlayerOnPlatform)
        {
            player.transform.position += delta;
        }
        lastPos = transform.position;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerOnPlatform = true;
            player = collision.gameObject;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerOnPlatform = false;
            player = null;
        }
    }
}
