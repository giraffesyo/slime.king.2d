using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject optionMenu;
    public GameObject player;
    private SlimeKingActions slimeKingActions;
    private void Awake()
    {
        // slimeKingActions = player.GetComponent<SlimeKingActions>();
        slimeKingActions = new SlimeKingActions();
    }
    private void OnEnable()
    {

        slimeKingActions.Player.Pause.performed += PauseGame;
        slimeKingActions.Player.Pause.Enable();

    }
    private void OnDisable()
    {
        slimeKingActions.Player.Pause.performed -= PauseGame;
        slimeKingActions.Player.Pause.Disable();
    }
    private void PauseGame(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (Time.timeScale == 1)
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
                AudioListener.pause = true;
            }
            else
            {
                pauseMenu.SetActive(false);
                optionMenu.SetActive(false);
                AudioListener.pause = false;
                Time.timeScale = 1;
            }
        }
    }
    public void LoadMenu()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        SceneManager.LoadScene("Menu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
