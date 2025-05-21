using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public TextMeshProUGUI inventoryIntroduce;
    public Image iconImage;

    public static Action gotItem;
    public static Action itemGone;

    private void Start()
    {
        inventoryIntroduce = GetComponentInChildren<TextMeshProUGUI>(true);
        
        inventoryIntroduce.gameObject.SetActive(false);
        
        gotItem += UpdateIcon;
        gotItem += IntroduceHowToUseItem;

        itemGone += DeleteIcon;
        itemGone += HideIntroduceText;
    }
    
    public void UpdateIcon()
    {
        iconImage.sprite = Player.Instance.playerItem.playerItemData.itemIcon;
    }

    public void DeleteIcon()
    {
        iconImage.sprite = null;
    }
    
    public void IntroduceHowToUseItem()
    {
        inventoryIntroduce.gameObject.SetActive(true);
    }

    public void HideIntroduceText()
    {
        inventoryIntroduce.gameObject.SetActive(false);
    }
}
