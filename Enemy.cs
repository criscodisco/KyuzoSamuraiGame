using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    #region Variables

    [Header ("AI Movement")]
    [SerializeField] private float rotationSmoothTime;
    [SerializeField] private float speed;
    private float currentAngle;
    private float currentAngleVelocity;
    
    [Header ("Enemy Position")]
    [SerializeField] public Transform currentEnemyPosition;

    [Header ("Animation")]
    public Animator animator;

    [Header ("Enemy Movement")]
    private NavMeshAgent navMeshAgent;

    [Header ("Enemy Location")]
    private Transform playerLocation;
    private float enemyDistance = 1.5f;

    [Header ("Enemy Pickup Manager")]
    private PickupManager pickupManager;

    [Header ("Enemy Spawner")]
    private SpawnEnemy spawnEnemy;

    #endregion

    #region Start
    private void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerLocation = GameObject.FindWithTag("Player").transform;
        pickupManager = GameObject.FindWithTag("PickupSpawner").GetComponent<PickupManager>();
        spawnEnemy = GameObject.FindWithTag("EnemySpawner").GetComponent<SpawnEnemy>();
    }

    #endregion

    #region Update
    private void Update()
    {
        if (gameObject.GetComponent<EnemyHealth>().isEnemyDead == false)
        {
            HandleAIMovement();
        }
        else
        {
            currentEnemyPosition = transform;
            gameObject.GetComponent<Animator>().Play("death1(wd)");          
            Invoke(nameof(DelayDestroyObject), 4f);
        }
    }

    #endregion

    #region Destroy Object Coroutine
    private void DelayDestroyObject()
    {
        pickupManager.SpawnPickupOnEnemyDeath(transform);
        bool isThisEnemyDead = true;

        if (spawnEnemy.isSpawningDone == true && isThisEnemyDead == true)
        {
            if ((GameObject.FindGameObjectsWithTag("Enemy").Length == 1) || (GameObject.FindGameObjectsWithTag("Enemy").Length == 0))
            {
                spawnEnemy.youWinTheGame = true;
            }
        }

        Destroy(gameObject);
    }

    #endregion

    #region AI Movement
    public void HandleAIMovement()
    {
        transform.LookAt(playerLocation);
        navMeshAgent.SetDestination(playerLocation.transform.position);

        if (navMeshAgent.velocity.magnitude >= 0.1f)
        {
            animator.SetFloat("EnemySpeed", navMeshAgent.velocity.magnitude);
        }
        else
        {
            animator.SetFloat("EnemySpeed", 0);
        }

        if (Vector3.Distance(transform.position, playerLocation.position) <= enemyDistance)
        {
            ResetAllAttackTriggers();
            HandleEnemyRandomAttack();       
        }
    }

    #endregion

    #region EnemyAttacks
    private void HandleEnemyRandomAttack()
    {
        int randomAttack = Random.Range(0, 3);

        switch(randomAttack)
        {
            case 0:
                EnemyAttack_1();
                break;
            case 1:
                EnemyAttack_2();
                break;
            case 2:
                EnemyAttack_3();
                break;
            default:
                break;
        }        
    }

    private void EnemyAttack_1()
    {
        gameObject.GetComponent<NavMeshAgent>().velocity = Vector3.zero;
        animator.SetTrigger("EnemyAttack_1");
    }

    private void EnemyAttack_2()
    {
        gameObject.GetComponent<NavMeshAgent>().velocity = Vector3.zero;
        animator.SetTrigger("EnemyAttack_2");
    }

    private void EnemyAttack_3()
    {
        gameObject.GetComponent<NavMeshAgent>().velocity = Vector3.zero;
        animator.SetTrigger("EnemyAttack_3");
    }

    private void ResetAllAttackTriggers()
    {
        animator.ResetTrigger("EnemyAttack_1");
        animator.ResetTrigger("EnemyAttack_2");
        animator.ResetTrigger("EnemyAttack_3");
    }

    #endregion
}
