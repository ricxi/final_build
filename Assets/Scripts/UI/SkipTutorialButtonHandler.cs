using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipTutorialButtonHandler : MonoBehaviour
{
    [SerializeField] private AudioClip clickButtonAudioClip;
    [SerializeField] private string FirstLevelSceneName = "LevelOne";
    public void OnSkipTutorialButtonClicked()
    {
        StartCoroutine(delayLoadScene(FirstLevelSceneName));
    }

    private IEnumerator delayLoadScene(string sceneName)
    {
        AudioManager.Instance.PlayOneShot(clickButtonAudioClip);
        yield return new WaitForSeconds(clickButtonAudioClip.length);
        SceneManager.LoadScene(sceneName);
    }
}
