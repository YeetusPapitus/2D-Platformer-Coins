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
    }

    public void EndLevel()
    {
        gameManager.LoadAndUnloadScene(nextScene, curScene);
    }
}
