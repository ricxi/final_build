using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyController : MonoBehaviour, IDamageable
{
    [SerializeField] public EnemyType enemyType;
    [SerializeField] public AggroTrigger aggroCollider;
    // [SerializeField] private CameraShake cameraShake; // Assign this in the Inspector
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private AudioClip deathAudioClip;
    [SerializeField] private GameObject explosionPrefab;

    private PlayerScore playerScore;
    private Transform playerTransform;
    private bool isAggroed = false;
    private int currentHealth;
    private bool isDead = false;
    // private Camera mainCamera;

    private void Start()
    {
        currentHealth = enemyType.maxHealth;

        playerScore = GameObject.Find("Player").GetComponent<PlayerScore>();

        // if (cameraShake == null)
        // {
        //     cameraShake = GameObject.FindWithTag("MainCamera").GetComponent<CameraShake>();
        // }

        if (aggroCollider != null)
            aggroCollider.OnAggroTriggerEnter2D += ChasePlayer;
    }

    private void Update()
    {
        if (isAggroed && playerTransform != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, enemyType.speed * Time.deltaTime);
        }
    }

    public void ChasePlayer(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            // if (!isAggroed)
            //     cameraShake.TriggerShake(1.5f, 0.8f);

            isAggroed = true;
            playerTransform = collision.transform;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
        if (player != null && !isDead)
        {
            player.TakeDamage(enemyType.damage);
        }

    }

    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                isDead = true;
                playDeathSequence();
            }
        }
    }

    private void playDeathSequence()
    {
        playerScore.UpdateScore(enemyType.points);
        // cameraShake.TriggerShake(deathAudioClip.length - 0.5f, 1f);
        AudioManager.Instance.InterruptBgMusic(deathAudioClip);
        GameObject explosionFx = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosionFx, 1.2f);
        Destroy(gameObject);
    }
}
