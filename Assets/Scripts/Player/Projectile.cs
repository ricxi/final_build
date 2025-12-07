using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileType projectileType;
    [SerializeField] private GameObject explosionPrefab; // animation for enemy explosion
    [SerializeField] private Vector2 direction;
    // [SerializeField] private Vector2 direction = new Vector2(1, 0);
    [SerializeField] private AudioClip shootSound;
    public AudioClip ShootSound => shootSound;
    [SerializeField] private Vector2 velocity;
    [SerializeField] private float currentSpeed;

    private void Start()
    {
        currentSpeed = projectileType.speed;
        direction = new Vector2(1, 0);
        Destroy(gameObject, projectileType.lifeSpan);
    }

    private void Update()
    {
        velocity = direction * currentSpeed;
    }

    private void FixedUpdate()
    {
        Vector2 position = transform.position;
        position += velocity * Time.fixedDeltaTime;
        transform.position = position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable enemy = collision.collider.gameObject.GetComponent<IDamageable>();
        if (enemy != null)
        {
            enemy.TakeDamage(projectileType.damage);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        Hazard hazard = collision.gameObject.GetComponent<Hazard>();
        if (hazard != null)
        {
            hazard.TakeDamage(projectileType.damage);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
