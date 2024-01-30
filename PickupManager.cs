using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    #region Variables

    [Header ("Player")]
    [SerializeField] private GameObject player;
    private PlayerHealth playerHealth;

    [Header ("Pickups")]
    [SerializeField] private GameObject healthPickup;
    [SerializeField] private GameObject specialPickup;
    [SerializeField] private GameObject enemyGO;
    private GameObject newSpecialPickup;
    private GameObject newHealthPickup;
    private Enemy enemy;

    [Header ("Special/Health Gained Amounts")]

    [SerializeField] private SpecialAttackBar specialAttackBar;
    public int healthGained;
    public int specialGained = 3;
    private SpecialAmount specialAmount;

    #endregion

    #region Start
    private void Start()
    {
        playerHealth = player.GetComponent<PlayerHealth>();
        specialAmount = player.GetComponent<SpecialAmount>();
    }

    #endregion

    #region Spawn Pickup

    public void SpawnPickupOnEnemyDeath(Transform enemyTransform)
    {
        /*  Gives random chance of the death of an enemy resulting in the spawning of a pickup  */
        /*  There is currently a 1 out of 9 chance a pickup will spawn with these switch statements  */

        int chanceOfPickup = Random.Range(0, 18);

        switch (chanceOfPickup)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                break;
            case 10:
                break;
            case 11:
                break;
            case 12:
                break;
            case 13:
                break;
            case 14:
                break;
            case 15:
                break;
            case 16:
                newHealthPickup = Instantiate(healthPickup, enemyTransform.position + new Vector3(0, 1, 0), Quaternion.identity);
                StartCoroutine(HealthPickupDisappearsCoroutine());
                break;
            case 17:
                newSpecialPickup = Instantiate(specialPickup, enemyTransform.position + new Vector3(0, 1, 0), Quaternion.identity);
                StartCoroutine(SpecialPickupDisappearsCoroutine());        
                break;
            default:
                break;
        }       
    }

    #endregion

    #region Pickups Disappear Coroutines
    private IEnumerator SpecialPickupDisappearsCoroutine()
    {
        yield return new WaitForSeconds(10f);
        if (newSpecialPickup != null)
        {
            Destroy(newSpecialPickup);
        }
    }

    private IEnumerator HealthPickupDisappearsCoroutine()
    {
        yield return new WaitForSeconds(10f);
        if (newHealthPickup != null)
        {
            Destroy(newHealthPickup);
        }
    }

    #endregion

    #region Restore Player Health/Special
    public void HealPlayerWithPickup()
    {
        playerHealth.TakeDamage(healthGained * (-1));
    }

    public void RestoreSpecialWithPickup()
    {
        specialAmount.GainSpecialFromPickup();
        specialAttackBar.SetMaxSpecial(specialGained);
    }

    #endregion
}
