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
    [SerializeField] private Image weaponImageUI;
    [SerializeField] private GameObject displayWindowPanel;
    [SerializeField] private TMP_Text displayWindowText;
    [SerializeField] private Button okButton;  // used to close the display window
    [SerializeField] private Transform heartContainerPanel;
    [SerializeField] private Image heartImagePrefab;

    private List<Image> heartImages = new List<Image>(); // Not sure if this should be instantiated in Start method
    private Coroutine _displayAndRemoveTextCoHandler;
    private float _previousTimeScale = 1f;
    private bool _isPaused;
    public bool IsPaused => _isPaused;

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

    private void OnEnable()
    {
        if (okButton != null)
            okButton.onClick.AddListener(OnOkButtonClicked);
    }

    private void OnDisable()
    {
        if (okButton != null)
            okButton.onClick.RemoveListener(OnOkButtonClicked);
    }

    private void Start()
    {
        CloseDisplayWindow();
        SetPaused(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) UnPauseAndCloseDisplayWindow();
    }

    public void DisplayText(string textToDisplay, float duration)
    {
        if (_displayAndRemoveTextCoHandler != null)
        {
            StopCoroutine(_displayAndRemoveTextCoHandler);
            _displayAndRemoveTextCoHandler = null;
        }

        _displayAndRemoveTextCoHandler = StartCoroutine(DisplayAndRemoveText(textToDisplay, duration));
    }

    public IEnumerator DisplayAndRemoveText(string textToDisplay, float duration)
    {
        helperText.text = textToDisplay;
        yield return new WaitForSeconds(duration);
        helperText.text = "";
    }

    // The default bullet image is set in the UI
    public void UpdateWeaponImage(Sprite sprite)
    {
        if (weaponImageUI != null)
            weaponImageUI.sprite = sprite;
    }

    // Must be called by Start method of PlayerScore
    public void UpdateScore(int score)
    {
        if (scoreText != null)
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
            if (i < currentHealth) heartImages[i].enabled = true;
            else heartImages[i].enabled = false;
        }
    }


    public void OnOkButtonClicked()
    {
        UnPauseAndCloseDisplayWindow();
    }

    public void PauseAndOpenDisplayWindow(string text)
    {
        OpenDisplayWindow(text);
        PauseGame();
    }

    public void UnPauseAndCloseDisplayWindow()
    {
        CloseDisplayWindow();
        UnPauseGame();
    }

    public void OpenDisplayWindow(string text)
    {
        if (displayWindowPanel != null) displayWindowPanel.SetActive(true);
        displayWindowText.text = text;
        helperText.text = "";
    }

    public void CloseDisplayWindow()
    {
        displayWindowText.text = "";
        if (displayWindowPanel != null) displayWindowPanel.SetActive(false);
    }

    public void PauseGame() => SetPaused(true);
    public void UnPauseGame() => SetPaused(false);

    private void SetPaused(bool paused)
    {
        if (_isPaused == paused) return;
        _isPaused = paused;

        if (paused)
        {
            _previousTimeScale = Time.timeScale;
            Time.timeScale = 0f;
        }
        else Time.timeScale = _previousTimeScale <= 0f ? 1f : _previousTimeScale;
    }
}
