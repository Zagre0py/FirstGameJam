using UnityEngine;
using System.Collections;

public class LightController : MonoBehaviour
{
    [Header("Luces que parpadean")]
    public Light[] flickeringLights; // SpotLights y PointLights que parpadean
    public float flickerSpeed = 0.1f; // Velocidad del parpadeo
    public float flickerDuration = 5f; // Tiempo que parpadean
    public float steadyDuration = 10f; // Tiempo que permanecen encendidas
    public float intensityVariation = 0.5f; // Cuánto aumenta o disminuye la intensidad

    private float[] initialIntensities; // Guarda la intensidad original de cada luz

    [Header("Luz Direccional")]
    public Light directionalLight;
    public float cycleDuration = 300f; // Duración del ciclo (en segundos)
    public float rotationAngle = 45f; // Ángulo total de rotación durante el ciclo

    private float rotationSpeed;
    private float cycleProgress = 0f;

    [Header("Transición de Skybox")]
    public Material nightSkybox;  // Skybox de noche
    public Material daySkybox;    // Skybox de día
    public Color nightAmbientLight = new Color(0.1f, 0.1f, 0.3f);
    public Color dayAmbientLight = new Color(1f, 0.95f, 0.8f);

    void Start()
    {
        if (directionalLight != null)
        {
            rotationSpeed = rotationAngle / cycleDuration;
        }

        // Guardar las intensidades originales de cada luz
        initialIntensities = new float[flickeringLights.Length];
        for (int i = 0; i < flickeringLights.Length; i++)
        {
            if (flickeringLights[i] != null)
            {
                initialIntensities[i] = flickeringLights[i].intensity;
            }
        }

        // Iniciar el ciclo de parpadeo
        StartCoroutine(FlickerLightsCycle());
    }

    void Update()
    {
        RotateDirectionalLight();
        UpdateSkyboxTransition();
    }

    void RotateDirectionalLight()
    {
        if (directionalLight != null)
        {
            directionalLight.transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
        }
    }

    void UpdateSkyboxTransition()
    {
        cycleProgress += Time.deltaTime / cycleDuration;
        cycleProgress %= 1f; // Mantiene el valor entre 0 y 1

        // Interpolación del Skybox
        RenderSettings.skybox.Lerp(nightSkybox, daySkybox, cycleProgress);

        // Ajuste de la luz ambiental para mejorar la transición
        RenderSettings.ambientLight = Color.Lerp(nightAmbientLight, dayAmbientLight, cycleProgress);
    }

    IEnumerator FlickerLightsCycle()
    {
        while (true)
        {
            float timer = 0f;
            while (timer < flickerDuration)
            {
                for (int i = 0; i < flickeringLights.Length; i++)
                {
                    if (flickeringLights[i] != null)
                    {
                        float variation = Random.Range(-intensityVariation, intensityVariation);
                        flickeringLights[i].intensity = Mathf.Clamp(
                            flickeringLights[i].intensity + variation,
                            0, // No permite valores negativos
                            initialIntensities[i] * 2 // Doble de la intensidad inicial como máximo
                        );
                    }
                }
                timer += flickerSpeed;
                yield return new WaitForSeconds(flickerSpeed);
            }

            // Restaurar las intensidades originales
            for (int i = 0; i < flickeringLights.Length; i++)
            {
                if (flickeringLights[i] != null)
                {
                    flickeringLights[i].intensity = initialIntensities[i];
                }
            }

            yield return new WaitForSeconds(steadyDuration);
        }
    }
}

