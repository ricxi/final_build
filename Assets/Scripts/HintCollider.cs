using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintCollider : MonoBehaviour
{
    [SerializeField] private PlayerUIHandler playerUI;
    // [SerializeField] private string helpText;

    private void Start()
    {
        if (playerUI == null) Debug.LogError("Reference to Player UI is missing");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerUI.DisplayTextToPlayer("Press \'Z\' to interact with the gate.", 2f);
    }
}
