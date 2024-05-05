using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;
    public GameObject puaseMenuUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused == true)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        puaseMenuUI.SetActive(false);
        isGamePaused = false;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        puaseMenuUI.SetActive(true);
        isGamePaused = true;
    }
}
