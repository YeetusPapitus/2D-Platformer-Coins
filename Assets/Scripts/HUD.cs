using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinCounter;

    [SerializeField] private AlienController alienController;

    [SerializeField] private GameObject[] LivesCounter;

    private void Awake()
    {
        alienController.OnCoinCollected += CollectCoin;
        alienController.OnActiveLivesChange += UpdateLives;
    }

    private void CollectCoin(int curCoins)
    {
        coinCounter.text = "Coins Collected: " + curCoins.ToString();
    }

    private void UpdateLives(int activeLives)
    {
        if(activeLives < 0 || activeLives > LivesCounter.Length)
            return;

        for(int i = 0; i < LivesCounter.Length; i++)
        {
            LivesCounter[i].SetActive(false);
        }
        for(int i = activeLives; i >= 0; i--)
        {
            LivesCounter[i].SetActive(true);
        }
    } 
}
