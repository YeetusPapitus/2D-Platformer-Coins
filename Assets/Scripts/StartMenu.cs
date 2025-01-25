using UnityEngine;
using UnityEngine.SceneManagement;  // For loading scenes
using UnityEngine.UI;              // For button functionality

public class StartMenu : MonoBehaviour
{
    public Button newGameButton;
    public Button quitButton;

    void Start()
    {
        // Add listeners to buttons
        newGameButton.onClick.AddListener(StartNewGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    void StartNewGame()
    {
        // Replace "GameScene" with the name of your scene
        SceneManager.LoadScene("Level1Scene");
    }

    void QuitGame()
    {
        // Quit the game
        Application.Quit();
        
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // Stop play mode in Unity editor
        #endif
    }
}
