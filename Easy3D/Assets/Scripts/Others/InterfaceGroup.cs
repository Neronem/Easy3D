using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShowInfoByRayCast
{
    public string GetText();
}

public interface IInteractable
{
    public void OnInteract();
}