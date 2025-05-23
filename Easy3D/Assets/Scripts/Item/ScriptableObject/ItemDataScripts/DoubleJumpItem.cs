using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DoubleJumpItem", menuName = "Item/DoubleJump")]
public class DoubleJumpItem : ItemData
{
    public float duration = 10f;
    
    public override void Use()
    {
        Player player = CharacterManager.Instance.Player;
        player.StartCoroutine(DoubleJumpAbleCoroutine(player));
    }

    private IEnumerator DoubleJumpAbleCoroutine(Player player) // 더블점프 효과
    {
        player.playerMovement.maxJumpCount = 2;
        yield return new WaitForSeconds(duration);
        player.playerMovement.maxJumpCount = 1;
    }
}