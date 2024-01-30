using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    #region Variables

    [Header ("Player Health Amount")]
    public int maxHealth = 30;
    public int currentHealth;
    public HealthBar healthBar;

    [Header ("Player Death Status")]
    public bool isPlayerDead = false;

    #endregion

    #region Start
    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    #endregion

    #region Damage Player Takes
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0 && isPlayerDead == false)
        {
            isPlayerDead = true;
        }
    }

    #endregion
}
