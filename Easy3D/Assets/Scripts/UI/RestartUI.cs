using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartUI : MonoBehaviour
{
    public GameObject RestartIntroduceUI;
    public GameObject ReallyRestartUI;
    
    private void Start()
    {
        RestartIntroduceUI.SetActive(true);
        ReallyRestartUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!CharacterManager.Instance.Player.playerCondition.playerDead)
        {
            if (Input.GetKey(KeyCode.R) && !ReallyRestartUI.activeInHierarchy)
            {
                RestartUIShowed();
            }

            if (Input.GetKey(KeyCode.Return) && ReallyRestartUI.activeInHierarchy)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            if (Input.GetKey(KeyCode.Escape) && ReallyRestartUI.activeInHierarchy)
            {
                RestartUIHide();
            }
        }
        else
        {
            return;
        }
    }

    private void RestartUIShowed()
    {
        RestartIntroduceUI.SetActive(false);
        ReallyRestartUI.SetActive(true);
    }

    private void RestartUIHide()
    {
        RestartIntroduceUI.SetActive(true);
        ReallyRestartUI.SetActive(false);
    } 
}
