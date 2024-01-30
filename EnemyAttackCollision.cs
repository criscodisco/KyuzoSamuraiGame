using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EnemyAttackCollision : MonoBehaviour
{
    #region Variables

    [Header ("Weapons")]
    private GameObject weaponLGO;
    private GameObject weaponRGO;

    [Header ("Weapon Pivot Transforms")]
    public Transform pivotWeapon_L;
    public Transform pivotWeapon_R;

    [Header ("Weapon Colliders")]
    BoxCollider weaponLCollider;
    BoxCollider weaponRCollider;

    [Header ("Animations")]
    private Animator playerAnimator;

    [Header ("Player")]
    private GameObject playerGO; 

    [Header ("Audio")]
    private GameObject audioControllerGO;
    private AudioController audioController;
    private AudioSource hurtAudioSource;

    [Header ("Colors")]
    private ChangeMeshColor changeMeshColor;
    private Color oldColor;

    [Header ("Enemy Damage Amount")]
    public int damage = 1;

    #endregion

    #region Start
    private void Start()
    {
        weaponLGO = pivotWeapon_L.GetChild(0).gameObject;
        weaponRGO = pivotWeapon_R.GetChild(0).gameObject;
        weaponLCollider = weaponLGO.GetComponent<BoxCollider>();
        weaponRCollider = weaponRGO.GetComponent<BoxCollider>();
        weaponLCollider.enabled = false;
        weaponRCollider.enabled = false;
        playerGO = GameObject.FindWithTag("Player");
        audioController = GameObject.FindWithTag("AudioSourceManager").GetComponent<AudioController>();
        hurtAudioSource = audioController.GetComponent<AudioSource>();
        changeMeshColor = playerGO.GetComponent<ChangeMeshColor>();
        oldColor = changeMeshColor.skinnedMeshRenderer.sharedMaterial.GetColor("_Color");
        playerAnimator = playerGO.GetComponent<Animator>();
    }

    #endregion

    #region Attack Collision Enable/Disable
    public void StartAttack()
    {
        weaponLCollider.enabled = true;
        weaponRCollider.enabled = true;
    }

    public void EndAttack()
    {
        weaponLCollider.enabled = false;
        weaponRCollider.enabled = false;
    }

    #endregion

    #region OnTriggerEnter
    private void OnTriggerEnter(Collider collision)
    {
        if (collision != null && collision.gameObject.CompareTag("Player") && collision.gameObject.CompareTag("Pickup") == false)
        {
            playerAnimator.SetTrigger("Hurt");
            changeMeshColor.ChangeMesh();
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            int randomHurtAudio = Random.Range(0, 16);
      
            switch (randomHurtAudio)
            {
                case 0:
                    hurtAudioSource.PlayOneShot(audioController.GetComponent<AudioController>().audioClipHurtArray[4], 1f);
                    break;
                case 1:
                    hurtAudioSource.PlayOneShot(audioController.GetComponent<AudioController>().audioClipHurtArray[5], 1f);
                    break;
                case 2:
                    hurtAudioSource.PlayOneShot(audioController.GetComponent<AudioController>().audioClipHurtArray[6], 1f);
                    break;
                case 3:
                    hurtAudioSource.PlayOneShot(audioController.GetComponent<AudioController>().audioClipHurtArray[7], 1f);
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
        else
        {
            playerAnimator.ResetTrigger("Hurt");
        }
    }

    #endregion

    #region Change Mesh Color Coroutine
    private IEnumerator DelayChangeMeshColorBack()
    {
        yield return new WaitForSeconds(1f);
        changeMeshColor.ChangeMeshBack();
    }

    #endregion
}
