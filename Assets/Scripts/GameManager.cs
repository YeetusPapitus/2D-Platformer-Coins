using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        LoadSceneAdditively("AudioManagerScene");
        LoadSceneAdditively("StartMenuScene");
    }

    public void LoadSceneAdditively(string sceneName)
    {
        Debug.Log($"Loading scene: {sceneName}");
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
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
        StartCoroutine(LoadFirstLevelAndUnloadStartMenu());
    }

    private IEnumerator LoadFirstLevelAndUnloadStartMenu()
    {
        LoadSceneAdditively("Level1Scene");

        while (!SceneManager.GetSceneByName("Level1Scene").isLoaded)
        {
            yield return null;
        }

        UnloadScene("StartMenuScene");
    }
}
