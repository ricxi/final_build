using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField] private int healthPoints = 7;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioClip explosionAudioClip;

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
