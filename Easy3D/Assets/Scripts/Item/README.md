# 🎮 Item 폴더 설명서

이 폴더에는 플레이어 캐릭터에 일시적인 버프(효과)를 부여하는 **아이템 데이터 클래스들**이 포함되어 있습니다.  
각 아이템은 `ItemData` 클래스를 상속받아 구현되며, 플레이어가 아이템을 사용했을 때 일정 시간 동안 효과가 유지됩니다.

---

## 🏷️ 기본 아이템 데이터 클래스 — `ItemData`

모든 아이템 클래스가 상속받는 추상 클래스입니다.  
아이템의 기본 정보와 필수 메서드를 정의합니다.

~~~csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    [Header("Info")]
    public string itemName;            // 아이템 이름
    public string itemDescription;     // 아이템 설명
    public Sprite itemIcon;            // 아이템 아이콘
    public GameObject itemPrefab;      // 아이템 프리팹

    public abstract void Use();        // 아이템 효과는 자식 클래스에서 구현
}
~~~

- `ScriptableObject`를 상속받아 에디터에서 자산으로 생성 및 관리 가능  
- `itemName`, `itemDescription`, `itemIcon`, `itemPrefab` 등의 기본 속성 포함  
- `Use()` 메서드는 반드시 자식 클래스에서 구현해야 하며, 아이템 사용 시 호출

---

## 🔍 공통 특징

- 아이템 클래스들은 모두 `ItemData`를 상속하며, `Use()` 메서드를 오버라이드합니다.  
- 각 아이템 효과는 코루틴으로 구현되어 있으며, `WaitForSeconds`를 통해 지속시간을 제어합니다.

---

## 🗂 아이템 목록 및 설명

### 1. 🦘 DoubleJumpItem

- **효과:** 플레이어의 최대 점프 횟수를 2회로 증가시켜 더블 점프 가능  
- **지속 시간:** `duration` 변수로 설정 (기본 10초)  
- **코어 로직:**  
  `maxJumpCount` 값을 1에서 2로 변경 후 일정 시간 유지, 이후 다시 1로 복구

~~~csharp
public float duration = 10f;

public override void Use()
{
    Player player = CharacterManager.Instance.Player;
    player.StartCoroutine(DoubleJumpAbleCoroutine(player));
}

private IEnumerator DoubleJumpAbleCoroutine(Player player)
{
    player.playerMovement.maxJumpCount = 2;
    yield return new WaitForSeconds(duration);
    player.playerMovement.maxJumpCount = 1;
}
~~~

---

### 2. ⚡ SpeedUpItemData

- **효과:** 플레이어 이동 속도를 일정량 증가시킴  
- **지속 시간:** `duration` 변수 (기본 10초)  
- **추가 속도량:** `speedBonus` 변수 (기본 10f)  
- **코어 로직:**  
  `moveSpeed` 값에 `speedBonus` 더하고, 일정 시간 후 다시 원래 속도로 감소

~~~csharp
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
~~~

---

### 3. 🛡️ InvincibleItem

- **효과:** 플레이어를 일정 시간 무적 상태로 만듦  
- **지속 시간:** `duration` 변수 (기본 10초)  
- **코어 로직:**  
  `playerCondition.invincible` 값을 `true`로 변경 후 일정 시간 유지, 이후 `false`로 복구

~~~csharp
public float duration = 10f;

public override void Use()
{
    Player player = CharacterManager.Instance.Player;
    player.StartCoroutine(InvincibleCoroutine(player));
}

private IEnumerator InvincibleCoroutine(Player player)
{
    player.playerCondition.invincible = true;
    yield return new WaitForSeconds(duration);
    player.playerCondition.invincible = false;
}
~~~

---

## 🎯 맵 위 아이템 오브젝트 — `ItemObject`

이 클래스는 게임 맵 위에 존재하는 **아이템 오브젝트**를 담당하며,  
플레이어가 근처에서 **E 키로 상호작용 시 아이템을 획득**하도록 구현되어 있습니다.

~~~csharp
using UnityEngine;

public class ItemObject : MonoBehaviour, IShowInfoByRayCast, IInteractable
{
    public ItemData data;

    public string GetText() // 상호작용 시 표시할 텍스트
    {
        return $"{data.itemName} \n {data.itemDescription}";
    }

    public void OnInteract() // E키로 상호작용 시 동작
    {
        CharacterManager.Instance.Player.playerItem.playerItemData = data; // 플레이어가 아이템 획득
        Inventory.instance.TriggerGotItem();  // 인벤토리에 이벤트 발생
        Destroy(gameObject); // 맵 위 오브젝트 삭제
    }
}
~~~

- `data`: 해당 오브젝트가 가지고 있는 `ItemData` 정보  
- `GetText()`: 마우스 오버 시 보여줄 아이템 이름과 설명  
- `OnInteract()`: 플레이어가 아이템을 **획득하고**, **인벤토리 이벤트 발생**, **오브젝트 제거**  

---

## 💡 설계 목적

- 아이템 효과는 모두 **코루틴**으로 처리하여 시간제한 버프를 간단하게 구현합니다.    
- 새로운 아이템은 `ItemData`를 상속한 클래스로 확장 가능하며,  
  오브젝트로 만들 때는 `ItemObject`에 연결해주면 됩니다.

---
## 🧩 요약

| 클래스 이름         | 상속 구조 및 관계                     | 역할 및 동작 방식                                                                                         |
|------------------|---------------------------------|-----------------------------------------------------------------------------------------------------|
| `ItemData`       | 최상위 추상 클래스 (`ScriptableObject` 상속) | - 모든 아이템의 공통 속성(`itemName`, `itemIcon` 등)과 추상 메서드 `Use()` 정의<br>- 자식 클래스가 반드시 `Use()`를 구현하여 아이템 효과를 구현하도록 강제 |
| `DoubleJumpItem`  | `ItemData` 상속                    | - `Use()` 오버라이드<br>- 플레이어 점프 횟수를 1회에서 2회로 증가시키는 코루틴 실행<br>- 일정 시간 후 원래 점프 횟수로 복구        |
| `SpeedUpItemData` | `ItemData` 상속                    | - `Use()` 오버라이드<br>- 플레이어 이동 속도에 `speedBonus`를 더하는 코루틴 실행<br>- 지속시간 후 원래 속도로 복구                  |
| `InvincibleItem`  | `ItemData` 상속                    | - `Use()` 오버라이드<br>- 플레이어 무적 상태(`invincible=true`)를 켜는 코루틴 실행<br>- 일정 시간 후 무적 상태 해제                  |

### 핵심 포인트

- `ItemData`가 **템플릿 역할**을 하며, 실제 아이템 효과는 모두 `Use()` 메서드 안에서 **코루틴을 통해 일정 시간 동안 효과를 적용하고 해제하는 패턴**을 따릅니다.
- 각 아이템 클래스는 `ItemData`를 상속받아 효과별로 필요한 상태 변화와 시간 관리를 구현하는 **특화된 버프 클래스**입니다.
- 확장성에 초점을 두고 상속을 적용했습니다.

---

