using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateDoor : MonoBehaviour
{
    public void Unlock()
    {
        // Play an animation
        Destroy(gameObject);
    }
}
