using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintCollider : MonoBehaviour
{
    [TextArea(minLines: 2, maxLines: 2)]
    [SerializeField]
    private string helpText;
    [SerializeField] private GameObject visualHint;

    private void Start()
    {
        if (visualHint != null) visualHint.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerUIHandler.Instance.DisplayText(helpText, 2f);
            if (visualHint != null) visualHint.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) if (visualHint != null) visualHint.SetActive(false);
    }
}
