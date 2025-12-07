using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] private int score;
    public int Score => score;
    private PlayerUIHandler playerUI;

    private void Start()
    {
        score = 0;
        playerUI = GameObject.Find("PlayerUIManager").GetComponent<PlayerUIHandler>();
        playerUI.UpdateScore(score);
    }

    public void UpdateScore(int points)
    {
        score += points;
        playerUI.UpdateScore(score);
    }

}
