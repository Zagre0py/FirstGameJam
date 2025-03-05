using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LightController : MonoBehaviour
{
    public static LightController instance { get; private set; }
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
    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            // Solo mantener este objeto persistente si no estamos en MainMenu, GameOver o GameVictory
            if (SceneManager.GetActiveScene().name != "MainMenu" &&
                SceneManager.GetActiveScene().name != "GameOver" &&
                SceneManager.GetActiveScene().name != "GameVictory")
            {
                DontDestroyOnLoad(gameObject); // Persistir en las demás escenas
            }
        }
        else
        {
            Destroy(gameObject); // Eliminar la instancia duplicada si ya existe
        }
    }

    void Start()
    {
        // Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // Destruir si ya existe otra instancia
        }

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
    }

    void RotateDirectionalLight()
    {
        if (directionalLight != null)
        {
            directionalLight.transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
        }
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
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Si la escena es MainMenu, GameOver, o GameVictory, destruir el WaveManager
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu" || scene.name == "GameOver" || scene.name == "GameVictory")
        {
            Destroy(gameObject); // Destruir WaveManager si está presente en estas escenas
        }
    }
}


