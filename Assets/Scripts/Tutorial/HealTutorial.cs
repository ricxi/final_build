using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealTutorial : MonoBehaviour
{
    [SerializeField] private TutorialEnemy enemy;
    [SerializeField] private GameObject[] gameObjects; // HintCollider will also be stored here.

    void Awake()
    {
        if (enemy != null)
            enemy.OnEnemyIsDead += RemoveTutorialObjects;
    }

    void OnDestroy()
    {
        if (enemy != null)
            enemy.OnEnemyIsDead -= RemoveTutorialObjects;
    }

    public void RemoveTutorialObjects()
    {
        foreach (GameObject tut in gameObjects)
        {
            Destroy(tut);
        }
    }
}
