using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour, IShowInfoByRayCast
{
    public float jumpForce = 20f; // AddForce할 힘 크기

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player")) // Player와 접촉 시
        {
            Rigidbody _rigidbody = other.gameObject.GetComponent<Rigidbody>(); // rigidbody 가져오고
            
            if (_rigidbody != null)
            {
                _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z); // 기존 속도 초기화 (일정한 점프 위함)
                _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // 위로 강하게 날려보냄
            }
        }
    }

    public string GetText() // 마우스 갖다대는 상호작용 시 텍스트 반환
    {
        return "점프대 \n 플라잉곰";
    }
}
