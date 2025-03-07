using System;
using UnityEngine;
using UnityEngine.UI;

public class VidaMosca : MonoBehaviour
{
    public int vidaTotal;
    public int vidaCurrente;
    public Image barraVidaEnemigo;
    public GameObject particulaDaño;

    // Evento para notificar cuando la mosca muere
    public event Action OnFlyDefeated;

    void Start()
    {
        particulaDaño = Resources.Load<GameObject>("particulaDaño");
        vidaCurrente = vidaTotal;
        barraVidaEnemigo.fillAmount = (float)vidaCurrente / vidaTotal;
        AudioManager.instance.PlaySfx("Mosca");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Racket"))
        {
            TakeDamage();
        }
        barraVidaEnemigo.fillAmount = (float)vidaCurrente / vidaTotal;
    }

    public void TakeDamage()
    {
        Instantiate(particulaDaño, transform.position, Quaternion.identity);
        vidaCurrente -= 10;

        if (vidaCurrente <= 0)
        {
            AudioManager.instance.PlaySfx("Slap");
            Debug.Log("TaMuerto");
            Muerte();
        }
    }

    void Muerte()
    {
        GameManager.Instance.AddScore();
        GameManager.Instance.FlyDefeated(); // Notificar al GameManager que una mosca ha sido derrotada
        Destroy(gameObject);
    }
}