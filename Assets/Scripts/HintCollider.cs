using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintCollider : MonoBehaviour
{
    [SerializeField] private string helpText;
    [SerializeField] private GameObject visualHint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerUIHandler.Instance.DisplayTextToPlayer(helpText, 2f);

            if (visualHint != null)
            {
                visualHint.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (visualHint != null)
            {
                visualHint.SetActive(false);
            }
        }
    }
}
