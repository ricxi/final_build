using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public TutorialManager Instance;
    public bool tutorialACompleted;
    public bool tutorialBCompleted;

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


    public void completeTutorialA()
    {
        tutorialACompleted = true;
    }

    public void completeTutorialB()
    {
        tutorialBCompleted = true;
    }
}
