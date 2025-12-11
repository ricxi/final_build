using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour, IDamageable
{
    public event System.Action<Asteroid> OnAsteroidIsDestroyed;
    [SerializeField] private int healthPoints = 7;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioClip explosionAudioClip;
    [SerializeField] private CapsuleCollider2D capsuleCollider;
    [SerializeField] private int damageThreshold = 5;
    private bool _isDestroyed = false;

    public void TakeDamage(int damage)
    {
        if (!_isDestroyed)
        {
            healthPoints -= damage;
            if (healthPoints <= 0)
            {
                _isDestroyed = true;
                capsuleCollider.enabled = false;
                _animator.SetTrigger("isDestroyed");
                AudioManager.Instance.PlayOneShot(explosionAudioClip);
                OnAsteroidIsDestroyed?.Invoke(this);
                Destroy(gameObject, explosionAudioClip.length);
            }

            if (healthPoints <= damageThreshold)
            {
                _animator.SetTrigger("isDamaged");
            }
        }
    }
}
