using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConsumableType
{
    SpeedUp,
    DoubleJump,
    Invincibility
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string itemName;
    public string itemDescription;
    public ConsumableType itemType;
    public Sprite itemIcon;
    public GameObject itemPrefab;
}
