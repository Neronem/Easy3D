using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerItem : MonoBehaviour
{
    public ItemData playerItemData = null; // 플레이어가 현 소유중인 아이템의 데이터

    public void OnUseItem(InputAction.CallbackContext context)  // 아이템 사용 입력 처리 (Input System)
    {
        if (context.phase == InputActionPhase.Started && playerItemData != null) // 상호작용 키 입력시
        {
            playerItemData.Use(); // 아이템 사용
            Inventory.instance.TriggerItemGone(); // 인벤토리에서 아이템 사라지는 이벤트 호출
            playerItemData = null; // 사용했으므로 비워놓기
        }
    }

    public void OnDropItem(InputAction.CallbackContext context)  // 아이템 버리기 입력 처리 (Input System)
    {
        if (context.phase == InputActionPhase.Started && playerItemData != null) // 상호작용 키 입력시
        {
            Instantiate(playerItemData.itemPrefab, transform.position + (transform.forward * 3f), Quaternion.identity); // 플레이어 앞에 아이템프리펩 생성
            Inventory.instance.TriggerItemGone(); // 인벤토리에서 아이템 사라지는 이벤트 호출
            playerItemData = null; // 비워놓기
        }
    }

}
