using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    #region Variables
    [Header ("Enemy Health")]
    public int currentHealth = 3;

    [Header ("Enemy Death Status")]
    public bool isEnemyDead = false;

    #endregion

    #region Enemy Take Damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0 && isEnemyDead == false)
        {
            isEnemyDead = true;
        }
    }

    #endregion
}
