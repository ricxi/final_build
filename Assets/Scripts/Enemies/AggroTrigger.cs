using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroTrigger : MonoBehaviour
{
    public event System.Action<Collider2D> OnAggroTriggerEnter2D;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnAggroTriggerEnter2D?.Invoke(collision);
    }
}
