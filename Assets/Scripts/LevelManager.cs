using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Image soundIcon;
    public Image lockImageLevel2;
    public Image lockImageLevel3;
    public Image lockImageLevel4;
    public Image lockImageLevel5;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;

    private bool isSoundOn = true;

    private AudioSource audioSource;

    public Text level1;
    public Text level2;
    public Text level3;
    public Text level4;
    public Text level5;

    public Button level1Button;
    public Button level2Button;
    public Button level3Button;
    public Button level4Button;
    public Button level5Button;
    // Start is called before the first frame update
    void Start()
    {
        isSoundOn = PlayerPrefs.GetInt("SoundOn", 1) == 1;
        UpdateSoundButtonImage();
        AudioListener.volume = isSoundOn ? 1 : 0;

        int highestLevel = PlayerPrefs.GetInt("HighestLevel", 3);

        level1Button.interactable = true;
        level2Button.interactable = highestLevel >= 4;
        level3Button.interactable = highestLevel >= 5;
        level4Button.interactable = highestLevel >= 6;
        level5Button.interactable = highestLevel >= 7;

        UpdateLockImages(highestLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public void Level1Button()
    {
        SceneManager.LoadScene(3);
    }

    public void Level2Button()
    {
        SceneManager.LoadScene(4);
    }

    public void Level3Button()
    {
        SceneManager.LoadScene(5);
    }

    public void Level4Button()
    {
        SceneManager.LoadScene(6);
    }

    public void Level5Button()
    {
        SceneManager.LoadScene(7);
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

    private void UpdateLockImages(int highestLevel)
    {
        UpdateLevelButton(lockImageLevel2, level2, highestLevel, 4);
        UpdateLevelButton(lockImageLevel3, level3, highestLevel, 5);
        UpdateLevelButton(lockImageLevel4, level4, highestLevel, 6);
        UpdateLevelButton(lockImageLevel5, level5, highestLevel, 7);
    }

    private void UpdateLevelButton(Image locked, Text levelText, int highestLevel, int level)
    {
        if (highestLevel < level)
        {
            locked.gameObject.SetActive(true);
            levelText.gameObject.SetActive(false);
        }
        else
        {
            locked.gameObject.SetActive(false);
            levelText.gameObject.SetActive(true);
        }
    }

    public void ResetProgress()
    {
        PlayerPrefs.SetInt("HighestLevel", 3);
        PlayerPrefs.Save();

        level2Button.interactable = false;
        level3Button.interactable = false;
        level4Button.interactable = false;
        level5Button.interactable = false;

        UpdateLockImages(1);

        Debug.Log("Progreso restablecido al nivel 1.");
    }
}
