using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    #region Variables

    [Header ("Health Slider")]
    public Slider healthSlider;

    #endregion

    #region Set Current/Max Health
    public void SetHealth(int health)
    {
        healthSlider.value = health;
    }

    public void SetMaxHealth(int health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    #endregion
}
