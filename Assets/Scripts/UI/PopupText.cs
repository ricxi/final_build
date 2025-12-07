using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupText : MonoBehaviour
{
    [SerializeField] private float lifespan = .6f;

    private void Start()
    {
        Destroy(gameObject, lifespan);
    }
}
