using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text helperText;
    [SerializeField] private TMP_Text showPointsCollected;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Transform heartContainerPanel;
    [SerializeField] private Image heartImagePrefab;
    private List<Image> heartImages = new List<Image>(); // Not sure if this should be instantiated in Start method
    private Coroutine displayAndRemoveTextCoHandler;

    public void DisplayTextToPlayer(string textToDisplay, float duration)
    {
        if (displayAndRemoveTextCoHandler != null)
        {
            StopCoroutine(displayAndRemoveTextCoHandler);
            displayAndRemoveTextCoHandler = null;
        }

        displayAndRemoveTextCoHandler = StartCoroutine(DisplayAndRemoveText(textToDisplay, duration));
    }

    public IEnumerator DisplayAndRemoveText(string textToDisplay, float lifetime)
    {
        helperText.text = textToDisplay;
        yield return new WaitForSeconds(5);
        helperText.text = "";
    }

    // Must be called by Start method of PlayerScoreManager
    public void UpdateScore(int score)
    {
        scoreText.text = "" + score;
    }

    // Must be called by Start method of PlayerHealthManager
    public void SetMaxHealth(int maxHealth)
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
