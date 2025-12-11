using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval = 7f;
    // [SerializeField] private BoxCollider2D spawnTrigger;

    private float _timer;

    private void Start()
    {
        _timer = spawnInterval;
    }
    private void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0f)
        {
            SpawnEnemy();
            _timer = spawnInterval;
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefab == null) return;
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}
