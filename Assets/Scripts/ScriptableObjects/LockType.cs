using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LockType", menuName = "Scriptable Object/Lock Type")]
public class LockType : ScriptableObject
{
    public string lockId;
    public string lockName;
    public string keyCode;
}
