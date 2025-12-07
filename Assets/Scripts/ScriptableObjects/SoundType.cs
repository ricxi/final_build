using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundType", menuName = "Scriptable Object/Sound Type")]
public class SoundType : ScriptableObject
{
    public AudioClip LowHealth;
    public AudioClip CollisionDamage;
    public AudioClip Death;
    public AudioClip ItemPickup;
    public AudioClip ScorePoint;
    public AudioClip Heal;
    public AudioClip EquipShield;
    public AudioClip EquipWeapon;
}
