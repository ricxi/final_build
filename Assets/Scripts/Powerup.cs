using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    // ids:
    // 10 = health
    // 11 = defense/shield
    // 12 = offense/attack?
    [SerializeField] private PowerupType powerupType;
    [SerializeField] private int healAmount = 3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        PlayerHealth playerH = collision.gameObject.GetComponent<PlayerHealth>();

        if (player != null)
        {
            switch (powerupType.id)
            {
                case 10:
                    if (!playerH.CanHeal())
                        break;
                    playerH.Heal(healAmount);
                    Destroy(gameObject);
                    break;
                case 11:
                    player.ActivateShield(5f);
                    Destroy(gameObject);
                    break;
                default:
                    Debug.Log("illegal powerup");
                    break;
            }

        }
    }
}
