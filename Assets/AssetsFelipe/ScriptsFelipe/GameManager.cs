using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] bool isPaused, didLose;
    public int scoreGame = 0;
    public TextMeshProUGUI scoreText, timeText, waveText;
    public TextMeshProUGUI fliesRemainingText;


    public int currentWave = 0;
    public int currentLevel = 1;

    [System.Serializable]
    public class Wave
    {
        public int fliesToSpawn;
        public float timeLimit;
    }

    [System.Serializable]
    public class LevelWaves
    {
        public int level;
        public Wave[] waves;
    }

    [SerializeField] private List<LevelWaves> levelWavesList = new List<LevelWaves>();

    private float currentTimeLimit;
    private int fliesRemainingInWave;
    private Wave[] waves;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            // No persistir en las escenas no deseadas
            if (SceneManager.GetActiveScene().name != "menuinicial" &&
                SceneManager.GetActiveScene().name != "GameOver" &&
                SceneManager.GetActiveScene().name != "GameVictory")
            {
                DontDestroyOnLoad(gameObject); // Persistir en las escenas que no sean MainMenu, GameOver, o GameVictory
            }
        }
        else
        {
            Destroy(gameObject); // Elimina esta instancia si ya existe
        }

        didLose = false;
    }

    private void Start()
    {
        ConfigureLevelWaves();  // Configurar las oleadas de cada nivel
        StartGame();  // Iniciar el juego
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void ConfigureLevelWaves()
    {
        LevelWaves level1Waves = new LevelWaves
        {
            level = 1,
            waves = new Wave[]
            {
                new Wave { fliesToSpawn = 5, timeLimit = 15 },
                new Wave { fliesToSpawn = 10, timeLimit = 25 },
                new Wave { fliesToSpawn = 20, timeLimit = 40 }
            }
        };
        levelWavesList.Add(level1Waves);

        LevelWaves level2Waves = new LevelWaves
        {
            level = 2,
            waves = new Wave[]
            {
                new Wave { fliesToSpawn = 8, timeLimit = 20 },
                new Wave { fliesToSpawn = 15, timeLimit = 30 },
                new Wave { fliesToSpawn = 25, timeLimit = 45 }
            }
        };
        levelWavesList.Add(level2Waves);
    }

    public Wave[] GetCurrentWaves()
    {
        var levelData = levelWavesList.Find(lw => lw.level == currentLevel);
        if (levelData != null)
        {
            return levelData.waves;
        }
        else
        {
            Debug.LogError("No se encontraron oleadas para el nivel " + currentLevel);
            return null;
        }
    }

    private void StartGame()
    {
        waves = GetCurrentWaves();
        FindTextReferences();
        StartWave();  // Iniciar la primera oleada
    }

    private void FindTextReferences()
    {
        scoreText = GameObject.Find("ScoreText")?.GetComponent<TextMeshProUGUI>();
        timeText = GameObject.Find("TimeText")?.GetComponent<TextMeshProUGUI>();
        waveText = GameObject.Find("WaveText")?.GetComponent<TextMeshProUGUI>();
        fliesRemainingText = GameObject.Find("FliesRemainingText")?.GetComponent<TextMeshProUGUI>();

        if (scoreText == null || timeText == null || waveText == null || fliesRemainingText == null)
        {
            Debug.LogError("No se encontraron los TextMeshProUGUI en la escena actual.");
        }
        else
        {
            Debug.Log("TextMeshProUGUI reasignados correctamente.");
        }
    }

    public void AddScore()
    {
        scoreGame += Random.Range(5, 15);
        UpdateScoreText(); // Actualiza el texto del score
        Debug.Log("Puntuación: " + scoreGame);
    }

 public void GameOverLose()
{
    if (!didLose)
    {
        didLose = true;
        Debug.Log("Fin del juego! Puntuación final: " + scoreGame);
        PlayerPrefs.SetInt("FinalScore", scoreGame);  // Guardamos el puntaje final
        PlayerPrefs.Save(); // Guardamos los cambios
        SceneManager.LoadScene("GameOver");
    }
}


    public void GameOverWin()
    {
        Debug.Log("¡Has ganado el nivel!");

        currentLevel++; // Pasar al siguiente nivel

        if (currentLevel > 2) // Suponiendo que solo haya 2 niveles por ahora
        {
            Debug.Log("¡Has completado todos los niveles!");
            PlayerPrefs.SetInt("FinalScore", scoreGame);  // Guardamos el puntaje final
            PlayerPrefs.Save(); // Guardamos los cambios
            SceneManager.LoadScene("GameVictory"); // Cambié de "GameOver" a "GameVictory"
        }
        else
        {
            Debug.Log("Cargando nivel " + currentLevel);
            currentWave = 0; // Reiniciar las oleadas para el siguiente nivel
            SceneManager.LoadScene("Andrea2" + currentLevel); // Cargar el siguiente nivel
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseGame()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
    }

   private void StartWave()
{
    // Verifica si ya no hay más oleadas para este nivel
    if (currentWave < waves.Length)
    {
        currentTimeLimit = waves[currentWave].timeLimit;
        fliesRemainingInWave = waves[currentWave].fliesToSpawn;
        Debug.Log("Iniciando oleada " + (currentWave + 1) + " del nivel " + currentLevel);
        Debug.Log("Moscas a spawnear: " + fliesRemainingInWave);
        UpdateWaveText();
        UpdateFliesRemainingText(); // Actualizamos el texto de las moscas restantes
        WaveManager.Instance.StartWave(fliesRemainingInWave);
    }
    else
    {
        Debug.Log("¡Has completado todas las oleadas del nivel " + currentLevel + "!");
        GameOverWin(); // Aquí se llama a GameOverWin para pasar al siguiente nivel
    }
}
    void Update()
    {
        if (currentTimeLimit > 0 && fliesRemainingInWave > 0)
        {
            // Reduce el tiempo usando Time.deltaTime
            currentTimeLimit -= Time.deltaTime;

            // Si el tiempo restante es menor a cero, aseguramos que no se muestre en negativo
            if (currentTimeLimit < 0)
            {
                currentTimeLimit = 0;
            }

            // Actualiza el texto del tiempo restante en la UI
            if (timeText != null)
            {
                timeText.text = "TIEMPO RESTANTE/ " + Mathf.CeilToInt(currentTimeLimit); // Redondeamos hacia arriba para mostrar segundos enteros
            }

            // Si no quedan moscas y el tiempo aún no ha acabado, completamos la oleada
            if (fliesRemainingInWave <= 0)
            {
                CompleteWave();
            }
        }

        // Si el tiempo se ha agotado y todavía hay moscas, el jugador pierde
        if (currentTimeLimit <= 0 && fliesRemainingInWave > 0)
        {
            GameOverLose(); // El jugador pierde si se acaba el tiempo
        }
    }

    public void CompleteWave()
    {
        currentWave++;
        if (currentWave < waves.Length)
        {
            StartWave();
        }
        else
        {
            GameOverWin();
        }
    }

    public void FlyDefeated()
    {
        fliesRemainingInWave--; // Reducimos el número de moscas restantes
        UpdateFliesRemainingText();
        // Si ya no quedan moscas, completamos la oleada
        if (fliesRemainingInWave <= 0)
        {
            CompleteWave(); // Finaliza la oleada actual
        }
    }

    private void UpdateWaveText()
    {
        if (waveText != null)
        {
            waveText.text = "OLEADA/ " + (currentWave + 1);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.StartsWith("Andrea2")) // Asegúrate de que el nombre de la escena sea correcto
        {
            FindTextReferences();
            UpdateScoreText(); // Actualiza el texto del score con el valor actual
            waves = GetCurrentWaves();
            currentWave = 0;
            WaveManager.Instance.FindSpawnPoints();
            StartWave();
        }
        if (scene.name == "MainMenu" || scene.name == "GameOver" || scene.name == "GameVictory")
        {
            Destroy(gameObject); // Destruir GameManager si está presente en estas escenas
        }
    }
    private void UpdateFliesRemainingText()
    {
        if (fliesRemainingText != null)
        {
            fliesRemainingText.text ="RESTANTES/ " + fliesRemainingInWave + "/" + waves[currentWave].fliesToSpawn;
        }
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "SCORE/ " + scoreGame.ToString();
        }
    }
}
