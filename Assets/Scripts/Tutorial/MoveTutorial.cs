using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTutorial : MonoBehaviour
{
    [SerializeField] private TutorialFinishedTrigger finishedTrigger;
    [SerializeField] private GameObject[] gameObjects; // HintCollider will also be stored here.

    private void Awake()
    {
        if (finishedTrigger != null)
            finishedTrigger.OnEnterCollider += RemoveTutorialObjects;
    }
    private void OnDestroy()
    {
        if (finishedTrigger != null)
            finishedTrigger.OnEnterCollider -= RemoveTutorialObjects;
    }

    public void RemoveTutorialObjects()
    {
        foreach (GameObject tut in gameObjects)
        {
            Destroy(tut);
        }
    }
}
