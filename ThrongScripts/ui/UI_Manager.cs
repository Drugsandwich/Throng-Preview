using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    private GameObject menuHolder;
    private GameObject settingsHolder;
    private float unpaused_TimeScale;
    private bool isPaused;
    public bool IsPaused { get { return isPaused; } set { isPaused = value; } }
    [SerializeField] private AudioMixer audioMixer;

    private float sfx_Vol;
    private float music_Vol;
    [SerializeField] private Slider sfx_Slider;
    [SerializeField] private Slider music_Slider;
    private Stronghold strongHold;
    private GameObject deathScreen;
    private GameObject howToPlayeScreen;

    private void Awake()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1) // if the active is the map scene then set up the stronghold componenet
        {
            strongHold = GameObject.FindGameObjectWithTag("StrongHold").GetComponent<Stronghold>();
        }

        howToPlayeScreen = transform.Find("AllMenuHolders/HowToPlayHolder").gameObject;
        deathScreen = transform.Find("AllMenuHolders/DeathScreenHolder").gameObject;
        menuHolder = transform.Find("AllMenuHolders/MenuHolder").gameObject;
        settingsHolder = transform.Find("AllMenuHolders/SettingsHolder").gameObject;
    }

    private void Start()
    {
        LoadSettings();
        GameSpeed(1);
    }

    private void Update()
    {
        PauseCheck();
        GameOverCheck();
    }

    //if a stronghold exsits and the health is equal or less then 0 freeze game and show game over screen
    private void GameOverCheck()
    {
        if( strongHold != null)
        {
            if(strongHold.Health <= 0)
            {
                deathScreen.SetActive(true);
                settingsHolder.SetActive(false);
                menuHolder.SetActive(false);
                howToPlayeScreen.SetActive(false);
                GameSpeed(0);
            }
        }
    }

    //function that pauses the game speed and enables or disables the menu only usable for in game scene not main menu
    private void PauseCheck()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (Time.timeScale != 0)
            {
                unpaused_TimeScale = Time.timeScale;
            }

            if (Input.GetButtonDown("Cancel"))
            {
                isPaused = !isPaused;
                menuHolder.SetActive(isPaused);
                settingsHolder.SetActive(false);
                howToPlayeScreen.SetActive(false);
            }

            if (isPaused)
            {
                menuHolder.SetActive(true);
                GameSpeed(0f);
            }
            else
            {
                GameSpeed(unpaused_TimeScale);
            }
        }
    }

    //set the game spped for fast forwading or feezing it
    public void GameSpeed(float speed)
    {
        Time.timeScale = speed;
    }

    //restart level by getting curret level and reloading it
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //start game button load the map level scene
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    ///activate menu
    public void ActiveMenu()
    {
        menuHolder.SetActive(true);
        settingsHolder.SetActive(false);
    }

    //set Sound effect volume when slider is moved
    public void SetSfxVol()
    {
        sfx_Vol = sfx_Slider.value;
        PlayerPrefs.SetFloat("SfxVol", sfx_Vol);
        audioMixer.SetFloat("Sfx", sfx_Vol);
    }
    
    //set Msuic volume when slider is moved
    public void SetMusicVol()
    {
        music_Vol = music_Slider.value;
        PlayerPrefs.SetFloat("MusicVol", music_Vol);
        audioMixer.SetFloat("Music", music_Vol);
    }

    //set up all the player prefs for music and sound effects
    private void LoadSettings()
    {
        sfx_Vol = PlayerPrefs.GetFloat("SfxVol");
        music_Vol = PlayerPrefs.GetFloat("MusicVol");

        // change the value of the slider to mach the player prefs
        music_Slider.value = music_Vol;
        sfx_Slider.value = sfx_Vol;

        // set the mixer volume 
        audioMixer.SetFloat("Sfx", sfx_Vol);
        audioMixer.SetFloat("Music", music_Vol);
    }

    //go back to main menu
    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    //close application
    public void QuitApplication()
    {
        Application.Quit();
    }
}
