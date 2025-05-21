using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public TextMeshProUGUI promptText;
    private IShowInfoByRayCast showInfoByRayCast;
    private Camera camera;
    void Start()
    {
        camera = Camera.main;
    }

    void Update()
    {
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * 2f, Color.red);
        
        if (Physics.Raycast(ray, out hit, 8f))
        {
            if (hit.collider.gameObject.TryGetComponent(out showInfoByRayCast))
            {
                promptText.text = showInfoByRayCast.GetText();
            }
            else
            {
                promptText.text = string.Empty;
            }
        }
        else
        {
            promptText.text = string.Empty;
        }
    }
}
