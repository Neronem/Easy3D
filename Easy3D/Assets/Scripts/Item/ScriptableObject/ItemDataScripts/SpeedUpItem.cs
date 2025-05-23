using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpeedUpItem", menuName = "Item/SpeedUp")]
public class SpeedUpItemData : ItemData
{
    public float speedBonus = 10f; // 추가속도량
    public float duration = 10f; // 지속시간

    public override void Use() 
    {
        Player player = CharacterManager.Instance.Player;
        player.StartCoroutine(SpeedUpCoroutine(player)); // [ItemData 상속받는 클래스 공통]
                                                                // player.으로 사용하는 이유
                                                                // 1. ItemData 상속받은 스크립트는 Monobehaviour 상속이 안되서 StartCoroutine을 못씀
                                                                // 2. player 객체 가져와서 속성 바꾸기 위함
    }

    private IEnumerator SpeedUpCoroutine(Player player) // 속도 올려주는 효과
    {
        player.playerMovement.moveSpeed += speedBonus;
        yield return new WaitForSeconds(duration);
        player.playerMovement.moveSpeed -= speedBonus;
    }
}