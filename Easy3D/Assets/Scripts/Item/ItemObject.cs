using UnityEngine;

public class ItemObject : MonoBehaviour, IShowInfoByRayCast, IInteractable
{
    public ItemData data;

    public string GetText() // 상호작용 시 띄울 테스트 반환
    {
        return $"{data.itemName} \n {data.itemDescription}"; // 아이템 이름이랑 설명 반환
    }

    public void OnInteract() // E키로 상호작용 할 시 동작 실행 (아이템 획득)
    {
        CharacterManager.Instance.Player.playerItem.playerItemData = data; // playerItemData에 자신의 데이터를 넣어줌
        
        Inventory.instance.TriggerGotItem();  // 아이템 획득 시의 이벤트 호출
        Destroy(gameObject); // 맵에있던 아이템은 삭제
    }
}