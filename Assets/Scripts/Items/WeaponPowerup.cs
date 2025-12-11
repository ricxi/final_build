using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPowerup : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private AudioClip audioClip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerAttack player = collision.gameObject.GetComponent<PlayerAttack>();
        if (player != null)
        {
            AudioManager.Instance.PlayOneShot(audioClip);
            player.SwitchWeapon(projectilePrefab);
            Destroy(gameObject);
        }
    }
}
