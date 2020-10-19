using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkShop : MonoBehaviour
{
    private AudioSource audioSource;
    private bool didBuyWeapon;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !didBuyWeapon)
        {
            UIManager.SharedInstance.ShowIndectorWithKey("E");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player && Input.GetKeyDown(KeyCode.E))
        {
            if (player.GetCoins() >= 1)
            {
                audioSource.Play();
                player.EnableWeapon();
                player.RemoveCoin();
                didBuyWeapon = true;
            }
            else
            {
                Debug.Log("Get out of here!");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            UIManager.SharedInstance.HideIndector();
        }
    }
}
