using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "InvincibleItem", menuName = "Item/Invincible")]
public class InvincibleItem : ItemData
{
    public float duration = 10f; // 아이템 지속시간
    public override void Use() 
    {
        Player player = CharacterManager.Instance.Player;
        player.StartCoroutine(InvincibleCoroutine(player));
    }
    
    private IEnumerator InvincibleCoroutine(Player player) // 플레이어 무적 효과
    {
        player.playerCondition.invincible = true;
        yield return new WaitForSeconds(duration);
        player.playerCondition.invincible = false;
    }
}
