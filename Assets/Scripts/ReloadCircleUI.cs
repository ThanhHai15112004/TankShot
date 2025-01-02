using UnityEngine;
using UnityEngine.UI;

public class ReloadCircleUI : MonoBehaviour
{
    [Header("References")]
    public Image reloadCircle;
    public PlayerTankShooting playerTankShooting;

    private void Update()
    {
        if (!playerTankShooting.CanShoot())
        {
            reloadCircle.gameObject.SetActive(true);
            reloadCircle.fillAmount = playerTankShooting.GetReloadProgress();
        }
        else
        {
            reloadCircle.gameObject.SetActive(false);
        }
    }
}
