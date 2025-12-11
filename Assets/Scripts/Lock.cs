using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour, IInteractable
{
    [SerializeField] AudioClip lockedAudioClip;
    [SerializeField] AudioClip unlockedAudioClip;
    [SerializeField] private GateDoor gate;
    [SerializeField] private LockType lockType;
    [SerializeField] private string helpText = "It looks like you need a key.";
    [SerializeField] private int maxUnlockAttempts = 2;

    private int _unlockAttempts = 0;

    public void Interact(GameObject interactor)
    {
        PlayerInventory inventory = interactor.GetComponent<PlayerInventory>();
        if (inventory != null)
        {
            if (inventory.HasKey(lockType.keyCode)) Unlock();
            else
            {
                AudioManager.Instance.PlayOneShotWithDelay(lockedAudioClip);
                if (_unlockAttempts >= maxUnlockAttempts)
                {
                    if (PlayerUIHandler.Instance != null) PlayerUIHandler.Instance.PauseAndOpenDisplayWindow(helpText);
                    _unlockAttempts = 0;
                }
                _unlockAttempts++;
            }
        }
    }

    public void Unlock()
    {
        gate.Unlock(); // I could also just destroy it.
        AudioManager.Instance.PlayOneShot(unlockedAudioClip);
        Destroy(gameObject);
    }
}
