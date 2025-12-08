using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour, IInteractable
{
    //   [SerializeField] AudioClips audioClips;
    [SerializeField] private GateDoor gate;

    public void Interact()
    {
        gate.Unlock();
        // AudioManager.Instance.PlayOneShot(audioClips.LockedDoor);
        Destroy(gameObject);
    }
}
