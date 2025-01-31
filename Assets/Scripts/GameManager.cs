using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static int coinsCollected = 0; // Static variable for coins collected

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);  // If a GameManager instance already exists, destroy the new one
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);  // Ensure this instance persists across scenes

        // Destroy duplicate EventSystem if found
        EventSystem[] eventSystems = FindObjectsOfType<EventSystem>();
        if (eventSystems.Length > 1)
        {
            foreach (var eventSystem in eventSystems)
            {
                if (eventSystem != eventSystems[0])  // Keep the first EventSystem
                {
                    Destroy(eventSystem.gameObject);
                }
            }
        }
    }

    void Start()
    {
        LoadSceneAdditively("AudioManagerScene");
        LoadSceneAdditively("StartMenuScene");
    }

    public void LoadSceneAdditively(string sceneName)
    {
        StartCoroutine(LoadAndUpdateHUD(sceneName));
    }

    private IEnumerator LoadAndUpdateHUD(string sceneName)
    {
        // Log to check when this starts
        Debug.Log($"Loading scene: {sceneName}");

        // Load the scene additively
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        
        // Wait for the scene to finish loading
        yield return new WaitUntil(() => asyncLoad.isDone);

        // Ensure the HUD gets updated after the scene has loaded
        StartCoroutine(UpdateHUDAfterSceneLoad());
    }

    public void UnloadScene(string sceneName)
    {
        StartCoroutine(UnloadSceneAsync(sceneName));
    }

    private IEnumerator UnloadSceneAsync(string sceneName)
    {
        AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(sceneName);

        while (!unloadOperation.isDone)
        {
            yield return null;
        }
    }

    public void StartNewGame()
    {
        coinsCollected = 0; // Reset coins when starting a new game
        StartCoroutine(LoadFirstLevelAndUnloadStartMenu());
    }

    private IEnumerator LoadFirstLevelAndUnloadStartMenu()
    {
        LoadSceneAdditively("Level1Scene");

        while (!SceneManager.GetSceneByName("Level1Scene").isLoaded)
        {
            yield return null;
        }

        // Force the HUD to update the coin counter
        var hud = FindObjectOfType<HUD>();
        if (hud != null)
        {
            hud.CollectCoin(coinsCollected);  // Update the HUD with the coin count
        }

        UnloadScene("StartMenuScene");

        // Load Level2 and update the HUD
        LoadSceneAdditively("Level2Scene");

        // Update HUD once Level2 has fully loaded
        yield return new WaitUntil(() => SceneManager.GetSceneByName("Level2Scene").isLoaded);
        var level2HUD = FindObjectOfType<HUD>();
        if (level2HUD != null)
        {
            level2HUD.UpdateUI();  // Update the UI directly to reflect the current coin count
        }
    }

    private IEnumerator UpdateHUDAfterSceneLoad()
    {
        // Wait for the scene to load
        yield return new WaitUntil(() => SceneManager.GetSceneByName("Level2Scene").isLoaded);

        // Find the HUD in the scene
        HUD hud = FindObjectOfType<HUD>();
        if (hud != null)
        {
            // Assign the AlienController (even if it is called multiple times, it should still work)
            hud.AssignAlienController();

            // Force the HUD to update the coin count based on the GameManager's current count
            hud.CollectCoin(coinsCollected);
            Debug.Log($"Coin count updated in HUD after scene load: {coinsCollected}");
        }
    }
}
