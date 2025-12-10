using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private string resetSceneName = "TutorialLevel";

    public int Score => score;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        score = 0;
        PlayerUIHandler.Instance.UpdateScore(score);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode __)
    {
        if (scene.name == resetSceneName) return;
        PlayerUIHandler.Instance.UpdateScore(score);
    }

    public void UpdateScore(int points)
    {
        score += points;
        PlayerUIHandler.Instance.UpdateScore(score);
    }
}
