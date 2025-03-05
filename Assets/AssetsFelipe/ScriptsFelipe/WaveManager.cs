using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { get; private set; }

    public GameObject flyPrefab; // Prefab de la mosca
    public Transform[] spawnPoints; // Puntos de spawn
    public float timeBetweenSpawns = 1f;

    private void Awake()
    {
        if (flyPrefab == null)
        {
            flyPrefab = Resources.Load<GameObject>("flyPrefab");
            if (flyPrefab == null)
            {
                Debug.LogError("No se pudo cargar el prefab 'flyPrefab' desde la carpeta Resources.");
            }
        }

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartWave(int numberOfFlies)
    {
        StartCoroutine(SpawnFlies(numberOfFlies));
    }

    private IEnumerator SpawnFlies(int numberOfFlies)
    {
        for (int i = 0; i < numberOfFlies; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
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
}