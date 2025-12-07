using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileType", menuName = "Scriptable Object/Projectile Type")]
public class ProjectileType : ScriptableObject
{
    public int id;
    public float speed;
    public float lifeSpan;
    public int damage;
}
