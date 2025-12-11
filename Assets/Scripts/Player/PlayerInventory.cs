using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private List<CollectableItemType> _items = new List<CollectableItemType>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ICollectable collectable = collision.gameObject.GetComponent<ICollectable>();
        if (collectable != null) collectable.Collect(gameObject); // I am passing PlayerInventory to the class that implements ICollectable
    }

    public void AddItem(CollectableItemType item)
    {
        if (item == null) return;
        _items.Add(item);
    }

    public bool HasKey(string keyCode)
    {
        foreach (var item in _items)
        {
            var key = item as KeyType;
            if (key != null && key.code == keyCode)
            {
                return true;
            }
        }
        return false;
    }

    public bool HasItem(string itemId)
    {
        foreach (var i in _items)
        {
            if (i.id == itemId)
                return true;
        }
        return false;
    }
}
