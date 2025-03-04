using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPaused = false;
    

    public void PauseGame()
    {

        Time.timeScale = 0;
        isPaused = true;
        pauseMenu.GetComponent<Image>().enabled = true;
        foreach (Transform child in pauseMenu.transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        pauseMenu.GetComponent<Image>().enabled = false;
        foreach (Transform child in pauseMenu.transform)
        {
            child.gameObject.SetActive(false);
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    private void OnGUI()
    {
        Event e = Event.current;
        if (e.type == EventType.KeyDown && e.keyCode == KeyCode.Escape)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
}
