using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour, IInteractable
{
    [SerializeField] AudioClip audioClip;
    [SerializeField] private GateDoor gate;

    public void Interact()
    {
        gate.Unlock();
        AudioManager.Instance.PlayOneShot(audioClip);
        Destroy(gameObject);
    }
}
