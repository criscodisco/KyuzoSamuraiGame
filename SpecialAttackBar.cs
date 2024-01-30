using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialAttackBar : MonoBehaviour
{
    #region Variables

    [Header ("Special Attack Bar")]
    public Slider specialSlider;

    #endregion

    #region Set Special Ability Amount
    public void SetSpecial(int special)
    {
        specialSlider.value = special;
    }

    public void SetMaxSpecial(int special)
    {
        specialSlider.maxValue = special;
        specialSlider.value = special;
    }

    #endregion
}
