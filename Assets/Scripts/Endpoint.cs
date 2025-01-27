using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Endpoint : MonoBehaviour
{
    [SerializeField] private string nextScene;
    [SerializeField] private string curScene;
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene! Please make sure it's in the scene.");
        }
    }

    public void EndLevel()
    {
        if (gameManager != null)
        {
            StartCoroutine(EndLevelRoutine());
        }
        else
        {
            Debug.LogError("GameManager is null, unable to load scenes.");
        }
    }

    private IEnumerator EndLevelRoutine()
    {
        gameManager.LoadSceneAdditively(nextScene);

        while (!SceneManager.GetSceneByName(nextScene).isLoaded)
        {
            yield return null;
        }

        gameManager.UnloadScene(curScene);
    }
}
