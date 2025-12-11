using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform teleportTarget;
    [SerializeField] private AudioClip audioClip;

    private Coroutine _delayTeleportCoHandler = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            player.ActivateShield(3f);
            player.FreezeMovement(1.2f);

            PlayerUIHandler.Instance.DisplayText("teleporting!!", 2f);
            AudioManager.Instance.PlayOneShotWithDelay(audioClip);

            Teleport(player.transform);
        }
    }

    private void Teleport(Transform playerTransform)
    {
        if (_delayTeleportCoHandler != null)
        {
            StopCoroutine(_delayTeleportCoHandler);
            _delayTeleportCoHandler = null;
        }
        _delayTeleportCoHandler = StartCoroutine(delayTeleport(playerTransform, 1f));
    }

    private IEnumerator delayTeleport(Transform playerTransform, float duration)
    {
        yield return new WaitForSeconds(duration);
        if (teleportTarget != null) playerTransform.position = teleportTarget.position;
        else Debug.LogError("Missing teleportTarget reference");
    }
}
