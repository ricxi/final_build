using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour, IInteractable
{
    [SerializeField] AudioClip lockedAudioClip;
    [SerializeField] AudioClip unlockedAudioClip;
    [SerializeField] private GameObject gate;
    [SerializeField] private LockType lockType;
    [SerializeField] private string helpText = "It looks like you need a key.";
    [SerializeField] private string displayText = "Press \'X\' to interact with lock.";

    public void Interact(GameObject interactor)
    {
        PlayerInventory inventory = interactor.GetComponent<PlayerInventory>();
        if (inventory != null)
        {
            if (inventory.HasKey(lockType.keyCode)) Unlock();
            else
            {
                AudioManager.Instance.PlayOneShotWithDelay(lockedAudioClip);
                if (PlayerUIHandler.Instance != null) PlayerUIHandler.Instance.PauseAndOpenDisplayWindow(helpText);
            }
        }
    }

    public void Unlock()
    {
        Destroy(gate); // I could also just destroy it.
        AudioManager.Instance.PlayOneShot(unlockedAudioClip);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (PlayerUIHandler.Instance != null)
                PlayerUIHandler.Instance.DisplayText(displayText, 2f);
        }
    }
}
