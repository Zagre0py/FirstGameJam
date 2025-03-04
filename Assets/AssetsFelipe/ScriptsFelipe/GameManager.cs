using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; } 
    [SerializeField] bool isPaused, didLose, didWinner;
    public float timeToWin = 15f;
    public int scoreGame;

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
        scoreGame = 0;
    }
    private void Start()
    {
        Time.timeScale = 1f;
    }
    public void AddScore(int points)
    {
        if(!didLose)
        {
            scoreGame += points;
            Debug.Log("Puntuacion: " + scoreGame);
        }
    }
    //Se ejecuta cuando perdemos y hace todo lo referente a ello (Reiniciar la escena, devovler valores a defecto);   
    public void GameOverLose()
    {
        didLose = true;
        Debug.Log("Fin del juego! Puntuacion final: " + scoreGame);
        SceneManager.LoadScene("NombreScena de GameOver ");

    }
    //Se ejecuta cuando ganamos y hace todo lo referente a ello (cargar neuvo nivel, darnos feedback, etc);   
    public void GameOverWin()
    {
        if (didWinner == true)
        {   
            //SceneManager.LoadScene("Nombre de Escena Ganador o nivel 2");
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    //Método para pausar el juego
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
}