using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { get; private set; }

    public GameObject flyPrefab; // Prefab de la mosca
    public Transform[] spawnPoints; // Puntos de spawn
    public float timeBetweenSpawns = 1f;

    private void Awake()
    {
        flyPrefab = Resources.Load<GameObject>("flyPrefab");

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
    }


    // M�todo para buscar los spawnPoints en la escena actual
    public void FindSpawnPoints()
    {
        GameObject[] spawnPointObjects = GameObject.FindGameObjectsWithTag("SpawnPoint");
        spawnPoints = new Transform[spawnPointObjects.Length];
        for (int i = 0; i < spawnPointObjects.Length; i++)
        {
            spawnPoints[i] = spawnPointObjects[i].transform;
        }
        Debug.Log("Se encontraron " + spawnPoints.Length + " spawnPoints en la escena actual.");
    }

    public void StartWave(int numberOfFlies)
    {
        StartCoroutine(SpawnFlies(numberOfFlies));
    }

    private IEnumerator SpawnFlies(int numberOfFlies)
    {
        for (int i = 0; i < numberOfFlies; i++)
        {
            if (spawnPoints.Length == 0)
            {
                Debug.LogError("No hay spawnPoints asignados.");
                yield break;
            }

            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Debug.Log("Instanciando mosca en: " + spawnPoint.position);
            GameObject fly = Instantiate(flyPrefab, spawnPoint.position, Quaternion.identity);

            // Suscribir el evento OnFlyDefeated de la mosca
            VidaMosca vidaMosca = fly.GetComponent<VidaMosca>();
            if (vidaMosca != null)
            {
                vidaMosca.OnFlyDefeated += GameManager.Instance.FlyDefeated;
            }
            else
            {
                Debug.LogError("El prefab de la mosca no tiene el componente VidaMosca.");
            }

            yield return new WaitForSeconds(timeBetweenSpawns);
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
        if (scene.name == "menuinicial" || scene.name == "GameOver" || scene.name == "GameVictory")
        {
            Destroy(gameObject); // Destruir WaveManager si est� presente en estas escenas
        }
    }
}