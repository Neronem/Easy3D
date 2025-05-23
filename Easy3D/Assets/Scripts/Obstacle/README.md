# 🧱 Obstacle 폴더 설명서

이 폴더는 플레이어의 이동 경로에 배치되어 특정한 상호작용을 유도하거나 방해하는 장애물(Obstacle) 관련 스크립트를 포함하고 있습니다. 각 스크립트의 역할은 다음과 같습니다:

---

## 🔹 `ClimbWallPlatform.cs`
🧗‍♂️ 플레이어가 벽을 타고 올라갈 수 있는 기능을 제공하는 클래스입니다.

- `rayCount`: 벽에서 발사할 Ray 수
- `spacing`: 각 Ray 사이의 간격
- `rayLength`: Ray의 길이
- Ray를 일정 간격으로 아래쪽에서 위쪽으로 여러 개 쏘아, 플레이어가 벽 근처에 있으면 `isClimbing`을 `true`로 설정하여 벽을 탈 수 있도록 함

---

## 🔹 `JumpPad.cs`
🦘 플레이어가 닿으면 점프시키는 점프대 기능을 담당합니다.

- `jumpForce`: 점프할 때 사용할 힘의 크기
- 플레이어가 `JumpPad`에 닿으면 기존의 `y`속도를 0으로 초기화하고 위 방향으로 힘을 가함
- `IShowInfoByRayCast` 인터페이스를 구현하여 상호작용 시 텍스트를 보여줌 (`점프대 \n 플라잉곰`)

---

## 🔹 `LaserTrap.cs`
💥 레이저 트랩 기능을 하는 클래스입니다.

- `rayOrigin`: 레이저를 발사할 시작 위치
- `rayLength`: 레이저의 최대 거리
- `playerLayer`: 플레이어 레이어 마스크
- 플레이어가 레이저에 닿으면 즉시 사망 처리 (`CharacterManager.Instance.Player.playerCondition.Die()`)
- `LineRenderer`를 통해 실제 레이저를 시각화하여 시작점부터 플레이어 충돌 지점까지 빨간 선을 그림

---

## 🔹 `MovingPlatform.cs`
🚧 좌우로 움직이는 플랫폼을 구현한 클래스입니다.

- `speed`: 이동 속도
- `distance`: 이동 거리의 절반 (총 이동 범위는 `distance * 2`)
- `PlayerOnPlatform`: 플레이어가 플랫폼 위에 있는지 여부
- `player`: 플랫폼 위의 플레이어 오브젝트 참조
- 플레이어가 플랫폼 위에 있는 동안, 플랫폼과 함께 움직이도록 위치를 조정함
- `Mathf.PingPong`을 사용해 플랫폼이 왕복 운동함

---

## 🧩 요약

| 클래스명            | 기능 설명                                          |
|-------------------|---------------------------------------------------|
| ClimbWallPlatform |  벽에 가까이 있을 때 플레이어가 벽을 타고 오르게 함        |
| JumpPad           | 플레이어와 충돌 시 위로 튀어 오르게 만드는 점프대 역할       |
| LaserTrap         | 플레이어와 충돌하면 즉사시키는 빨간색 레이저 함정           |
| MovingPlatform    | 왕복하는 플랫폼으로, 플레이어가 위에 있을 경우 같이 움직이게 함 |

---
