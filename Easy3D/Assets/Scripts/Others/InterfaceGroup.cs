using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShowInfoByRayCast // 이 인터페이스를 상속하면, 마우스 가져다 댈 시 텍스트가 보임
{
    public string GetText();
}

public interface IInteractable // 이 인터페이스를 상속하면, 플레이어가 상호작용이 가능함
{
    public void OnInteract();
}