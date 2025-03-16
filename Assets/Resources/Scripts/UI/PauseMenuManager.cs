using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPaused = false;

    private InputActions inputActions;

    private void Awake()
    {
        // Initialize the InputActions
        inputActions = new InputActions();
    }

    private void OnEnable()
    {
        // Enable the input actions and subscribe to the Pause event
        inputActions.PlayerActions.Enable();
        inputActions.PlayerActions.OpenPauseMenu.performed += OnPausePerformed;
    }

    private void OnDisable()
    {
        // Disable the input actions and unsubscribe from the Pause event
        inputActions.PlayerActions.OpenPauseMenu.performed -= OnPausePerformed;
        inputActions.PlayerActions.Disable();
    }

    private void OnPausePerformed(InputAction.CallbackContext context)
    {
        // Toggle pause state when the Pause action is performed
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseMenu.GetComponent<Image>().enabled = true;
        Time.timeScale = 0;
        isPaused = true;

        // Activate all child objects of the pause menu
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

        // Deactivate all child objects of the pause menu
        foreach (Transform child in pauseMenu.transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}