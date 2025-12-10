using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Projectile bulletPrefab; // Player will always have this as their default weapon
    [SerializeField] private Transform gunpoint;
    [SerializeField] private Transform gunpointA;
    [SerializeField] private Transform gunpointB;
    [SerializeField] private string resetSceneName = "TutorialLevel";

    private Projectile _currentWeapon;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void Start()

    {
        if (bulletPrefab != null)
        {
            _currentWeapon = bulletPrefab;
            PlayerUIHandler.Instance.UpdateWeaponImage(_currentWeapon.GetSprite());
        }
        else Debug.LogError("Missing Reference: _currentWeapon must have Projectile reference default.");
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1")) Fire();
        if (Input.GetButtonDown("Jump")) DoubleFire();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode __)
    {
        if (scene.name == resetSceneName) return;
        if (_currentWeapon == null) _currentWeapon = bulletPrefab;
        PlayerUIHandler.Instance.UpdateWeaponImage(_currentWeapon.GetSprite());
    }

    public void Fire()
    {
        AudioManager.Instance.PlayOneShot(_currentWeapon.ShootSound);
        Instantiate(_currentWeapon.gameObject, gunpoint.position, Quaternion.identity);
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
        PlayerUIHandler.Instance.UpdateWeaponImage(_currentWeapon.GetSprite());
    }

    public void ResetToBaseWeapon()
    {
        _currentWeapon = bulletPrefab;
        PlayerUIHandler.Instance.UpdateWeaponImage(_currentWeapon.GetSprite());
    }
}
