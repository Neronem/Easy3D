using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class RestartUI : MonoBehaviour
{
    public GameObject RestartIntroduceUI; // "R키를 눌러 재시작" 안내 UI 오브젝트
    public GameObject ReallyRestartUI; // "정말로 재시작 하시겠습니까?" UI 오브젝트

    private void Start()
    {
        RestartIntroduceUI.SetActive(true); // 시작할 때 재시작 안내 UI 표시
        ReallyRestartUI.SetActive(false); // 재시작 확인 UI는 숨김
    }

    void Update()
    {
        if (!CharacterManager.Instance.Player.playerCondition.playerDead) // 플레이어 살아있을 때만 동작 (죽었을 때의 재시작과 충돌 방지)
        {
            if (Input.GetKey(KeyCode.R) && !ReallyRestartUI.activeInHierarchy) // 재시작 확인 UI 안켜져있을때 R누르면
            {
                RestartUIShowed(); // 재시작 확인 UI 표시
            }

            if (Input.GetKey(KeyCode.Return) && ReallyRestartUI.activeInHierarchy) // 재시작 확인 UI 켜져있을때 엔터누르면
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 게임 재시작
            }

            if (Input.GetKey(KeyCode.Escape) && ReallyRestartUI.activeInHierarchy) // 재시작 확인 UI 켜져있을때 Esc누르면
            {
                RestartUIHide(); // 재시작 확인 UI 숨기고 안내 UI 다시 표시
            }
        }
        else
        {
            return;
        }
    }

    // 재시작 실수 방지 확인 UI
    private void RestartUIShowed()
    {
        RestartIntroduceUI.SetActive(false); // 안내 UI 숨김
        ReallyRestartUI.SetActive(true); // 확인 UI 표시
    }

    // 재시작 실수 방지 확인 UI에서 Esc 누를 시
    private void RestartUIHide()
    {
        RestartIntroduceUI.SetActive(true); // 안내 UI 다시 표시
        ReallyRestartUI.SetActive(false); // 확인 UI 숨김
    } 
}