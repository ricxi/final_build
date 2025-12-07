using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemySound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManager.Instance.InterruptBgMusic(audioClip);
            Destroy(gameObject);
        }
    }
}
