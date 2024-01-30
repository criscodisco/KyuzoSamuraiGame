using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    #region Variables

    [Header ("Rotation")]
    public float rotationSpeed;

    [Header ("Sounds")]
    public AudioClip pickupSound;

    [Header ("Player Health")]
    private PlayerController playerHealth;
    public int healthGained;

    [Header ("Pickup Manager")]
    private PickupManager pickupManager;

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
        transform.Rotate(transform.up* rotationSpeed * Time.deltaTime, Space.World);
    }

    #endregion

    #region Pickup Collision
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PickupPickedUp();
        }
    }

    public void PickupPickedUp()
    {
        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position, 1f);
        }

        pickupManager.HealPlayerWithPickup();
        Destroy(gameObject);
    }

    #endregion
}
