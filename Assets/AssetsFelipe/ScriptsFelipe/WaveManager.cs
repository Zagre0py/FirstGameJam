using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { get; private set; }

    public GameObject flyPrefab; // Prefab de la mosca
    public Transform[] spawnPoints; // Puntos de spawn
    public float timeBetweenSpawns = 1f;

    private int fliesRemaining;

    private void Awake()
    {
        // Cargar el prefab dinámicamente desde la carpeta Resources
        if (flyPrefab == null)
        {
            flyPrefab = Resources.Load<GameObject>("flyPrefab"); // Asegúrate de que el nombre coincida con el archivo del prefab
            if (flyPrefab == null)
            {
                Debug.LogError("No se pudo cargar el prefab 'flyPrefab' desde la carpeta Resources.");
            }
        }

        flyPrefab.gameObject.SetActive(true);

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Método para iniciar una oleada con un número específico de moscas
    public void StartWave(int numberOfFlies)
    {
        fliesRemaining = numberOfFlies;
        StartCoroutine(SpawnFlies());
    }

    private IEnumerator SpawnFlies()
    {
        for (int i = 0; i < fliesRemaining; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(flyPrefab, spawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    public void FlyDefeated()
    {
        fliesRemaining--;

        if (fliesRemaining <= 0)
        {
            GameManager.Instance.CompleteWave(); // Notificar al GameManager que la oleada ha sido completada
        }
    }
}