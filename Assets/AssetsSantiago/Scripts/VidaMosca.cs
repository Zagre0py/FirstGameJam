using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VidaMosca : MonoBehaviour
{
  public int vidaTotal;
  public Slider BarraVidaEnemigo;


    void Update()
    {
        BarraVidaEnemigo.value = vidaTotal;
    }

    void OnCollisionEnter(Collision collision)
    {
       if(collision.gameObject.CompareTag("Player")){
        vidaTotal -= 10;
       }
    }

    public void TomarDaño(int daño){

        vidaTotal -= daño;
        if(vidaTotal <= 0){

           Muerte();
        }
    }
    void Muerte(){

        Destroy(this.gameObject);
    }
}
