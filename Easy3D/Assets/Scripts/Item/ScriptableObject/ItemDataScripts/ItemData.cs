using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    [Header("Info")]
    public string itemName;
    public string itemDescription;
    public Sprite itemIcon;
    public GameObject itemPrefab;

    public abstract void Use();
}