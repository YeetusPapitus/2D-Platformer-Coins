using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endpoint : MonoBehaviour
{
    [SerializeField] private string nextScene;
    [SerializeField] private string curScene;
    private GameManager gameManager;

    // Start is called before the first frame update
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
            gameManager.LoadAndUnloadScene(nextScene, curScene);
        }
        else
        {
            Debug.LogError("GameManager is null, unable to load scenes.");
        }
    }
}
