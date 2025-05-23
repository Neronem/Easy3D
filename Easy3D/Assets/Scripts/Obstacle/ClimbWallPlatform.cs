using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbWallPlatform : MonoBehaviour
{   
    public LayerMask playerLayer;
    public int rayCount = 7;
    public float spacing = 1.5f;
    public float rayLength = 2f;
    
    void Update()
    {
        Vector3 rayOrigin = new Vector3(transform.position.x, transform.position.y - (transform.position.y / 2), transform.position.z);

        for (int i = 1; i <= rayCount; i++)
        {
            Vector3 offset = -transform.forward * i * spacing;
            Vector3 origin = rayOrigin + offset;
            
            Ray ray = new Ray(origin, transform.up);
            RaycastHit hit;
            
            Debug.DrawRay(origin, ray.direction * rayLength, Color.red);

            if (Physics.Raycast(ray, out hit, rayLength))
            {
                if (hit.collider.gameObject.layer == playerLayer)
                {
                    CharacterManager.Instance.Player.playerMovement.isClimbing = true;
                }
                else
                {
                    CharacterManager.Instance.Player.playerMovement.isClimbing = false;
                }
            }
            else
            {
                CharacterManager.Instance.Player.playerMovement.isClimbing = false;
            }
            
        }
    }
}
