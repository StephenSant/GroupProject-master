using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public GameObject mainMenu, optionMenu;
    public bool optionOn;

    public GameObject pauseMenu, pauseOptions, gamePanel;
    public bool paused;

    private void Start()
    {
        if (GameObject.Find("Main Menu") != null)
        {
            mainMenu = GameObject.Find("Main Menu");
            optionMenu = GameObject.Find("Option Menu");
            mainMenu.SetActive(true);
            optionMenu.SetActive(false);
            optionOn = false;
        }
        else if (GameObject.Find("Pause Menu") != null)
        {
            pauseMenu = GameObject.Find("Pause Menu");
            pauseOptions = GameObject.Find("Pause Options");
            gamePanel = GameObject.Find("Game Panel");
            gamePanel.SetActive(true);
            pauseMenu.SetActive(false);
            pauseOptions.SetActive(false);
            paused = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void GoToScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void ToggleOption()
    {
        if (!optionOn)
        {
            if (mainMenu != null)
            {
                mainMenu.SetActive(false);
                optionMenu.SetActive(true);
            }

            else if (pauseMenu != null)
            {
                pauseMenu.SetActive(false);
                pauseOptions.SetActive(true);
            }

            optionOn = true;
        }
        else if (optionOn)
        {
            if (optionMenu != null)
            {
                mainMenu.SetActive(true);
                optionMenu.SetActive(false);
            }
            else if (pauseOptions != null)
            {
                pauseMenu.SetActive(true);
                pauseOptions.SetActive(false);
            }
                optionOn = false;
            }
    }

    public void TogglePause()
    {
        if (!paused)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            gamePanel.SetActive(false);
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            paused = true;
        }
        else if (paused)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            gamePanel.SetActive(true);
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            paused = false;
            
        }
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

}
