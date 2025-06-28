using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light sun;
    public float rotationSpeed = 10f;

    private Gradient lightColor;
    private Gradient skyColor;
    [SerializeField]
    private Gradient skyGradient;


    void Start()
    {
        SetupGradients();
    }

    void Update()
    {
        // Güneşi döndür
        sun.transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);

        // Güneşin yukarıda mı aşağıda mı olduğunu hesapla
        float sunDot = Vector3.Dot(sun.transform.forward, Vector3.down);
        float t = Mathf.Clamp01((sunDot + 1f) / 2f);

        // Işık ve gökyüzü rengi güncelle
        sun.color = lightColor.Evaluate(t);
        RenderSettings.skybox.SetColor("_SkyTint", skyColor.Evaluate(t));
    }

    void SetupGradients()
    {
        // ☀️ IŞIK RENKLERİ
         GradientColorKey[] lightColorKeys = new GradientColorKey[5];
        lightColorKeys[0].color = new Color(0.05f, 0.1f, 0.2f); lightColorKeys[0].time = 0f;    // gece
        lightColorKeys[1].color = new Color(1f, 0.5f, 0f); lightColorKeys[1].time = 0.25f; // gün doğumu
        lightColorKeys[2].color = Color.yellow; lightColorKeys[2].time = 0.5f;  // öğle
        lightColorKeys[3].color = new Color(1f, 0.5f, 0f); lightColorKeys[3].time = 0.75f; // gün batımı
        lightColorKeys[4].color = new Color(0.05f, 0.1f, 0.2f); lightColorKeys[4].time = 1f;   // gece

        GradientAlphaKey[] lightAlphaKeys = {
            new GradientAlphaKey(1f, 0f),
            new GradientAlphaKey(1f, 1f)
        };

        lightColor = new Gradient();
        lightColor.SetKeys(lightColorKeys, lightAlphaKeys);

        // 🌌 GÖKYÜZÜ RENKLERİ (Skybox)
        GradientColorKey[] skyColorKeys = new GradientColorKey[5];
        skyColorKeys[0].color = new Color(0.02f, 0.02f, 0.05f); skyColorKeys[0].time = 0f;    // gece
        skyColorKeys[1].color = new Color(0.4f, 0.2f, 0.6f); skyColorKeys[1].time = 0.25f; // gün doğumu
        skyColorKeys[2].color = new Color(0.5f, 0.7f, 1f); skyColorKeys[2].time = 0.5f;  // gündüz
        skyColorKeys[3].color = new Color(0.4f, 0.2f, 0.6f); skyColorKeys[3].time = 0.75f; // gün batımı
        skyColorKeys[4].color = new Color(0.02f, 0.02f, 0.05f); skyColorKeys[4].time = 1f;    // gece

        GradientAlphaKey[] skyAlphaKeys = {
            new GradientAlphaKey(1f, 0f),
            new GradientAlphaKey(1f, 1f)
        };

        skyColor = new Gradient();
        skyColor.SetKeys(skyColorKeys, skyAlphaKeys); 
    }
}


