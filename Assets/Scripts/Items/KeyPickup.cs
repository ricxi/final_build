using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour, ICollectable
{
    [SerializeField] private KeyType key;

    public void Collect(GameObject collector)
    {
        PlayerInventory inventory = collector.GetComponent<PlayerInventory>();
        if (inventory != null)
        {
            inventory.AddItem(key);
            AudioManager.Instance.PlayOneShot(key.pickupAudioClip);
            Destroy(gameObject);
        }
    }

    public CollectableItemType Collect()
    {
        return key;
    }
}
