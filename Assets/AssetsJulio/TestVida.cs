using UnityEngine;

public class TestVida : MonoBehaviour
{
    public barradevida barraDeVida;
    private float vidaMaxima = 100;
    private float vidaActual;

    void Start()
    {
        vidaActual = vidaMaxima;
        barraDeVida.InicializarBarradeVida(vidaMaxima);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Presiona espacio para reducir la vida
        {
            RecibirDa�o(10);
        }
    }

    void RecibirDa�o(float da�o)
    {
        vidaActual -= da�o;
        barraDeVida.CambiarVidaActual(vidaActual);
    }
}

