using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    
    public TextMeshProUGUI inventoryIntroduce;
    public Image iconImage;

    public event Action gotItem;
    public event Action itemGone;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        
        inventoryIntroduce = GetComponentInChildren<TextMeshProUGUI>(true);
        inventoryIntroduce.gameObject.SetActive(false);

        gotItem += UpdateIcon;
        gotItem += IntroduceHowToUseItem;
        itemGone += DeleteIcon;
        itemGone += HideIntroduceText;
    }

    public void UpdateIcon()
    {
        iconImage.sprite = CharacterManager.Instance.Player.playerItem.playerItemData.itemIcon;
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
    
    // 외부 호출용 트리거 함수
    public void TriggerGotItem()
    {
        gotItem?.Invoke();
    }

    public void TriggerItemGone()
    {
        itemGone?.Invoke();
    }
    
    private void OnDestroy()
    {
        gotItem -= UpdateIcon;
        gotItem -= IntroduceHowToUseItem;
        itemGone -= DeleteIcon;
        itemGone -= HideIntroduceText;
    }
}