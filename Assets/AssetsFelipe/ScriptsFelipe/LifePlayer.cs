using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePlayer : MonoBehaviour
{
   public float lifeMax;
   public float lifeCurrent;

    private void Start()
    {
        lifeMax = lifeCurrent;
        lifeMax = 10;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();
            Debug.Log("El player  esta recibiendo daño" + lifeCurrent);
        }
    }
    public void TakeDamage()
    {
        lifeCurrent -= 1;
    }
}
