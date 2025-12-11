using UnityEngine;

[CreateAssetMenu(fileName = "KeyType", menuName = "Scriptable Object/Key Type")]
public class KeyType : CollectableItemType
{
    public string targetLockName;
    public string code;
}
