using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{

    [Header("Audio Settings")]
    [SerializeField] private AudioMixer MasterMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider masterSlider;
    [Space(10)]

    [Header("Resolution Settings")]
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;
    private float currentRefreshRate;
    private int currentResolutionIndex = 0;
    [Space(10)]	

    [Header("Brightness Settings")]	
    public Slider slider;
    public float sliderValue;
    public Image panelBrillo;
    public float valorBlack;
    public float valorWhite;
    
    private void Start()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
            SetSFXVolume();
            SetMasterVolume();
        }

        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();

        resolutionDropdown.ClearOptions();
        currentRefreshRate = (float)Screen.currentResolution.refreshRateRatio.value;

        Debug.Log("Current Refresh Rate: " + currentRefreshRate);

        for (int i = 0; i < resolutions.Length; i++)
        {
            Debug.Log("Resolution: " + resolutions[i]);
            if ((float)resolutions[i].refreshRateRatio.value == currentRefreshRate)
            {
                filteredResolutions.Add(resolutions[i]);
            }
        }

        // Automatically set the highest resolution
        Resolution highestResolution = filteredResolutions[filteredResolutions.Count - 1];
        Screen.SetResolution(highestResolution.width, highestResolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionWidth", highestResolution.width);
        PlayerPrefs.SetInt("ResolutionHeight", highestResolution.height);

        List<string> options = new List<string>();
        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            string ResolutionOption = filteredResolutions[i].width + " x " + filteredResolutions[i].height + " " + filteredResolutions[i].refreshRateRatio.value.ToString("0.##") + " Hz";
            options.Add(ResolutionOption);
            if (filteredResolutions[i].width == highestResolution.width && filteredResolutions[i].height == highestResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // settings del brillo
        slider.value = PlayerPrefs.GetFloat("Brillo", 0.5f);
        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, sliderValue / 3);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = filteredResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionWidth", resolution.width);
        PlayerPrefs.SetInt("ResolutionHeight", resolution.height);
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        MasterMixer.SetFloat("MusicVol", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        MasterMixer.SetFloat("SFXVol", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void SetMasterVolume()
    {
        float volume = masterSlider.value;
        MasterMixer.SetFloat("MasterVol", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");

        SetMusicVolume();
        SetSFXVolume();
        SetMasterVolume();
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        PlayerPrefs.SetInt("FullScreen", isFullScreen ? 1 : 0);
    }
    
    void Update()
    {
        valorBlack = 1 - sliderValue - 0.5f;
        valorWhite = sliderValue - 0.5f;
        if (sliderValue < 0.5f)
        {
            panelBrillo.color = new Color( 0, 0, 0, valorBlack);
        }
        if (sliderValue > 0.5f)
        {
            panelBrillo.color = new Color(255, 255, 255, valorWhite);
        }
    }
    public void ChangeSliderBrillo(float valor)
    {
        sliderValue = valor;
        PlayerPrefs.SetFloat("Brillo", sliderValue);
        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, sliderValue / 3);
    }
}