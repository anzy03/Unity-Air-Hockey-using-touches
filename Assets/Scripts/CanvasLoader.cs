using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasLoader : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject playCanvas;

    /* public void start()
    {
        
    } */

    public void PauseMenuButton()
    {
        if(!pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(true);
            playCanvas.SetActive(false);
            Time.timeScale = 0;
        }
    }

    public void ResumeButton()
    {
        if(pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
            playCanvas.SetActive(true);
            Time.timeScale = 1;
        }
    }
    public void ExitButton()
    {
        if(pauseMenu.activeSelf)
        {
            playCanvas.SetActive(true);
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            SceneManager.LoadScene(0);

        }
    }
}
