using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;  // Assuming you are using TextMeshPro for text UI

public class EndScene : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI endMessageText;  // Reference to the TextMeshPro component
    [SerializeField] private int totalCoins = 56;  // Total coins in the game

    private void Start()
    {
        // Display the message with the current coins collected
        DisplayEndMessage();
    }

    private void DisplayEndMessage()
    {
        // Get the number of coins collected from the GameManager
        int coinsCollected = GameManager.coinsCollected;

        // Set the text to display something like: "Good work! You collected X/56 coins."
        endMessageText.text = $"Good work! You collected {coinsCollected}/{totalCoins} coins.";
    }

    // Retry button logic: go to Level1Scene
    public void RetryLevel()
    {
        // Load the Level1Scene
        SceneManager.LoadScene("Level1Scene");
    }

    // Quit button logic: quit the game or go to the main menu
    public void QuitGame()
    {
        // Option 1: Quit the game (works in a built game, not in the editor)
        Application.Quit();

        // Option 2: Load the main menu scene (if you have one)
        // SceneManager.LoadScene("StartMenuScene");
    }
}
