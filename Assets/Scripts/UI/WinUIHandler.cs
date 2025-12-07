using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinUIHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private string StartScreenSceneName;
    [SerializeField] private string FirstLevelSceneName;

    private void Start()
    {

        if (GameManager.Instance != null)
        {
            foreach (var pgo in GameManager.Instance.PersistentGameObjects)
            {
                if (pgo.name == "Player")
                {
                    PlayerScore ps = pgo.GetComponent<PlayerScore>(); // Maybe perform a null check
                    scoreText.text = "SCORE:\n" + ps.Score;
                }
            }
            GameManager.Instance.DestroyAll();
        }

    }

    public void OnRestartButtonClicked()
    {
        SceneManager.LoadScene(FirstLevelSceneName);
    }

    public void OnMainMenuButtonClicked()
    {
        SceneManager.LoadScene(StartScreenSceneName);
    }

    public void OnExitButtonClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }

}
