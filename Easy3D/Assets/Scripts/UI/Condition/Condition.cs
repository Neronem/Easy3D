using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float curValue; // 현재 상태 수치 (예: 현재 체력)
    public float startValue; // 게임 시작 시 초기값 (예: 시작 체력)
    public float maxValue; // 최대 상태 수치 (예: 최대 체력)

    public Image uiBar; // 상태 수치를 시각적으로 표현할 UI 이미지 (게이지 바 등)

    private void Start()
    {
        curValue = startValue; // 시작할 때 현재 수치를 초기값으로 설정
    }

    private void Update()
    {
        uiBar.fillAmount = curValue / maxValue; // 매 프레임마다 상태 UI 바를 업데이트
                                                // fillAmount는 0.0 ~ 1.0 사이의 값으로, 현재 수치를 최대 수치로 나눈 비율을 의미
    }

    // 상태 수치를 증감
    public void ChangeHealth(float value)
    {
        curValue = Mathf.Min(Mathf.Max(curValue + value, 0), maxValue); // 수치를 value 만큼 증가 또는 감소시키되,
                                                                        // 0보다 작아지지 않도록 하고 maxValue를 초과하지 않도록 제한
    }
}