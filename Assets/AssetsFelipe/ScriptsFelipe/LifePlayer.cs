using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LifePlayer : MonoBehaviour
{
   public float lifeMax;
   public float lifeCurrent;
    public Image barLife;

    private void Start()
    {
        lifeMax = lifeCurrent;
        barLife.fillAmount = (float) lifeCurrent / lifeMax;
        lifeMax = 40;

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();
            //Debug.Log("El player  esta recibiendo daño" + lifeCurrent);
        }
        barLife.fillAmount = (float) lifeCurrent / lifeMax;
    }
    public void TakeDamage()
    {
        lifeCurrent -= 1;

        if (lifeCurrent <= 0)
        {
            lifeCurrent = 0; // Asegurarnos de que no sea negativo
            Debug.Log("El jugador ha muerto");
            GameManager.Instance.GameOverLose(); // Notificar al GameManager que el jugador ha perdido
        }

        barLife.fillAmount = lifeCurrent / lifeMax;
    }
}
