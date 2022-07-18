using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MenuUIHandler
{
    [SerializeField] GameObject pauseScreen;
    private void Update()
    {
        PauseScreen();
    }
    private void PauseScreen()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            pauseScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            MouseLook.isPauseScreenActive = true;
            Time.timeScale = 0;
        }
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        MouseLook.isPauseScreenActive = false;
    }
}
