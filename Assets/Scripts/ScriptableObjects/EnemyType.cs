using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyType", menuName = "Scriptable Object/Enemy Type")]
public class EnemyType : ScriptableObject
{
    public int maxHealth;
    public int damage;
    public float speed;
    public int points;
    public bool followPlayer;
}
