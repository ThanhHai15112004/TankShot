using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject gameOverPanel;
    public GameObject winPanel;
    public UIManager uiManager;

    [Header("Game State")]
    private bool isGameOver = false;

    private List<EnemyHealth> enemies = new List<EnemyHealth>();

    [Header("Player")]
    public PlayerHealth playerHealth;

    private void Start()
    {
        // Tắt các panel lúc đầu
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);

        // Lấy danh sách các enemy trong scene
        enemies.AddRange(FindObjectsOfType<EnemyHealth>());

        // Lắng nghe sự kiện chết của Player
        if (playerHealth != null)
        {
            playerHealth.OnHealthChanged += CheckPlayerDeath;
        }
    }

    private void Update()
    {
        if (!isGameOver)
        {
            CheckWinCondition();
        }
    }

    private void CheckPlayerDeath(int currentHealth, int maxHealth)
    {
        if (currentHealth <= 0 && !isGameOver)
        {
            GameOver();
        }
    }

    private void CheckWinCondition()
    {
        enemies.RemoveAll(enemy => enemy == null); // Loại bỏ các enemy đã chết

        if (enemies.Count == 0)
        {
            WinGame();
        }
    }

    private void GameOver()
    {
        isGameOver = true;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0; // Dừng game
    }

    public  void WinGame()
    {
        isGameOver = true;

        winPanel.SetActive(true);
        Time.timeScale = 0; // Dừng game
    }

}
