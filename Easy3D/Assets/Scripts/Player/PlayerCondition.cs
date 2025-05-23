using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCondition : MonoBehaviour
{
    // 플레이어의 상태(체력 등)를 관리하는 그룹 클래스 참조
    public ConditionGroup conditionGroup;

    // 무적 상태 여부 (무적일 땐 데미지 받지 않음)
    public bool invincible = false;

    // 플레이어가 사망했는지 여부
    public bool playerDead = false;

    // conditionGroup 내부에서 체력 상태를 참조
    Condition Health { get { return conditionGroup.health; } }

    // 데미지를 받을 때 호출되는 함수
    public void TakeDamage(float value)
    {
        // 무적 상태가 아닐 때만 데미지를 받음
        if (invincible == false)
        {
            // 체력 감소
            Health.ChangeHealth(-value);

            // 체력이 0 이하가 되면 사망 처리
            if (Health.curValue <= 0)
            {
                Die();
            }
        }
    }

    // 플레이어가 사망할 때 호출
    public void Die()
    {
        // 무적 상태가 아니고 아직 죽지 않았다면
        if (invincible == false && playerDead == false)
        {
            playerDead = true; // 사망 상태로 변경

            // 게임 오버 UI에 등록된 이벤트 호출
            GameOverUI.instance.OnGameOver.Invoke();
        }
    }
}