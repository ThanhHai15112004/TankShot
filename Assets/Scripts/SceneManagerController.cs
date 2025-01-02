using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerController : MonoBehaviour
{
    public void StartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void NextGame()
    {
        // Chuyển sang màn tiếp theo
        Time.timeScale = 1;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        // Thoát game
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

    public void ShowMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
