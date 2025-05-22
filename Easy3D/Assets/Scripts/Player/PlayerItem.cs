using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerItem : MonoBehaviour
{
    public ItemData playerItemData = null;

    public void OnUseItem(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && playerItemData != null)
        {
            playerItemData.Use();
            Inventory.instance.TriggerItemGone();
            playerItemData = null;
        }
    }

    public void OnDropItem(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && playerItemData != null)
        {
            Instantiate(playerItemData.itemPrefab, transform.position + (transform.forward * 3f), Quaternion.identity);
            Inventory.instance.TriggerItemGone();
            playerItemData = null;
        }
    }

}
