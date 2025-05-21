using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player instance;
    public PlayerMovement playerMovement;
    public PlayerCondition playerCondition;
    
    public static Player Instance
    {
        get { return instance; }
    }

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        playerMovement = GetComponent<PlayerMovement>();
        playerCondition = GetComponent<PlayerCondition>();
    }
    
    
}
