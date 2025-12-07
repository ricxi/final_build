using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUIHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private string startScreenSceneName;
    [SerializeField] private string firstLevelSceneName;
    private int finalScore;

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            foreach (var pgo in GameManager.Instance.PersistentGameObjects)
            {
                if (pgo.name == "Player")
                {
                    PlayerScore ps = pgo.GetComponent<PlayerScore>(); // Maybe perform a null check
                    finalScore = ps.Score;
                    finalScoreText.text = "score:\n" + finalScore;
                }
            }
            GameManager.Instance.DestroyAll();
        }
    }

    public void OnRestartButtonClicked()
    {
        SceneManager.LoadScene(firstLevelSceneName);
    }

    public void OnMainMenuButtonClicked()
    {
        SceneManager.LoadScene(startScreenSceneName);
    }

    public void OnExitButtonClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }
}
