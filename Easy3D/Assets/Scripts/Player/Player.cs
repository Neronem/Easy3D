using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 각 Player클래스들 전부 참조 (다른곳에서 쉽게 참조하기 위해 묶음)
    public PlayerMovement playerMovement;
    public PlayerCondition playerCondition;
    public PlayerItem playerItem;

    private void Awake()
    {
        CharacterManager.Instance.Player = this; // CharacterManager의 Player 프로퍼티에 자신을 넣음
        playerMovement = GetComponent<PlayerMovement>();
        playerCondition = GetComponent<PlayerCondition>();
        playerItem = GetComponent<PlayerItem>();
    }
}
