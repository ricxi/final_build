using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour, IDamageable
{
    [SerializeField] private int healthPoints = 7;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioClip explosionAudioClip;
    [SerializeField] private CapsuleCollider2D capsuleCollider;

    private int damageThreshold = 5;
    private bool isDestroyed = false;

    public void TakeDamage(int damage)
    {
        if (!isDestroyed)
        {
            healthPoints -= damage;
            if (healthPoints <= 0)
            {
                isDestroyed = true;
                capsuleCollider.enabled = false;
                _animator.SetTrigger("isDestroyed");
                AudioManager.Instance.PlayOneShot(explosionAudioClip);
                Destroy(gameObject, explosionAudioClip.length);
            }

            if (healthPoints <= damageThreshold)
            {
                _animator.SetTrigger("isDamaged");
            }
        }
    }
}
