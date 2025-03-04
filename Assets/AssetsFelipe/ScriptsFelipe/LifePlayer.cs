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
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            TakeDamage();
            Debug.Log("El player  esta recibiendo daño");
        }
    }
    public void TakeDamage()
    {
        lifeCurrent -= 1;
    }
}
