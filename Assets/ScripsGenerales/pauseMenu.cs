using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;

    

    void Update()
    {
        // Verifica si se presionó la tecla Esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
          Cursor.lockState = CursorLockMode.Locked;

        TestButton();
        pauseMenuUI.SetActive(false); // Oculta el menú de pausa
        Time.timeScale = 1f; // Reanuda el tiempo del juego
        isPaused = false;
    }

    void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        pauseMenuUI.SetActive(true); // Muestra el menú de pausa
        Time.timeScale = 0f; // Pausa el tiempo del juego
        isPaused = true;
    }

    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        SceneManager.LoadScene("menuinicial");
    }
    public void ResetGame(){

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void TestButton()
{
    Debug.Log("Botón funcionando");
}
}


