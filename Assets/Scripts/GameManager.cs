using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        // Singleton pattern to ensure only one GameManager exists
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // Ensures GameManager persists across scenes
    }

    void Start()
    {
        // Load the AudioManagerScene and StartMenuScene additively at the start
        LoadAndUnloadScene("AudioManagerScene");
        LoadAndUnloadScene("StartMenuScene");
    }

    public void LoadAndUnloadScene(string loadScene, string unloadScene = null)
    {
        // Unload the old scene if one is provided
        if (unloadScene != null)
        {
            SceneManager.UnloadSceneAsync(unloadScene);
        }

        // Load the new scene additively so the AudioManagerScene stays loaded
        SceneManager.LoadSceneAsync(loadScene, LoadSceneMode.Additive);
    }

    public void StartNewGame()
    {
        // Load the first level additively and then unload the StartMenuScene
        StartCoroutine(LoadFirstLevelAndUnloadStartMenu());
    }

    // Coroutine to make sure the StartMenuScene is unloaded after Level1Scene is fully loaded
    private IEnumerator LoadFirstLevelAndUnloadStartMenu()
    {
        // Load the first level additively
        LoadAndUnloadScene("Level1Scene");

        // Wait for the Level1Scene to finish loading
        while (!SceneManager.GetSceneByName("Level1Scene").isLoaded)
        {
            yield return null;
        }

        // Now unload the StartMenuScene
        LoadAndUnloadScene("Level1Scene", "StartMenuScene");
    }
}
