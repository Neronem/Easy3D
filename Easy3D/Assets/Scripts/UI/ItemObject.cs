using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour, IShowInfoByRayCast, IInteractable
{
    public ItemData data;

    public string GetText()
    {
        string str = $"{data.itemName} \n {data.itemDescription}";
        return str; 
    }

    public void OnInteract()
    {
        Player.Instance.playerItem.playerItemData = data;
        Inventory.gotItem.Invoke();
        Destroy(gameObject);
    }
}
