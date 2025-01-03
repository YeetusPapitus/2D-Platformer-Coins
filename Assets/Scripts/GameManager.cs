using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LoadAndUnloadScene("Level1Scene");
    }

    public void LoadAndUnloadScene(string loadScene, string unloadScene = null)
    {
        if(unloadScene != null)
            SceneManager.UnloadSceneAsync(unloadScene);

        SceneManager.LoadSceneAsync(loadScene, LoadSceneMode.Additive);
    }
}
