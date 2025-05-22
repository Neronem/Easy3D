using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrap : MonoBehaviour
{
    public Transform rayOrigin;
    public float rayLength = 3000f;
    public LayerMask playerLayer;

    public float damage = 50f;

    private Vector3 endPosition;
    private LineRenderer lineRenderer;
    
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
        Ray ray = new Ray(rayOrigin.position, rayOrigin.forward);
        RaycastHit hit;
        
        endPosition = rayOrigin.position + (rayOrigin.forward * rayLength);

        if (Physics.Raycast(ray, out hit, rayLength, playerLayer))
        {
            Debug.Log("Laser Hit!");
            CharacterManager.Instance.Player.playerCondition.Die();
            endPosition = hit.point;
        }
        
        lineRenderer.SetPosition(0, rayOrigin.position);
        lineRenderer.SetPosition(1, endPosition);
    }
}
