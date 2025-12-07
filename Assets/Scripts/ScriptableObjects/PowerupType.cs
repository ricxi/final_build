using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerupType", menuName = "Scriptable Object/Powerup Type")]
public class PowerupType : ScriptableObject
{
    // ids:
    // 10 = health
    // 11 = defense/shield
    // 12 = offense/attack?
    public int id;
    public string type;
}
