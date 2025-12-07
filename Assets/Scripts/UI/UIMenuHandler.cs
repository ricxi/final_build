using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenuHandler : MonoBehaviour
{
    [SerializeField] private string FirstLevelSceneName;
    [SerializeField] private string InstructionsSceneName;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clickButtonAudioClip;

    public void OnStartButtonClicked()
    {
        StartCoroutine(delayLoadScene(FirstLevelSceneName));
    }

    public void OnExitButtonClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }

    public void OnInstructionsButtonClicked()
    {
        SceneManager.LoadScene(InstructionsSceneName);
    }

    private IEnumerator delayLoadScene(string sceneName)
    {
        audioSource.PlayOneShot(clickButtonAudioClip);
        yield return new WaitForSeconds(clickButtonAudioClip.length);
        SceneManager.LoadScene(sceneName);
    }
}
