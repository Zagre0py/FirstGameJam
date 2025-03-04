using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] bool isPaused, didLose;
    public float timeToWin = 15f;
    public int scoreGame;

    // Variables para el sistema de oleadas
    public int currentWave = 0;
    public int totalWavesPerLevel = 3;
    public int currentLevel = 1;

    // Variables para ajustar la dificultad
    public int baseFliesPerWave = 10; // Moscas base por oleada
    public float difficultyMultiplier = 1.2f; // Multiplicador de dificultad por nivel

    private void Awake()
    {
        // Implementación del patrón Singleton
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
        //didWinner = false;
        scoreGame = 0;
    }

    private void Start()
    {
        Time.timeScale = 1f;
        StartWave(); // Iniciar la primera oleada
    }

    public void AddScore(int points)
    {
        if (!didLose)
        {
            scoreGame += points;
            Debug.Log("Puntuación: " + scoreGame);
        }
    }

    // Se ejecuta cuando perdemos y hace todo lo referente a ello (Reiniciar la escena, devolver valores a defecto)
    public void GameOverLose()
    {
        didLose = true;
        Debug.Log("Fin del juego! Puntuación final: " + scoreGame);
        SceneManager.LoadScene("NombreScenaDeGameOver");
    }

    // Se ejecuta cuando ganamos y hace todo lo referente a ello (cargar nuevo nivel, darnos feedback, etc)
    public void GameOverWin()
    {
        //didWinner = true;
        Debug.Log("¡Has ganado el nivel!");
        currentLevel++; // Pasar al siguiente nivel

        if (currentLevel > 3) // Supongamos que hay 3 niveles en total
        {
            Debug.Log("¡Has completado todos los niveles!");
            SceneManager.LoadScene("NombreScenaDeVictoria");
        }
        else
        {
            Debug.Log("Cargando nivel " + currentLevel);
            SceneManager.LoadScene("Nivel" + currentLevel); // Cargar el siguiente nivel
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Método para pausar el juego
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

    // Método para iniciar una oleada
    public void StartWave()
    {
        if (currentWave < totalWavesPerLevel)
        {
            currentWave++;
            Debug.Log("Iniciando oleada " + currentWave + " del nivel " + currentLevel);

            // Calcular la dificultad ajustada al nivel
            int fliesThisWave = Mathf.RoundToInt(baseFliesPerWave * Mathf.Pow(difficultyMultiplier, currentLevel - 1));
            WaveManager.Instance.StartWave(fliesThisWave); // Iniciar la oleada con el número de moscas ajustado
        }
        else
        {
            Debug.Log("¡Has completado todas las oleadas del nivel " + currentLevel + "!");
            GameOverWin(); // Pasar al siguiente nivel o ganar el juego
        }
    }

    // Método para notificar que una oleada ha sido completada
    public void CompleteWave()
    {
        Debug.Log("Oleada " + currentWave + " completada!");
        StartWave(); // Iniciar la siguiente oleada
    }
}