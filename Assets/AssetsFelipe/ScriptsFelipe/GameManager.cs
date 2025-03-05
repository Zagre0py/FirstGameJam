using TMPro; 
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] bool isPaused, didLose;
    public int scoreGame = 0;
    public TextMeshProUGUI scoreText, timeText, waveText; 

    // Variables para el sistema de oleadas
    public int currentWave = 0;
    public int totalWavesPerLevel = 3;
    public int currentLevel = 1;

    // Definición de las oleadas
    [System.Serializable]
    public class Wave
    {
        public int fliesToSpawn;
        public float timeLimit;
    }

    public Wave[] waves;

    private float currentTimeLimit;
    private int fliesRemainingInWave;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        didLose = false;
        scoreGame = 0;
    }

    private void Start()
    {
        timeText.text = "Tiempo restante: " + currentTimeLimit;
        scoreText.text = "Score: " + scoreGame.ToString();
        UpdateWaveText(); // Actualizar el texto de la oleada al inicio
        Time.timeScale = 1f;
        StartWave(); // Iniciar la primera oleada
    }

    public void AddScore()
    {
        scoreGame += Random.Range(5, 15);
        scoreText.text = "Score: " + scoreGame.ToString();
        Debug.Log("Puntuación: " + scoreGame);
    }

    public void GameOverLose()
    {
        didLose = true;
        Debug.Log("Fin del juego! Puntuación final: " + scoreGame);
        //SceneManager.LoadScene("NombreScenaDeGameOver");
    }

    public void GameOverWin()
    {
        Debug.Log("¡Has ganado el nivel!");
        currentLevel++; // Pasar al siguiente nivel

        if (currentLevel > 2) 
        {
            Debug.Log("¡Has completado todos los niveles!");
            SceneManager.LoadScene("NombreScenaDeVictoria");
        }
        else
        {
            Debug.Log("Cargando nivel " + currentLevel);
            //SceneManager.LoadScene("Nivel" + currentLevel); // Cargar el siguiente nivel
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseGame()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void StartWave()
    {
        if (currentWave < waves.Length)
        {
            currentTimeLimit = waves[currentWave].timeLimit;
            fliesRemainingInWave = waves[currentWave].fliesToSpawn;
            Debug.Log("Iniciando oleada " + (currentWave + 1) + " del nivel " + currentLevel);

            UpdateWaveText(); // Actualizar el texto de la oleada
            WaveManager.Instance.StartWave(fliesRemainingInWave); // Iniciar la oleada con el número de moscas ajustado
            StartCoroutine(WaveTimer());
        }
        else
        {
            Debug.Log("¡Has completado todas las oleadas del nivel " + currentLevel + "!");
            GameOverWin(); // Pasar al siguiente nivel o ganar el juego
        }
    }

    private IEnumerator WaveTimer()
    {
        while (currentTimeLimit > 0)
        {
            yield return new WaitForSeconds(1f);
            currentTimeLimit--;
            timeText.text = "Tiempo restante: " + currentTimeLimit;

            if (fliesRemainingInWave <= 0)
            {
                CompleteWave();
                yield break;
            }
        }
        // Si el tiempo se acaba y aún hay moscas vivas, el jugador pierd
        if (fliesRemainingInWave > 0)
        {
            GameOverLose();
        }
    }

    public void CompleteWave()
    {
        Debug.Log("Oleada " + (currentWave + 1) + " completada!");
        currentWave++;
        StartWave(); // Iniciar la siguiente oleada
    }

    public void FlyDefeated()
    {
        fliesRemainingInWave--;

        if (fliesRemainingInWave <= 0)
        {
            CompleteWave();
        }
    }

    // Método para actualizar el texto de la oleada
    private void UpdateWaveText()
    {
        if (waveText != null)
        {
            waveText.text = "Oleada: " + (currentWave + 1); // Mostrar la oleada actual
        }
    }
}