using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    [Header("UI Elements")]
    public Transform foregroundSprite;
    public Transform backgroundSprite;

    [Header("References")]
    public BaseHealth health;

    private float initialScaleX;

    private void Start()
    {
        if (foregroundSprite != null)
        {
            initialScaleX = foregroundSprite.localScale.x;
        }
    }

    private void Update()
    {
        if (health != null && foregroundSprite != null)
        {
            float healthPercentage = (float)health.GetCurrentHealth() / health.GetMaxHealth();

            healthPercentage = Mathf.Clamp01(healthPercentage);

            if (healthPercentage <= 0)
            {
                foregroundSprite.localScale = new Vector3(0, foregroundSprite.localScale.y, foregroundSprite.localScale.z);
            }
            else
            {
                foregroundSprite.localScale = new Vector3(
                    healthPercentage * initialScaleX,
                    foregroundSprite.localScale.y,
                    foregroundSprite.localScale.z
                );
            }
        }
        else
        {
            if (foregroundSprite != null)
            {
                foregroundSprite.localScale = Vector3.zero;
            }
        }
    }
}
