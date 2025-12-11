using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemy : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private string tutorialText = "Ouch!!\nLooks like it was an enemy.\nGrab the heart nearby to heal.";
    [SerializeField] private GateDoor gate;
    [SerializeField] private GameObject explosionPrefab;

    private Coroutine _pauseRoutineCoHandler;
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
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            if (_pauseRoutineCoHandler != null)
            {
                StopCoroutine(_pauseRoutineCoHandler);
                _pauseRoutineCoHandler = null;
            }
            _pauseRoutineCoHandler = StartCoroutine(PauseAfterExplosion(0.3f));
        }
    }

    private IEnumerator PauseAfterExplosion(float duration)
    {
        yield return new WaitForSecondsRealtime(duration);
        if (PlayerUIHandler.Instance != null)
            PlayerUIHandler.Instance.PauseAndOpenDisplayWindow(tutorialText);
        Destroy(gameObject);
    }

    public void TakeDamage(int __) { }
}

