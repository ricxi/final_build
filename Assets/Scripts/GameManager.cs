using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Only works on parent objects; will not persist children objects
    [Header("Persistent Game Objects")] public GameObject[] PersistentGameObjects;

    private void Awake()
    {
        // if (Instance != null && Instance != this)
        if (Instance != null)
        {
            DestroyAll(); // Destroy all duplicates if another GameManager instance exists
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyObjectsOnLoad();
        }
    }

    private void DontDestroyObjectsOnLoad()
    {
        foreach (GameObject pgo in PersistentGameObjects)
        {
            if (pgo != null)
            {
                DontDestroyOnLoad(pgo);
            }
        }
    }

    public void DestroyAll()
    {
        foreach (GameObject pgo in PersistentGameObjects)
        {
            Destroy(pgo);
        }
        Destroy(gameObject);
    }
}
