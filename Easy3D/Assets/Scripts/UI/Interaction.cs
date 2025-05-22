using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    public TextMeshProUGUI promptText;
    public TextMeshProUGUI alreadyHaveText;
    
    private IShowInfoByRayCast showInfoByRayCast; // 보유한 정보 텍스트가 있는지?
    private IInteractable interactable; // 보유한 상호작용 동작이 있는지?
    
    private Camera camera;
    
    private Coroutine alreadyHaveCoroutine;
    
    void Start()
    {
        camera = Camera.main;
        promptText.text = string.Empty;
        alreadyHaveText.text = string.Empty;
    }

    void Update()
    {
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 8f))
        {
            if (hit.collider.gameObject.TryGetComponent(out showInfoByRayCast)) // 정보출력하는 인터페이스가 있다면
            {
                promptText.text = showInfoByRayCast.GetText(); // 출력해주기
                hit.collider.gameObject.TryGetComponent(out interactable); // 만약 상호작용까지 가능한 오브젝트라면, 필드에 넣어놓기
            }
            else
            {
                showInfoByRayCast = null;
                interactable = null;
                promptText.text = string.Empty;
            }
        }
        else
        {
            interactable = null;
            showInfoByRayCast = null;
            promptText.text = string.Empty;
        }
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && interactable != null)
        {
            if (CharacterManager.Instance.Player.playerItem.playerItemData == null)
            {
                interactable.OnInteract();
            }
            else
            {
                if (alreadyHaveCoroutine != null)
                {
                    StopCoroutine(alreadyHaveCoroutine);
                }
                
                alreadyHaveCoroutine = StartCoroutine(ShowAlreadyHaveMessage());
            }
        }
    }

    private IEnumerator ShowAlreadyHaveMessage()
    {
        alreadyHaveText.text = "이미 보유중인 아이템이 있습니다!";
        yield return new WaitForSeconds(2f);
        alreadyHaveText.text = String.Empty;
        alreadyHaveCoroutine = null;
    }
}
