using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTutorial : MonoBehaviour
{
    [SerializeField] private Transform Asteroids;
    [SerializeField] private GameObject[] gameObjects; // HintCollider will also be stored here.
    [SerializeField] private string tutorialText = "Shoot to destroy enemies or obstacles.";
    private Asteroid[] _asteroids;
    private bool _hasShownMessage = false;

    void Awake()
    {
        _asteroids = Asteroids.GetComponentsInChildren<Asteroid>();

        foreach (var asteroid in _asteroids)
        {
            if (asteroid != null)
                asteroid.OnAsteroidIsDestroyed += handleAsteroidDestroyed;
        }
    }

    void OnDestroy()
    {
        if (_asteroids == null || _asteroids.Length == 0) return;

        foreach (var asteroid in _asteroids)
        {
            if (asteroid != null)
                asteroid.OnAsteroidIsDestroyed -= handleAsteroidDestroyed;
        }
    }

    private void handleAsteroidDestroyed(Asteroid asteroid)
    {
        if (!_hasShownMessage)
        {
            _hasShownMessage = true;
            if (PlayerUIHandler.Instance != null) PlayerUIHandler.Instance.PauseAndOpenDisplayWindow(tutorialText);
            foreach (GameObject tut in gameObjects)
            {
                Destroy(tut);
            }
        }
    }
}
