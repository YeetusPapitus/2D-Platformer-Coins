using System.Collections;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText; // Reference to the TextMeshProUGUI component for coin count display
    [SerializeField] private GameObject[] LivesCounter;

    private static HUD instance;
    private AlienController alienController;

    private void Start()
    {
        Debug.Log("HUD Start method called. Coin count: " + GameManager.coinsCollected);
        // Assign the AlienController and set the coin count
        AssignAlienController();
        // Ensure coin counter is displayed correctly
        CollectCoin(GameManager.coinsCollected);
    }

    private void Awake()
    {
        if (instance != null)
        {
            // If the instance already exists, destroy the new duplicate instance.
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);  // Persist HUD across scenes
    }

    private void OnDestroy()
    {
        // Unsubscribe from events to prevent memory leaks
        if (alienController != null)
        {
            alienController.OnCoinCollected -= CollectCoin;
            alienController.OnActiveLivesChange -= UpdateLives;
        }
    }

    private void OnEnable()
    {
        Debug.Log("HUD OnEnable method called. Coin count: " + GameManager.coinsCollected);
        CollectCoin(GameManager.coinsCollected);  // Update the UI when enabled
    }

    public void AssignAlienController()
    {
        GameObject alienObject = GameObject.Find("Alien");
        if (alienObject == null)
        {
            Debug.LogWarning("Alien GameObject not found in the scene.");
            return;
        }

        alienController = alienObject.GetComponent<AlienController>();
        if (alienController == null)
        {
            Debug.LogError("AlienController script not found on the Alien GameObject.");
            return;
        }

        // Subscribe to the OnCoinCollected event only once
        alienController.OnCoinCollected += CollectCoin;

        // Ensure coin count is synchronized with GameManager
        CollectCoin(GameManager.coinsCollected);
        Debug.Log($"Coin count updated in HUD: {GameManager.coinsCollected}");
    }

    // This method will be triggered when a coin is collected
    public void CollectCoin(int coinAmount)
    {
        Debug.Log("Collecting coin. Current coins: " + coinAmount);
        // Directly update the GameManager coin count
        GameManager.coinsCollected = coinAmount;  // Update the GameManager with the new count
        UpdateUI();
    }

    // Update the UI to show the current coin count
    public void UpdateUI()
    {
        Debug.Log("Updated UI text to: Coins Collected: " + GameManager.coinsCollected);
        coinText.text = "Coins Collected: " + GameManager.coinsCollected; // Update the coin counter in the UI
    }

    // Update lives UI based on active lives
    private void UpdateLives(int activeLives)
    {
        if (activeLives < 0 || activeLives > LivesCounter.Length)
            return;

        for (int i = 0; i < LivesCounter.Length; i++)
        {
            LivesCounter[i].SetActive(false);
        }
        for (int i = activeLives; i >= 0; i--)
        {
            LivesCounter[i].SetActive(true);
        }
    }
}
