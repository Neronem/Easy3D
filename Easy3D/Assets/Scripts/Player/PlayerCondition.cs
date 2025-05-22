using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCondition : MonoBehaviour
{
    public ConditionGroup conditionGroup;
    public bool invincible = false;
    public bool playerDead = false;
    
    Condition Health {get { return conditionGroup.health; }}
    
    public void TakeDamage(float value)
    {
        if (invincible == false)
        {
            Health.ChangeHealth(-value);
            if (Health.curValue <= 0)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        if (invincible == false && playerDead == false)
        {
            Debug.Log("Die");
            playerDead = true;
            GameOverUI.instance.OnGameOver.Invoke();
        }
    }
}
