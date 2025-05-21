using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    public ConditionGroup conditionGroup;   
    
    Condition health {get {return conditionGroup.health;}}
}
