using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SciptGame : MonoBehaviour
{
   public void Reiniciar(){

    SceneManager.LoadScene("Andrea21");
   }

    public void Salir()
    {
        SceneManager.LoadScene("menuinicial");
    }
}
