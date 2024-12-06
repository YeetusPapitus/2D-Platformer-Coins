using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinCounter;

    [SerializeField] private AlienController alienController;

    private void Awake()
    {
        alienController.OnCoinCollected += CollectCoin;
    }

    public void CollectCoin(int curCoins)
    {
        coinCounter.text = "Coins Collected: " + curCoins.ToString();
    }   
}
