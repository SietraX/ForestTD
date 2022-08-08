using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    [Header("Volume Settings")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;

    [Header("Graphic Settings")]
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private TMP_Text brightnessTextValue = null;
    [SerializeField] private float defaultBrightness = 1;

    private int _qualityLevel;
    private bool _isFullscreen;
    private float _brightnessLevel;

    [Header("Levels to Load")]
    public string _newGameLevel;
    private string levelToLoad;
    [SerializeField] private GameObject noSavedGameDialog = null;

    [Header("Resolution Dropdowns")]
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;
    private void Start()
    {
        if (PlayerPrefs.HasKey("masterVolume"))
        {
            float masterVolume = PlayerPrefs.GetFloat("masterVolume");
            SetVolume(masterVolume);
        }
        if (PlayerPrefs.HasKey("masterBrightness"))
        {
            float masterBrightness = PlayerPrefs.GetFloat("masterBrightness");
            SetBrightness(masterBrightness);
        }
        if (PlayerPrefs.HasKey("masterFullscreen"))
        {
            bool masterFullscreen = PlayerPrefs.GetInt("masterFullscreen") == 1;
            SetFullScreen(masterFullscreen);
        }
        if (PlayerPrefs.HasKey("masterQuality"))
        {
            int masterQuality = PlayerPrefs.GetInt("masterQuality");
            SetQuality(masterQuality);
        }



        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void NewGameDialogYes()
    {
        SceneManager.LoadScene(_newGameLevel);
    }

    public void LoadGameDialogYes()
    {
        if (PlayerPrefs.HasKey("SavedLevel"))
        {
            levelToLoad = PlayerPrefs.GetString("SavedLevel");
            SceneManager.LoadScene(levelToLoad);
        }
        else
        {
            noSavedGameDialog.SetActive(true);
        }
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeTextValue.text = "%" + volume.ToString("0");
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
    }

    public void SetBrightness(float brightness)
    {

        _brightnessLevel = brightness;
        brightnessTextValue.text = brightness.ToString();
        PlayerPrefs.SetFloat("masterBrightness", _brightnessLevel);
        Screen.brightness = _brightnessLevel / 100;

    }
    public void SetFullScreen(bool isFullscreen)
    {
        _isFullscreen = isFullscreen;
        PlayerPrefs.SetInt("masterFullscreen", (_isFullscreen ? 1 : 0));
        Screen.fullScreen = _isFullscreen;
    }

    public void SetQuality(int qualityIndex)
    {
        _qualityLevel = qualityIndex;
        PlayerPrefs.SetInt("masterQuality", _qualityLevel);
        QualitySettings.SetQualityLevel(_qualityLevel);
    }
}