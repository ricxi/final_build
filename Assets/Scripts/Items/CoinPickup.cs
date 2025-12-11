using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] private int points = 5;
    [SerializeField] private GameObject popupCanvasPrefab;
    [SerializeField] private AudioClip audioClip;

    public event System.Action<CoinPickup> OnCoinDestroyed;

    private void OnDestroy()
    {
        OnCoinDestroyed?.Invoke(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerScore player = collision.gameObject.GetComponent<PlayerScore>();
        if (player != null)
        {
            ShowPoints();
            AudioManager.Instance.PlayOneShot(audioClip);
            player.UpdateScore(points);
            Destroy(gameObject);
        }
    }

    public void ShowPoints()
    {
        var popup = Instantiate(popupCanvasPrefab, transform.position, Quaternion.identity);
        var popupText = popup.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        popupText.text = "+" + points;
    }
}
