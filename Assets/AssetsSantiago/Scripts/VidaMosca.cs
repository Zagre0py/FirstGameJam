using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VidaMosca : MonoBehaviour
{
    public int vidaTotal;
    public int vidaCurrente;
    public Image barraVidaEnemigo;

    void Start()
    {
        vidaCurrente = vidaTotal;
        // Asegúrate de que el cálculo de fillAmount sea correcto usando float
        barraVidaEnemigo.fillAmount = (float)vidaCurrente / vidaTotal;
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
        vidaCurrente -= 10;

        if (vidaCurrente <= 0)
        {
            Debug.Log("TaMuerto");
            Muerte();
        }
    }

    void Muerte()
    {
        Destroy(gameObject);
    }
}
