using System;
using System.Collections.Generic;
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

    Resolution[] resolutions;

    [Header("Resolution Settings")]
    public Dropdown resolutionDropdown;     

    private void Start()
    {
        if(PlayerPrefs.HasKey("MusicVolume"))
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

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
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
}
