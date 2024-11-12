using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{

    public GameObject PauseMenu;
    public GameObject ExitMenu;
    public GameObject PauseButton;
    public GameObject RetryMenu;
    public GameObject NextLevelMenu;
    public GameObject Finished;
    public bool onPause;

    public Image soundIcon;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;

    private bool isSoundOn = true;

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        isSoundOn = PlayerPrefs.GetInt("SoundOn", 1) == 1;
        UpdateSoundButtonImage();
        AudioListener.volume = isSoundOn ? 1 : 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pauseButton()
    {
        onPause = true;
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeButton()
    {
        onPause = false;
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ReplayButton()
    {
        onPause = false;
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitButton()
    {
        ExitMenu.SetActive(true);
        PauseMenu.SetActive(false);
        PauseButton.SetActive(false);
    }

    public void AcceptButton()
    {
        Debug.Log("Salir del juego");
        Application.Quit();
    }

    public void CancelButton()
    {
        ExitMenu.SetActive(false);
        PauseMenu.SetActive(true);
        PauseButton.SetActive(true);
    }

    public void MainMenuButton()
    {
        onPause = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        PauseMenu.SetActive(false);
    }

    public void ToggleSound()
    {
        isSoundOn = !isSoundOn;
        AudioListener.volume = isSoundOn ? 1 : 0;

        UpdateSoundButtonImage();

        PlayerPrefs.SetInt("SoundOn", isSoundOn ? 1 : 0);

        PlayerPrefs.Save();
    }

    private void UpdateSoundButtonImage()
    {
        if (isSoundOn)
        {
            soundIcon.sprite = soundOnSprite;
        }
        else 
        {
            soundIcon.sprite = soundOffSprite;
        }
    }

    public void ShowRetryMenu()
    {
        RetryMenu.SetActive(true);
        PauseButton.SetActive(false);
    }

    public void NextLevel()
    {
        NextLevelMenu.SetActive(true);
        PauseButton.SetActive(false);

        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        int highestLevel = PlayerPrefs.GetInt("HighestLevel", 3);

        if (currentLevel >= highestLevel)
        {
            PlayerPrefs.SetInt("HighestLevel", currentLevel + 1);
            PlayerPrefs.Save();

            Debug.Log("Escena completada: " + currentLevel);
            Debug.Log("Nuevo HighestScene: " + (currentLevel + 1));
        }
        else
        {
            Debug.Log("El jugador ya ha completado hasta la escena " + highestLevel);
        }
    }

    public void AcceptNextLevel()
    {
        int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextLevelIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextLevelIndex);
            Debug.Log("Cargando siguiente escena: " + nextLevelIndex);
        }
        else
        {
            Debug.Log("No hay más niveles, has completado el último nivel.");
            Finished.SetActive(true);
        }
    }
}
