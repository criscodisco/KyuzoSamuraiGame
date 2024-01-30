using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerAttackCollision : MonoBehaviour
{
    #region Variables

    [Header ("Weapon")]
    public Transform pivotWeapon;
    BoxCollider weaponCollider;
    private GameObject weaponGO;

    [Header ("Player")]
    [SerializeField] public GameObject playerPrefab;

    [Header ("Enemy Spawner")]
    [SerializeField] private SpawnEnemy enemySpawner;

    [Header ("Enemy")]
    [SerializeField] private GameObject enemyGO;

    [Header ("Audio")]
    private GameObject audioControllerGO;
    private AudioController audioController;
    private AudioSource hurtAudioSource;

    [Header("Mesh Colors")]
    private ChangeMeshColor changeMeshColor;
    private Color oldColor;

    [Header ("Damage Dealt")]
    public int damage = 1;

    #endregion

    #region Start
    private void Start()
    {
        weaponGO = pivotWeapon.GetChild(0).gameObject;
        weaponCollider = weaponGO.GetComponent<BoxCollider>();
        weaponCollider.enabled = false;
        audioController = GameObject.FindGameObjectWithTag("AudioSourceManager").GetComponent<AudioController>();
        audioControllerGO = GameObject.FindGameObjectWithTag("AudioSourceManager");
        hurtAudioSource = audioControllerGO.gameObject.GetComponent<AudioSource>();
        changeMeshColor = enemyGO.GetComponent<ChangeMeshColor>();
        oldColor = changeMeshColor.skinnedMeshRenderer.sharedMaterial.GetColor("_Color");
    }

    #endregion

    #region Player Weapon Collider Enable/Disable
    public void StartAttack()
    {
        weaponCollider.enabled = true;
    }

    public void EndAttack()
    {
        weaponCollider.enabled = false;
    }

    #endregion

    #region Player Attack Combo Audio
    public void PlayComboAttackSound()
    {
        hurtAudioSource.PlayOneShot(audioController.audioClipHurtArray[14]);
    }

    public void PlayComboAttackSound2()
    {
        hurtAudioSource.PlayOneShot(audioController.audioClipHurtArray[15]);
    }

    public void PlayComboAttackSound3()
    {
        hurtAudioSource.PlayOneShot(audioController.audioClipHurtArray[16]);
    }

    #endregion

    #region Attack Collision OnTriggerEnter
    private void OnTriggerEnter(Collider collision)
    {
        if (collision != null && collision.gameObject.CompareTag("Enemy"))
        {
            enemySpawner.enemyAnimator.ResetTrigger("EnemyHurt");
            enemySpawner.enemyAnimator.SetTrigger("EnemyHurt");
            changeMeshColor.ChangeMesh();
            collision.GetComponent<EnemyHealth>().TakeDamage(damage);

            /*  Gives random chance of a sound being played when the enemy is hurt  */
            /*  There is currently a 1 out of 4 chance a hurt sound will occur with these switch statements  */
            /*  This reduced occurence of a sound seemed to make the game less annoying after testing  */

            int randomHurtAudio = Random.Range(0, 16);

            switch (randomHurtAudio)
            {
                case 0:
                    hurtAudioSource.PlayOneShot(audioControllerGO.GetComponent<AudioController>().audioClipHurtArray[0]);
                    break;
                case 1:
                    hurtAudioSource.PlayOneShot(audioControllerGO.GetComponent<AudioController>().audioClipHurtArray[1]);
                    break;
                case 2:
                    hurtAudioSource.PlayOneShot(audioControllerGO.GetComponent<AudioController>().audioClipHurtArray[2]);
                    break;
                case 3:
                    hurtAudioSource.PlayOneShot(audioControllerGO.GetComponent<AudioController>().audioClipHurtArray[3]);
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
                default:
                    break;
            }

            StartCoroutine(DelayChangeMeshColorBack());
        }
    }

    #endregion

    #region Changes Enemy Mesh Color Back Coroutine
    private IEnumerator DelayChangeMeshColorBack()
    {
        yield return new WaitForSeconds(1f);
        changeMeshColor.ChangeMeshBack();
    }

    #endregion
}
