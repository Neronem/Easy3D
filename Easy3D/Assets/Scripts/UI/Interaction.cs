using System;
using System.Collections;
using System.Collections.Generic;
using TMPro; 
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    public TextMeshProUGUI promptText; // 커서가 오브젝트 위에 올라갔을 때 옵젝 정보를 담는 텍스트
    public TextMeshProUGUI alreadyHaveText; // 아이템을 이미 가지고 있을 때 출력할 경고 메시지

    private IShowInfoByRayCast showInfoByRayCast; // 마우스 오버 시 텍스트 정보를 제공하는 인터페이스
    private IInteractable interactable; // 상호작용 기능을 제공하는 인터페이스

    private Camera _camera; // 카메라 참조 (중앙에서 Ray를 쏘기 위함)

    private Coroutine alreadyHaveCoroutine; // 이미 보유 중 메시지를 출력 중인지 추적하는 코루틴 변수

    void Start()
    {
        _camera = Camera.main; // 메인 카메라 가져오기
        promptText.text = string.Empty; // 시작 시 안내 텍스트 초기화
        alreadyHaveText.text = string.Empty; // 경고 메시지도 초기화
    }

    void Update()
    {
        // 화면 중앙에서 Ray를 발사
        Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        // Ray가 어떤 오브젝트에 맞았고, 그 거리가 8 이하일 때
        if (Physics.Raycast(ray, out hit, 8f))
        {
            // 맞은 오브젝트에 IShowInfoByRayCast 인터페이스가 있다면
            if (hit.collider.gameObject.TryGetComponent(out showInfoByRayCast))
            {
                // 텍스트 표시
                promptText.text = showInfoByRayCast.GetText();

                // 동시에 IInteractable 인터페이스도 있는지 확인해서 저장
                hit.collider.gameObject.TryGetComponent(out interactable);
            }
            else
            {
                // 없으면 전부 초기화
                showInfoByRayCast = null;
                interactable = null;
                promptText.text = string.Empty;
            }
        }
        else
        {
            // Ray가 아무것도 안 맞았을 경우도 초기화
            interactable = null;
            showInfoByRayCast = null;
            promptText.text = string.Empty;
        }
    }

    // 상호작용 입력 처리 (Input System)
    public void OnInteractInput(InputAction.CallbackContext context)
    {
        // 키가 눌렸고, interactable이 존재하면
        if (context.phase == InputActionPhase.Started && interactable != null)
        {
            // 플레이어가 아이템을 가지고 있지 않다면
            if (CharacterManager.Instance.Player.playerItem.playerItemData == null)
            {
                // 상호작용 실행
                interactable.OnInteract();
            }
            else
            {
                // 아이템을 이미 가지고 있으면 경고 메시지를 출력
                if (alreadyHaveCoroutine != null)
                {
                    StopCoroutine(alreadyHaveCoroutine); // 기존 출력 중이던 코루틴 종료
                }

                alreadyHaveCoroutine = StartCoroutine(ShowAlreadyHaveMessage()); // 새로 코루틴 시작
            }
        }
    }

    // "이미 보유 중입니다" 메시지를 잠깐 보여주는 코루틴
    private IEnumerator ShowAlreadyHaveMessage()
    {
        alreadyHaveText.text = "이미 보유중인 아이템이 있습니다!"; // 메시지 출력
        yield return new WaitForSeconds(2f); // 2초 대기
        alreadyHaveText.text = String.Empty; // 텍스트 비우기
        alreadyHaveCoroutine = null; // 코루틴 변수 초기화
    }
}
