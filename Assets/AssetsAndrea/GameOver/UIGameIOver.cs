using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIGameOver : MonoBehaviour
{
    [Header("UI References")]
    public Button restartButton;
    public Button mainMenuButton;

    private void Start()
    {
        // Verificar si los botones están asignados antes de agregar los listeners
        if (restartButton != null)
            restartButton.onClick.AddListener(RestartGame);

        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(GoToMainMenu);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Restablecer el tiempo por si estaba en pausa
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Cargar la escena actual
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Asegurarse de que el tiempo vuelve a la normalidad
        SceneManager.LoadScene("menuinicial"); // Asegúrate de que el nombre coincide con la escena del menú
    }
}
