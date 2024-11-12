using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Image soundIcon;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;

    private bool isSoundOn = true;

    public GameObject MainMenu;
    public GameObject ExitMenu;

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

    public void StartButton()
    {
        SceneManager.LoadScene(3);
    }

    public void LevelsButton()
    {
        SceneManager.LoadScene(1);
    }

    public void CreditsButton()
    {
        SceneManager.LoadScene(2);
    }

    public void ExitButton()
    {
        ExitMenu.SetActive(true);
        MainMenu.SetActive(false);
    }

    public void AcceptButton()
    {
        Debug.Log("Salir del juego");
        Application.Quit();
    }

    public void CancelButton()
    {
        ExitMenu.SetActive(false);
        MainMenu.SetActive(true);
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
}
