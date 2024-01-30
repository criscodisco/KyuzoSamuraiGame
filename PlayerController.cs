using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    #region Variables

    [Header ("Player Movement")]
    public NavMeshAgent navMeshAgent;
    private Rigidbody rb;
    private float currentAngle;
    private float currentAngleVelocity;
    [SerializeField] private float rotationSmoothTime;
    private float speed;

    [Header ("Camera")]
    private Camera mainCamera;

    [Header ("Animation")]
    private Animator animator;

    [Header ("Player Collision")]
    private CapsuleCollider capsuleCollider;

    [Header ("Audio")]
    [SerializeField] private GameObject audioControllerGO;
    private AudioSource audioSource;
    private AudioController audioController;

    [Header ("Player Special Ability")]
    [SerializeField] private SpecialAmount specialAmount;
    private PlayerExplosion playerExplosion;
    private int specialJuice = 1;
    public bool isSpecialAttacking = false;

    [Header ("Game Over Handler")]
    [SerializeField] private GameObject gameOverHandlerGO;  
    private GameOverHandler gameOverHandler;

    #endregion

    #region Start
    private void Start()
    {
        mainCamera = Camera.main;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        speed = 400f;
        navMeshAgent = GetComponent<NavMeshAgent>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        audioController = audioControllerGO.gameObject.GetComponent<AudioController>();
        audioSource = audioControllerGO.gameObject.GetComponent<AudioSource>();
        playerExplosion = GetComponent<PlayerExplosion>();
        specialAmount = GetComponent<SpecialAmount>();
        gameOverHandler = GetComponent<GameOverHandler>();
    }

    #endregion

    #region Updates
    private void FixedUpdate()
    {
        if (gameObject.GetComponent<PlayerHealth>().isPlayerDead == false)
        {
            HandleMovement();
        }
    }

    private void Update()
    {
        if (gameObject.GetComponent<PlayerHealth>().isPlayerDead == false)
        {
            HandleRandomAttackChoice();
            Attack_Combo();
            if (!specialAmount.isSpecialDepleted)
            {
                SpecialAbility();
            }
        }
        else
        {
            capsuleCollider.enabled = false;
            gameObject.GetComponent<Animator>().Play("playerDeath");
        }     
    }

    #endregion

    #region Delay Appearance of Game Over Menu Coroutine
    private IEnumerator DelayGameOverMenu()
    {
        yield return new WaitForSeconds(5.3f);
        gameOverHandlerGO.GetComponent<GameOverHandler>().OpenMainMenu();
    }

    #endregion

    #region Delay Destroying Player Coroutine
    private void DelayDestroyObject()
    {
        Destroy(gameObject);
    }

    #endregion

    #region Player Movement
    private void HandleMovement()
    {
        Vector3 playerMovement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (playerMovement.magnitude >= 0.1f && isSpecialAttacking == false)
        {
            float speedPercent = playerMovement.magnitude;
            animator.SetFloat("PlayerSpeed", speedPercent);
            float targetAngle = Mathf.Atan2(playerMovement.x, playerMovement.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
            currentAngle = Mathf.SmoothDampAngle(currentAngle, targetAngle, ref currentAngleVelocity, rotationSmoothTime);
            transform.rotation = Quaternion.Euler(0, currentAngle, 0);
            Vector3 rotatedMovement = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;            
            navMeshAgent.velocity = rotatedMovement * speed * Time.deltaTime;
        }
        else
        {
            navMeshAgent.velocity = Vector3.zero;
            animator.SetFloat("PlayerSpeed", 0);
        }       
    }

    #endregion

    #region Player Attack
    private void HandleRandomAttackChoice()
    {
        int randomAnimation = Random.Range(0, 2);

        switch (randomAnimation)
        {
            case 0:
                Attack_1();
                break;
            case 1:
                Attack_2();
                break;
            default:
                break;
        }
    }

    private void Attack_1()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Attack_1");
            audioSource.PlayOneShot(audioController.GetComponent<AudioController>().audioClipHurtArray[8], 1f);
        }
        else
        {
            animator.ResetTrigger("Attack_1");      
        }      
    }

    private void Attack_2()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetButtonDown("Fire1"))
        {         
            animator.SetTrigger("Attack_2");
            audioSource.PlayOneShot(audioController.GetComponent<AudioController>().audioClipHurtArray[8], 1f);
        }
        else
        {
            animator.ResetTrigger("Attack_2");
        }
    }

    private void Attack_Combo()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetButtonDown("Fire2"))
        {
            animator.SetTrigger("Attack_Combo");
        }
        else
        {
            animator.ResetTrigger("Attack_Combo");
        }
    }
    private void SpecialAbility()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire3"))
        {
            navMeshAgent.velocity = Vector3.zero;
            audioSource.PlayOneShot(audioController.GetComponent<AudioController>().audioClipHurtArray[13], 1f);
            specialAmount.UseSpecial(specialJuice);
            playerExplosion.ExecuteSpecialAbility();
        }
    }

    private void PlayerHurt()
    {      
        animator.SetTrigger("Hurt");   
    }

    private void ResetAllTriggers()
    {
        animator.ResetTrigger("Attack_1");
        animator.ResetTrigger("Attack_2");
        animator.ResetTrigger("Hurt");
    }

    #endregion

    #region Sword Audio
    private void HandleRandomSwordAudio()
    {
        int randomSwordAudio = Random.Range(0, 3);

        switch (randomSwordAudio)
        {
            case 0:
                audioSource.PlayOneShot(audioController.GetComponent<AudioController>().audioClipHurtArray[8], 1f);
                break;
            case 1:
                audioSource.PlayOneShot(audioController.GetComponent<AudioController>().audioClipHurtArray[9], 1f);
                break;
            case 2:
                audioSource.PlayOneShot(audioController.GetComponent<AudioController>().audioClipHurtArray[10], 1f);
                break;
            default:
                break;
        }
    }

    #endregion

    #region Collision
    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null && collision.gameObject.tag != "Enemy")
        {
            rb.isKinematic = false;
        }
    }

    #endregion
}
