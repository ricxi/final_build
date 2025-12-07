using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionUIHandler : MonoBehaviour
{
    [SerializeField] private string FirstLevelSceneName;
    [SerializeField] private string StartScreenSceneName;

    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene(FirstLevelSceneName);
    }

    public void OnMainMenuButtonClicked()
    {
        SceneManager.LoadScene(StartScreenSceneName);
    }

}
