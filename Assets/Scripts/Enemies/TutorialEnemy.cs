using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemy : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private GateDoor gate;
    [SerializeField] private GameObject explosionPrefab;

    private bool _isDead = false;

    private void Start()
    {
        if (enemyType == null) return;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHealth player = collision.collider.gameObject.GetComponent<PlayerHealth>();
        if (player != null && !_isDead)
        {
            _isDead = true;
            player.TakeDamage(enemyType.damage);
            gate.Unlock();
            PlayerUIHandler.Instance.DisplayText("Ouch! It looks like it was an enemy, but no worries.\nJust pick up the heart to heal", 5f);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int __)
    {
    }
}

