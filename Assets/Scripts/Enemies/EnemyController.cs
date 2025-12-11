using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyController : MonoBehaviour, IDamageable
{
    public EnemyType enemyType;
    private PlayerScore playerScore;
    private Transform playerTransform;

    private int _currentHealth;
    private bool _isDead = false;

    private void Start()
    {
        if (enemyType == null) return;
        _currentHealth = enemyType.maxHealth;

        if (enemyType.followPlayer)
            playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerScore = GameObject.Find("Player").GetComponent<PlayerScore>();
    }

    private void Update()
    {
        if (playerTransform != null && enemyType.followPlayer)
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, enemyType.speed * Time.deltaTime);
        else
            transform.Translate(Vector2.left * enemyType.speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHealth player = collision.collider.gameObject.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.TakeDamage(enemyType.damage);
        }

        TilemapCollider2D td = collision.gameObject.GetComponent<TilemapCollider2D>();
        if (td != null)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        if (!_isDead)
        {
            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                _isDead = true;
                playerScore.UpdateScore(enemyType.points);
                Destroy(gameObject);
            }
        }
    }
}
