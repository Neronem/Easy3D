using UnityEngine;

public class ItemObject : MonoBehaviour, IShowInfoByRayCast, IInteractable
{
    public ItemData data;

    public string GetText()
    {
        return $"{data.itemName} \n {data.itemDescription}";
    }

    public void OnInteract()
    {
        CharacterManager.Instance.Player.playerItem.playerItemData = data;
        
        Inventory.instance.TriggerGotItem();  // static 없이 메서드 호출
        Destroy(gameObject);
    }
}