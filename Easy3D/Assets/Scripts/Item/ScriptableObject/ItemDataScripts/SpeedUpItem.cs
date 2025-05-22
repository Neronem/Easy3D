using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpeedUpItem", menuName = "Item/SpeedUp")]
public class SpeedUpItemData : ItemData
{
    public float speedBonus = 10f;
    public float duration = 10f;

    public override void Use()
    {
        Player player = CharacterManager.Instance.Player;
        player.StartCoroutine(SpeedUpCoroutine(player));
    }

    private IEnumerator SpeedUpCoroutine(Player player)
    {
        player.playerMovement.moveSpeed += speedBonus;
        yield return new WaitForSeconds(duration);
        player.playerMovement.moveSpeed -= speedBonus;
    }
}