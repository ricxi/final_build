using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Projectile bulletPrefab;
    [SerializeField] private Transform gunpoint;
    [SerializeField] private Transform gunpointA;
    [SerializeField] private Transform gunpointB;

    private Projectile _currentWeapon;

    private void Start()
    {
        if (bulletPrefab != null) _currentWeapon = bulletPrefab;
        else Debug.LogError("Missing: _currentWeapon must have Projectile reference default.");
    }

    private void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }

        if (Input.GetButtonDown("Jump"))
        {
            DoubleFire();
        }

    }

    public void Fire()
    {
        AudioManager.Instance.PlayOneShot(_currentWeapon.ShootSound);
        GameObject gameObject = Instantiate(_currentWeapon.gameObject, gunpoint.position, Quaternion.identity);
    }

    public void DoubleFire()
    {
        AudioManager.Instance.PlayOneShot(_currentWeapon.ShootSound);
        Instantiate(bulletPrefab, gunpointA.position, Quaternion.identity);
        Instantiate(bulletPrefab.gameObject, gunpointB.position, Quaternion.identity);
    }

    public void SwitchWeapon(Projectile projectilePrefab)
    {
        _currentWeapon = projectilePrefab;
    }

    public void ResetToBaseWeapon()
    {
        _currentWeapon = bulletPrefab;
    }
}
