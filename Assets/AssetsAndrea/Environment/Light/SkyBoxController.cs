using UnityEngine;

public class SkyBoxController : MonoBehaviour
{
    public float rotationSpeed = 1.0f;
    public Color nightAmbientLight = new Color(0.1f, 0.1f, 0.3f);
    public Color dayAmbientLight = new Color(1f, 0.95f, 0.8f);
    public Color nightFog = new Color(0.05f, 0.05f, 0.1f, 0.8f);
    public Color dayFog = new Color(0.8f, 0.9f, 1f, 0.2f);
    public float cycleDuration = 60f;

    private float cycleProgress = 0f;

    void Update()
    {
        // Rotación del Skybox
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);

        // Interpolación basada en el ciclo
        cycleProgress += Time.deltaTime / cycleDuration;
        cycleProgress %= 1f; // Mantiene el valor entre 0 y 1

        float interpolationFactor = Mathf.Sin(cycleProgress * Mathf.PI);

        // Ajustar luz ambiental y niebla en vez del tinte del Skybox
        RenderSettings.ambientLight = Color.Lerp(nightAmbientLight, dayAmbientLight, interpolationFactor);
        RenderSettings.fogColor = Color.Lerp(nightFog, dayFog, interpolationFactor);
    }
}
