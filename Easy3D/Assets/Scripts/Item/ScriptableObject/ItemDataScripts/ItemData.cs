using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    [Header("Info")]
    public string itemName; // 아이템 이름
    public string itemDescription; // 아이템 설명
    public Sprite itemIcon; // 아이템 아이콘
    public GameObject itemPrefab; // 프리펩

    public abstract void Use(); // 아이템 효과 자식들이 정의
}