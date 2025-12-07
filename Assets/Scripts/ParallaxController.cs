using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [SerializeField] private GameObject cameraTransform;
    [SerializeField] private float speedX = 1; // Determines speed the camera moves relative to the background
    private float startPositionX;
    private float spriteWidth;

    private void Start()
    {
        GameObject mainCamera = GameObject.FindWithTag("MainCamera");
        if (mainCamera != null) cameraTransform = mainCamera;

        startPositionX = transform.position.x;
        spriteWidth = GetComponent<SpriteRenderer>().sprite.bounds.size.x;
    }

    private void LateUpdate()
    {
        float relativeDistanceX = cameraTransform.transform.position.x * speedX;
        float relativeCameraPositionX = cameraTransform.transform.position.x - relativeDistanceX;

        transform.position = new Vector3(startPositionX + relativeDistanceX, transform.position.y, transform.position.z);

        if (relativeCameraPositionX > startPositionX + spriteWidth)
        {
            startPositionX += spriteWidth;
        }
        else if (relativeCameraPositionX < startPositionX - spriteWidth)
        {
            startPositionX -= spriteWidth;
        }
    }
}
