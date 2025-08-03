using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsUI_Simple : MonoBehaviour
{
    [Header("UI References")]
    public Button backButton;
    public Slider sfxSlider;
    public Slider musicSlider;

    [Header("Scene Settings")]
    public string mainMenuSceneName = "AnaMenu";

    // Ses seviyeleri
    private float sfxVolume = 1f;
    private float musicVolume = 1f;

    void Start()
    {
        Debug.Log("SettingsUI ba�lat�ld�");

        // Kaydedilmi� ayarlar� y�kle
        LoadSettings();

        // Slider'lar� ayarla
        if (sfxSlider != null)
        {
            sfxSlider.value = sfxVolume;
            Debug.Log("SFX Slider ayarland�: " + sfxVolume);
        }
        else
        {
            Debug.LogError("SFX Slider atanmam��!");
        }

        if (musicSlider != null)
        {
            musicSlider.value = musicVolume;
            Debug.Log("Music Slider ayarland�: " + musicVolume);
        }
        else
        {
            Debug.LogError("Music Slider atanmam��!");
        }

        // Event'leri ba�la
        SetupEvents();
    }

    void SetupEvents()
    {
        if (backButton != null)
        {
            // �nce mevcut listener'lar� temizle
            backButton.onClick.RemoveAllListeners();
            // Yeni listener ekle
            backButton.onClick.AddListener(GoBackToMainMenu);
            Debug.Log("Back Button event'i ba�land�");
        }
        else
        {
            Debug.LogError("Back Button atanmam��!");
        }

        if (sfxSlider != null)
        {
            sfxSlider.onValueChanged.RemoveAllListeners();
            sfxSlider.onValueChanged.AddListener(OnSFXChanged);
            Debug.Log("SFX Slider event'i ba�land�");
        }

        if (musicSlider != null)
        {
            musicSlider.onValueChanged.RemoveAllListeners();
            musicSlider.onValueChanged.AddListener(OnMusicChanged);
            Debug.Log("Music Slider event'i ba�land�");
        }
    }

    public void OnSFXChanged(float value)
    {
        sfxVolume = value;
        SaveSettings();
        Debug.Log("SFX Volume de�i�ti: " + value);
    }

    public void OnMusicChanged(float value)
    {
        musicVolume = value;
        SaveSettings();
        Debug.Log("Music Volume de�i�ti: " + value);
    }

    public void GoBackToMainMenu()
    {
        Debug.Log("GoBackToMainMenu fonksiyonu �a�r�ld�!");
        Debug.Log("Y�klenecek sahne: " + mainMenuSceneName);

        // Sahne var m� kontrol et
        if (Application.CanStreamedLevelBeLoaded(mainMenuSceneName))
        {
            SceneManager.LoadScene(mainMenuSceneName);
        }
        else
        {
            Debug.LogError("Sahne bulunamad�: " + mainMenuSceneName +
                          ". Build Settings'te bu sahne var m� kontrol edin!");
        }
    }

    void SaveSettings()
    {
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.Save();
        Debug.Log("Ayarlar kaydedildi - SFX: " + sfxVolume + ", Music: " + musicVolume);
    }

    void LoadSettings()
    {
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        Debug.Log("Ayarlar y�klendi - SFX: " + sfxVolume + ", Music: " + musicVolume);
    }

    // Test i�in public fonksiyon
    public void TestButton()
    {
        Debug.Log("Test butonu �al���yor!");
    }
}
