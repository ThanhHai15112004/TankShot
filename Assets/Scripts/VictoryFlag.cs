using UnityEngine;

public class VictoryFlag : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player collided with Flag.");
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                Debug.Log("Calling WinGame...");
                gameManager.WinGame();
            }
            else
            {
                Debug.LogWarning("GameManager not found.");
            }
        }
    }
}
