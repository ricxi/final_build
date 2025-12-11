using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CollectableKeyType", menuName = "Scriptable Object/Collectable Item Type")]
public class CollectableItemType : ScriptableObject
{
    public string id;
    public string itemName;
    public Sprite sprite;
    public AudioClip pickupAudioClip;
}
