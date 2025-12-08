using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintCollider : MonoBehaviour
{
    // [SerializeField] private string helpText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerUIHandler.Instance.DisplayTextToPlayer("Press \'X\' to interact with the gate.", 2f);
    }
}
