using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerExplosion : MonoBehaviour
{
    #region Variables

    [Header ("Player ")]
    PlayerController playerController;

    [Header ("Player Collider")]
    private Collider[] colliders = new Collider[20];

    [Header ("Explosion Animation")]
    [SerializeField] ParticleSystem explosionEffect;
    private float abilityRadius = 7f;
    private Animator animator;

    #endregion

    #region Start
    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
    }

    #endregion

    #region Player Special Ability Explosion Radius
    public void ExecuteSpecialAbility()
    {
        ExplosionFX();
        StartCoroutine(ExplosionPlayerAnimation());
        int numberOfColliders = Physics.OverlapSphereNonAlloc(transform.position, abilityRadius, colliders);

        if (numberOfColliders > 0)
        {
            for (int i = 0; i < numberOfColliders; i++)
            {
                if (colliders[i].gameObject.TryGetComponent(out EnemyHealth depletedHealth))
                {
                    depletedHealth.TakeDamage(10);
                }
            }
        }
    }

    #endregion

    #region Explosion FX
    private void ExplosionFX()
    {
        explosionEffect.Emit(1);
    }

    private IEnumerator ExplosionPlayerAnimation()
    {
        playerController.isSpecialAttacking = true;
        animator.ResetTrigger("Special_Attack");
        animator.SetTrigger("Special_Attack");
        yield return new WaitForSeconds(1.6f);
        playerController.isSpecialAttacking = false;
    }

    #endregion
}
