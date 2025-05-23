using System;
using TMPro; 
using UnityEngine;
using UnityEngine.UI; 

public class Inventory : MonoBehaviour
{
    public static Inventory instance; // 싱글톤

    public TextMeshProUGUI inventoryIntroduce; // 아이템 사용 방법을 안내 텍스트
    public Image iconImage; // 플레이어가 보유한 아이템 아이콘 이미지

    public event Action gotItem; // 아이템을 얻었을 때 실행되는 이벤트
    public event Action itemGone; // 아이템을 잃었을 때 실행되는 이벤트

    private void Start()
    {
        // 싱글톤
        if (instance == null)
        {
            instance = this;
        }

        // 자식 오브젝트 중 비활성화된 TextMeshProUGUI도 포함해서 찾음
        inventoryIntroduce = GetComponentInChildren<TextMeshProUGUI>(true);
        inventoryIntroduce.gameObject.SetActive(false); // 최초엔 안내 텍스트를 숨김

        // 이벤트에 함수 연결
        gotItem += UpdateIcon; // 아이콘 이미지 갱신
        gotItem += IntroduceHowToUseItem; // 안내 텍스트 표시
        itemGone += DeleteIcon; // 아이콘 제거
        itemGone += HideIntroduceText; // 안내 텍스트 숨김
    }

    public void UpdateIcon()
    {
        iconImage.sprite = CharacterManager.Instance.Player.playerItem.playerItemData.itemIcon; // 아이템 아이콘을 플레이어가 들고 있는 아이템의 아이콘으로 변경
    }

    public void DeleteIcon()
    {
        iconImage.sprite = null; // 아이템 아이콘 제거
    }

    public void IntroduceHowToUseItem()
    {
        inventoryIntroduce.gameObject.SetActive(true); // 아이템 사용 방법 안내 텍스트 표시
    }

    public void HideIntroduceText()
    {
        inventoryIntroduce.gameObject.SetActive(false); // 아이템 사용 방법 안내 텍스트 숨김
    }

    // 외부에서 호출해서 아이템 획득 이벤트 실행
    public void TriggerGotItem()
    {
        gotItem?.Invoke(); // 등록된 모든 함수 실행
    }

    // 외부에서 호출해서 아이템 제거 이벤트 실행
    public void TriggerItemGone()
    {
        itemGone?.Invoke(); // 등록된 모든 함수 실행
    }

    // 오브젝트가 파괴 시
    private void OnDestroy()
    { // 이벤트 등록 해제
        gotItem -= UpdateIcon; 
        gotItem -= IntroduceHowToUseItem;
        itemGone -= DeleteIcon;
        itemGone -= HideIntroduceText;
    }
}
