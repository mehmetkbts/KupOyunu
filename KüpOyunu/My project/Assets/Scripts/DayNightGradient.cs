using UnityEngine;

public class DayNightGradient : MonoBehaviour
{
    [Header("Skybox Ayarları")]
    [SerializeField] private Gradient skyGradient;
    [SerializeField] private float cycleSpeed = 0.1f; // Geçiş hızı

    private float timeValue = 0f;

    void Start()
    {
        // Başlangıçta Skybox Material atanmış mı kontrol et
        if (RenderSettings.skybox == null)
        {
            Debug.LogWarning("Skybox material atanmadı! Lighting → Environment → Skybox Material alanını kontrol et.");
        }
    }

    void Update()
    {
        // Zamanı döngüye sok (0–1 arasında)
        timeValue += Time.deltaTime * cycleSpeed;
        if (timeValue > 1f) timeValue = 0f;

        // Gradient'ten rengi al
        Color currentColor = skyGradient.Evaluate(timeValue);

        // Skybox rengi değiştir
        RenderSettings.skybox.SetColor("_SkyTint", currentColor);
    }
}
