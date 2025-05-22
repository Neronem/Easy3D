using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerCondition playerCondition;
    public PlayerItem playerItem;

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        playerMovement = GetComponent<PlayerMovement>();
        playerCondition = GetComponent<PlayerCondition>();
        playerItem = GetComponent<PlayerItem>();
    }
}
