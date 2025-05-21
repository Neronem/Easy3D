using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float curValue;
    public float startValue;
    public float maxValue;

    public Image uiBar;

    private void Start()
    {
        curValue = startValue;
    }

    private void Update()
    {
        uiBar.fillAmount = curValue / maxValue;
    }

    public void Add(float value)
    {
        curValue = Mathf.Min(Mathf.Max(curValue + value, 0), maxValue);
    }
}
