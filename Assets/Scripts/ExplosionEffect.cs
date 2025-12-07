using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    [SerializeField] private float lifeSpan = 1.5f;

    private void Start()
    {
        // Or destroy after animation is finished playing
        Destroy(gameObject, lifeSpan);
    }
}
