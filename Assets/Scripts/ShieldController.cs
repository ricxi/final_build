using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: not sure if this is still being used.
public class ShieldController : MonoBehaviour
{
    private float delayAfterExit = 1.5f;

    private void OnTriggerExit2D(Collider2D collision)
    {
        Destroy(gameObject, delayAfterExit);
    }
}
