using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    public ConditionGroup conditionGroup;
    public bool invincible;
    
    Condition Health {get { return conditionGroup.health; }}

    public void TakeDamage(int value)
    {
        if (!invincible)
        {
            Health.Add(value);
        }
    }
}
