using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{

    [Header("Item Pickup Variables")]
    [SerializeField] private Sprite pickupSprite;
    [SerializeField] private string pickupString;
    [SerializeField] private int itemPickupSound;
    [SerializeField] private bool playerInRange = false;

    [Header("References")]
    [SerializeField] private Player thePlayer;

    public string GetItemName { get { return pickupString; } }

    private void Update()
    {
        PlayerPickupItem();
    }

    private void PlayerPickupItem()
    {
        if (playerInRange && thePlayer != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GameManager.instance.ItemCheck();
                SoundManager.instance.PlaySoundEffect(itemPickupSound);
                thePlayer.GetPlayerItem.SetNewItem(pickupSprite, pickupString);
                this.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            thePlayer = other.GetComponent<Player>();
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            thePlayer = null;
            playerInRange = false;
        }
    }
}
