using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public bool isPaused = false;
    
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        isPaused = !isPaused;
    }
    public void Exit()
    {
        SceneManager.LoadScene("MainMenu_Scene");
        Time.timeScale = 1;
        isPaused = !isPaused;
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        isPaused = !isPaused;
    }
    public void Restart()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        isPaused = !isPaused;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}
