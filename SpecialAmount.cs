using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpecialAmount : MonoBehaviour
{
    #region Variables

    [Header ("Special Amount")]
    public int maxSpecial = 3;
    public int currentSpecial;

    [Header ("Special Empty Status")]
    public bool isSpecialDepleted = false;

    [Header ("Special Attack Bar")]
    public SpecialAttackBar specialBar;

    #endregion

    #region Start
    private void Start()
    {
        currentSpecial = maxSpecial;
        specialBar.SetSpecial(currentSpecial);
    }

    #endregion

    #region Use Special Ability
    public void UseSpecial(int specialAmount)
    {
        currentSpecial -= specialAmount;
        if (currentSpecial > maxSpecial)
        {
            currentSpecial = maxSpecial;
        }

        specialBar.SetSpecial(currentSpecial);

        if (currentSpecial <= 0 && isSpecialDepleted == false)
        {
            isSpecialDepleted = true;
        }
    }

    #endregion

    #region Add to Special Ability Amount with Pickup
    public void GainSpecialFromPickup()
    {
        currentSpecial = maxSpecial;
        isSpecialDepleted = false;
    }

    #endregion
}
