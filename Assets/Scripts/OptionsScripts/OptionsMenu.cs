using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    public Toggle fullscreenToggle;
    public Slider volumeSlider;
    public Dropdown qualityDropdown;
    public AudioMixer audioMixer;

    private void Start()
    {
        
        fullscreenToggle.isOn = Screen.fullScreen;
        volumeSlider.value = PlayerPrefs.GetFloat("volume", 1f);
        qualityDropdown.value = PlayerPrefs.GetInt("quality", QualitySettings.GetQualityLevel());

        ApplyVolume(volumeSlider.value);
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("fullscreen", isFullscreen ? 1 : 0);
    }

    public void SetVolume(float volume)
    {
        ApplyVolume(volume);
        PlayerPrefs.SetFloat("volume", volume);
    }

    private void ApplyVolume(float volume)
    {
        audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20); 
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("quality", qualityIndex);
    }

    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); 
    }
}

