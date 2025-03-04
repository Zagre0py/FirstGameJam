using UnityEngine;
using UnityEngine.UI;

public class ControladorAudio : MonoBehaviour
{
    public Slider sliderFX;
    public Slider sliderMusica;
    public AudioSource audioFX;
    public AudioSource audioMusica;

    void Start()
    {
        // Cargar valores guardados
        sliderFX.value = PlayerPrefs.GetFloat("VolumenFX", 1);
        sliderMusica.value = PlayerPrefs.GetFloat("VolumenMusica", 1);

        // Aplicar volumen inicial
        audioFX.volume = sliderFX.value;
        audioMusica.volume = sliderMusica.value;

        // Agregar eventos a los sliders
        sliderFX.onValueChanged.AddListener(CambiarVolumenFX);
        sliderMusica.onValueChanged.AddListener(CambiarVolumenMusica);
    }

    public void CambiarVolumenFX(float volumen)
    {
        audioFX.volume = volumen;
        PlayerPrefs.SetFloat("VolumenFX", volumen); // Guardar preferencia
    }

    public void CambiarVolumenMusica(float volumen)
    {
        audioMusica.volume = volumen;
        PlayerPrefs.SetFloat("VolumenMusica", volumen); // Guardar preferencia
    }
}

