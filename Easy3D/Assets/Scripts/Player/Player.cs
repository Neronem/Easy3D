using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player _instance;
    public PlayerMovement playerMovement;
    public PlayerCondition playerCondition;
    public PlayerItem playerItem;
    
    public static Player Instance
    {
        get { return _instance; }
    }

    private void Start()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        playerMovement = GetComponent<PlayerMovement>();
        playerCondition = GetComponent<PlayerCondition>();
        playerItem = GetComponent<PlayerItem>();
    }
}
