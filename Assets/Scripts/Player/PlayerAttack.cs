using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Projectile bulletPrefab;
    [SerializeField] private Transform gunpoint;
    private Projectile currentWeapon;
    // private AudioManager audioManager;

    private void Start()
    {
        // audioManager = GetComponent<AudioManager>();

        if (bulletPrefab != null) currentWeapon = bulletPrefab;
        else Debug.LogError("Missing: currentWeapon must have Projectile reference default.");
    }

    private void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }

    public void Fire()
    {
        GameObject gameObject = Instantiate(currentWeapon.gameObject, gunpoint.position, Quaternion.identity);
        AudioManager.Instance.PlayOneShot(currentWeapon.ShootSound);
    }

    public void SwitchWeapon(Projectile projectilePrefab)
    {
        currentWeapon = projectilePrefab;
    }

    public void ResetToBaseWeapon()
    {
        currentWeapon = bulletPrefab;
    }
}
