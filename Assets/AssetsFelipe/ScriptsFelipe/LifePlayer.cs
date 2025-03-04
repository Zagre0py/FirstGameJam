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
        barLife.fillAmount = lifeCurrent / lifeMax;
        lifeMax = 10;

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();
            Debug.Log("El player  esta recibiendo daño" + lifeCurrent);
        }
        barLife.fillAmount = lifeCurrent / lifeMax;
    }
    public void TakeDamage()
    {
        lifeCurrent -= 1;
    }
}
