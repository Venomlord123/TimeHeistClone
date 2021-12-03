using Luke;
using UnityEngine;

public class Pause : MonoBehaviour
{
    //References 
    public MouseCursor mouseCursor;
    
    //Variables
    public GameObject pauseMenu;
    public bool isPaused;

    public void PauseGame()
    {
        if (isPaused == false)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            mouseCursor.EnableMouse();
        }
    }

    public void UnpauseGame()
    {
        if (isPaused)
        {
            pauseMenu.SetActive(false);
            mouseCursor.DisableMouse();
            Time.timeScale = 1;
        }
    }
}