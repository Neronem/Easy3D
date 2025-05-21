using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour, IShowInfoByRayCast
{
    public float jumpForce = 20f;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody _rigidbody = other.gameObject.GetComponent<Rigidbody>();
            
            if (_rigidbody != null)
            {
                _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z); // 기존 속도 초기화 (일정한 점프 위함)
                _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }

    public string GetText()
    {
        return "점프대 \n 플라잉곰";
    }
}
