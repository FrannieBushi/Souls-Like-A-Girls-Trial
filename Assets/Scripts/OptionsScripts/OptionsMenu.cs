using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    public Slider volumeSlider;
    public float sliderValue;
    public Image imagenMute;

    public Toggle toggle;

    public TMP_Dropdown dropdown;
    public int quality;


    private void Start()
    {
        
        volumeSlider.value = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
        AudioListener.volume = volumeSlider.value;
        MuteCheck();

        if(Screen.fullScreen)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }

        quality = PlayerPrefs.GetInt("QualityNumber", 3);
        dropdown.value = quality;
        SetQuality();
   
    }

    public void ChangeSlider(float valor)
    {
        sliderValue = valor;
        PlayerPrefs.SetFloat("volumenAudio", sliderValue);
        AudioListener.volume = volumeSlider.value;
        MuteCheck();
    }

    public void MuteCheck()
    {
        if(sliderValue == 0)
        {
            imagenMute.enabled = true;
        }
        else
        {
            imagenMute.enabled = false;
        }
    }

    public void activateFullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }

    public void SetQuality()
    {
        QualitySettings.SetQualityLevel(dropdown.value);
        PlayerPrefs.SetInt("QualityNumber", dropdown.value);
        quality = dropdown.value;
    }

    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene("Menu"); 
    }
}

