using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAudioClips", menuName = "Scriptable Object/Player Audio Clips")]
public class PlayerAudioClips : ScriptableObject
{
    public AudioClip LowHealth;
    public AudioClip CollisionDamage;
    public AudioClip Death;
    public AudioClip ItemPickup;
    public AudioClip ScorePoint;
    public AudioClip Heal;
    public AudioClip EquipShield;
    public AudioClip EquipWeapon;
    public AudioClip Teleport;
}
