using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialPickup : MonoBehaviour
{
    #region Variables

    [Header ("Pickup Manager")]
    private PickupManager pickupManager;

    [Header ("Pickup Rotation")]
    public float rotationSpeed;

    [Header ("Pickup Audio")]
    public AudioClip pickupSound;

    [Header ("Special Pickup Amount Gained")]
    public int specialGained;

    #endregion

    #region Start

    private void Start()
    {
        pickupManager = GameObject.FindWithTag("PickupSpawner").GetComponent<PickupManager>();
    }

    #endregion

    #region Update
    private void Update()
    {
        RotatePickup();   
    }

    #endregion

    #region Pickup Rotation
    public void RotatePickup()
    {
        transform.Rotate(transform.right * rotationSpeed * Time.deltaTime, Space.World);
    }

    #endregion

    #region Pickup Collision
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.CompareTag("Enemy") == false)
        {
            SpecialPickupPickedUp();
        }
    }

    private void SpecialPickupPickedUp()
    {
        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position, 1f);
        }

        pickupManager.RestoreSpecialWithPickup();
        Destroy(gameObject);
    }

    #endregion
}
