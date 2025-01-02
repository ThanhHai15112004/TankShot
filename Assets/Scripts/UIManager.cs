using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Coin UI")]
    public Text coinText;

    public void UpdateCoinCount(int count)
    {
        // Cập nhật giá trị hiển thị
        coinText.text = count.ToString();
    }
}
