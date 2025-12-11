using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTutorial : MonoBehaviour
{
    [SerializeField] private CoinPickup[] Coins;
    [SerializeField] private HintCollider hintCollider;

    private int _coinsRemaining;

    private void Awake()
    {
        _coinsRemaining = Coins.Length;
        foreach (CoinPickup coin in Coins)
        {
            coin.OnCoinDestroyed += removeHintCollider;
        }
    }

    private void OnDestroy()
    {
        if (Coins == null || Coins.Length == 0) return;

        foreach (var coin in Coins)
        {
            if (coin != null)
                coin.OnCoinDestroyed -= removeHintCollider;
        }
    }

    private void removeHintCollider(CoinPickup coin)
    {
        _coinsRemaining--;
        if (_coinsRemaining <= 0)
        {
            Destroy(hintCollider);
        }
    }
}
