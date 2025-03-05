using TMPro;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText; // Referencia al texto donde se mostrar� el puntaje

    void Start()
    {
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0); // Obtiene el puntaje guardado, 0 es el valor predeterminado
        finalScoreText.text = "PUNTOS/ " + finalScore.ToString();
    }
}
