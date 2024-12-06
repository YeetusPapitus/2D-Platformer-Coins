using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public enum MainMenuState { Open, Closed}
    [SerializeField] private MainMenuState menuState;
    private Transform[] buttons;
    // Start is called before the first frame update
    void Start()
    {   Time.timeScale = 1f; 
        buttons = GetComponentsInChildren<Transform>();
        
        CloseMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {   
            if (menuState == MainMenuState.Open)
            {
                CloseMenu();
            }
            else
            {
                OpenMenu();
            }
        }
    }

    public void OpenMenu()
    {
        Time.timeScale = 0f;  
        foreach(var button in buttons)
                {
                    button.gameObject.SetActive(true);

                }
                menuState = MainMenuState.Open;
    }

    public void CloseMenu()
    {
        Time.timeScale = 1f;  
        foreach(var button in buttons)
        {
            if(button.gameObject != gameObject)
            button.gameObject.SetActive(false);
        }
        menuState = MainMenuState.Closed;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
