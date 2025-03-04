using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
public class barradevida : MonoBehaviour
{
    // Start is called before the first frame update

    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void CambiarVidaMaxima(float vidaMaxima)
    {
        slider.maxValue = vidaMaxima;
    }
    public void CambiarVidaActual(float cantidadVida)
    {
        slider.value = cantidadVida;
    }

    public void InicializarBarradeVida(float cantidadVida)
    {
        CambiarVidaMaxima(cantidadVida);
        CambiarVidaActual(cantidadVida);
    }

    // Update is called once per frame  
    void Update()
    {

    }
}

