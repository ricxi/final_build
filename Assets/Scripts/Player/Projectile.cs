using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileType projectileType;
    [SerializeField] private GameObject explosionPrefab; // animation for enemy explosion
    [SerializeField] private Vector2 direction;
    [SerializeField] private Vector2 velocity;
    [SerializeField] private float currentSpeed;
    [SerializeField] private int currentDamage;
    [SerializeField] private AudioClip shootSound;

    public AudioClip ShootSound => shootSound;

    public Sprite GetSprite() => projectileType.sprite;

    private void Start()
    {
        currentSpeed = projectileType.speed;
        currentDamage = projectileType.damage;
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
        PlayerAttack player = collision.gameObject.GetComponent<PlayerAttack>();
        if (player != null)
        {
            currentSpeed *= 5;
            currentDamage *= 3;
        }

        IDamageable damageableGo = collision.gameObject.GetComponent<IDamageable>();
        if (damageableGo != null)
        {
            damageableGo.TakeDamage(currentDamage);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        CompositeCollider2D cc2d = collision.gameObject.GetComponent<CompositeCollider2D>();
        if (cc2d != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        TilemapCollider2D tc2d = collision.gameObject.GetComponent<TilemapCollider2D>();
        if (tc2d != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
