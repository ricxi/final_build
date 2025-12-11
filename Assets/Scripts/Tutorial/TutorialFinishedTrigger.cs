using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFinishedTrigger : MonoBehaviour
{
    public event System.Action OnEnterCollider;

    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            OnEnterCollider?.Invoke();
        }
    }
}
