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
            RecibirDaño(10);
        }
    }

    void RecibirDaño(float daño)
    {
        vidaActual -= daño;
        barraDeVida.CambiarVidaActual(vidaActual);
    }
}

