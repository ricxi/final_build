using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIHandler : MonoBehaviour
{
    public static PlayerUIHandler Instance;

    [SerializeField] private TMP_Text helperText;
    [SerializeField] private TMP_Text showPointsCollected;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Transform heartContainerPanel;
    [SerializeField] private Image weaponImageUI;
    [SerializeField] private Image heartImagePrefab;
    private List<Image> heartImages = new List<Image>(); // Not sure if this should be instantiated in Start method
    private Coroutine displayAndRemoveTextCoHandler;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    public void DisplayText(string textToDisplay, float duration)
    {
        if (displayAndRemoveTextCoHandler != null)
        {
            StopCoroutine(displayAndRemoveTextCoHandler);
            displayAndRemoveTextCoHandler = null;
        }

        displayAndRemoveTextCoHandler = StartCoroutine(DisplayAndRemoveText(textToDisplay, duration));
    }

    public IEnumerator DisplayAndRemoveText(string textToDisplay, float duration)
    {
        helperText.text = textToDisplay;
        yield return new WaitForSeconds(duration);
        helperText.text = "";
    }

    public void UpdateWeaponImage(Sprite sprite)
    {
        weaponImageUI.sprite = sprite;
    }

    // Must be called by Start method of PlayerScore
    public void UpdateScore(int score)
    {
        scoreText.text = "" + score;
    }

    // Must be called by Start method of PlayerHealth
    // because it creates the heart containers.
    public void BuildHeartContainers(int maxHealth)
    {
        foreach (Image heart in heartImages)
        {
            Destroy(heart.gameObject);
        }
        heartImages.Clear();

        for (int i = 0; i < maxHealth; i++)
        {
            Image newHeart = Instantiate(heartImagePrefab, heartContainerPanel);
            heartImages.Add(newHeart);
        }
    }

    public void UpdateHealth(int currentHealth)
    {
        for (int i = 0; i < heartImages.Count; i++)
        {
            if (i < currentHealth)
            {
                heartImages[i].enabled = true;
            }
            else
            {
                heartImages[i].enabled = false;
            }
        }
    }
}
