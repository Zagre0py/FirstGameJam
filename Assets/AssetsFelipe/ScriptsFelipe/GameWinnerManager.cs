using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameWinnerManager : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText; // Referencia al texto donde se mostrará el puntaje

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0); // Obtiene el puntaje guardado, 0 es el valor predeterminado
        finalScoreText.text = "Puntaje final: " + finalScore.ToString();
    }
}

