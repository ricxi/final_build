using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemy : MonoBehaviour, IDamageable
{
    public event System.Action OnEnemyIsDead;
    [SerializeField] private EnemyType enemyType;

    [TextArea(minLines: 3, maxLines: 3)]
    [SerializeField]
    private string tutorialText = "Ouch!!\nLooks like it was a foe.\nGrab the heart nearby to heal.";
    [TextArea(minLines: 2, maxLines: 2)]
    [SerializeField]
    private string helpText = "Friend or foe?\nCollide to find out.";
    [SerializeField] private GateDoor gate;
    [SerializeField] private GameObject explosionPrefab;

    private Coroutine _pauseRoutineCoHandler;
    private bool _isDead = false;
    private int _accumulatedDamage = 0;

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
            OnEnemyIsDead?.Invoke();

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
        if (PlayerUIHandler.Instance != null) PlayerUIHandler.Instance.PauseAndOpenDisplayWindow(tutorialText);
        Destroy(gameObject);
    }

    // Plays a message if the player keeps attacking the enemy
    public void TakeDamage(int _)
    {
        _accumulatedDamage += 1;
        if (_accumulatedDamage >= 3)
        {
            if (PlayerUIHandler.Instance != null) PlayerUIHandler.Instance.DisplayText(helpText, 2f);
            _accumulatedDamage = 0;
        }
    }
}

