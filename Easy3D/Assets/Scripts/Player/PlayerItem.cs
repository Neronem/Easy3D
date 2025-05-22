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
            Inventory.itemGone.Invoke();
            
            switch (playerItemData.itemType)
            {
                case ConsumableType.SpeedUp:
                    playerItemData = null;
                    StartCoroutine(SpeedUpCoroutine());
                    break;
                case ConsumableType.DoubleJump:
                    playerItemData = null;
                    StartCoroutine(DoubleJumpAbleCoroutine());
                    break;
                case ConsumableType.Invincibility:
                    playerItemData = null;
                    StartCoroutine(InvincibleCoroutine());
                    break;
            }
        }
    }

    public void OnDropItem(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && playerItemData != null)
        {
            Instantiate(playerItemData.itemPrefab, transform.position + (transform.forward * 3f), Quaternion.identity);
            Inventory.itemGone.Invoke();
            playerItemData = null;
        }
    }

    private IEnumerator SpeedUpCoroutine()
    {
        CharacterManager.Instance.Player.playerMovement.moveSpeed += 10f;
        yield return new WaitForSeconds(10f);
        CharacterManager.Instance.Player.playerMovement.moveSpeed -= 10f;
    }

    private IEnumerator DoubleJumpAbleCoroutine()
    {
        CharacterManager.Instance.Player.playerMovement.maxJumpCount = 2;
        yield return new WaitForSeconds(10f);
        CharacterManager.Instance.Player.playerMovement.maxJumpCount = 1;
    }

    private IEnumerator InvincibleCoroutine()
    {
        CharacterManager.Instance.Player.playerCondition.invincible = true;
        yield return new WaitForSeconds(10f);
        CharacterManager.Instance.Player.playerCondition.invincible = false;
    }
}
