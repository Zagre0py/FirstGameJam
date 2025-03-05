using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIOptions : MonoBehaviour
{
   
    [Header("UI References")]
    public Button resumeButton;
    public Button restartButton;
    public Button mainMenuButton;

    public GameObject panelOptions;

    private void Start()
    {
        // Configuramos los listeners de los botones
        resumeButton.onClick.AddListener(ResumeGame);
        restartButton.onClick.AddListener(RestartGame);
        mainMenuButton.onClick.AddListener(GoToMainMenu);
    }

    public void ResumeGame()
    {
        panelOptions.SetActive(false);
        Time.timeScale = 1f; // Reanudar el tiempo
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Asegurarse de que el tiempo vuelve a ser normal
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Asegurarse de que el tiempo vuelve a ser normal
        SceneManager.LoadScene("menuinicial");
    }
   
}