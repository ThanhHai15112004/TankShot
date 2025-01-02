using UnityEngine;

public class Coin : MonoBehaviour
{
    private static int totalCoins = 0; // Tổng số coin thu thập

    private void Start()
    {
        UpdateCoinUI();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            totalCoins++; // Cộng coin
            UpdateCoinUI();
            Destroy(gameObject); // Xóa đối tượng coin
        }
    }

    private void UpdateCoinUI()
    {
        UIManager uiManager = FindObjectOfType<UIManager>();
        if (uiManager != null)
        {
            uiManager.UpdateCoinCount(totalCoins);
        }
    }
}
