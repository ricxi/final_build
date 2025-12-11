using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowProjectileTip : MonoBehaviour
{
    [TextArea(minLines: 2, maxLines: 2)]
    [SerializeField]
    private string tutorialText = "Running into your own projectiles increases their speed and power";

    private bool _hasShownMessage = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!_hasShownMessage)
        {
            _hasShownMessage = true;
            if (PlayerUIHandler.Instance != null) PlayerUIHandler.Instance.PauseAndOpenDisplayWindow(tutorialText);
        }
    }
}
