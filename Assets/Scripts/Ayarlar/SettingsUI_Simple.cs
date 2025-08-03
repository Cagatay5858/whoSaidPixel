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
        Debug.Log("SettingsUI baþlatýldý");

        // Kaydedilmiþ ayarlarý yükle
        LoadSettings();

        // Slider'larý ayarla
        if (sfxSlider != null)
        {
            sfxSlider.value = sfxVolume;
            Debug.Log("SFX Slider ayarlandý: " + sfxVolume);
        }
        else
        {
            Debug.LogError("SFX Slider atanmamýþ!");
        }

        if (musicSlider != null)
        {
            musicSlider.value = musicVolume;
            Debug.Log("Music Slider ayarlandý: " + musicVolume);
        }
        else
        {
            Debug.LogError("Music Slider atanmamýþ!");
        }

        // Event'leri baðla
        SetupEvents();
    }

    void SetupEvents()
    {
        if (backButton != null)
        {
            // Önce mevcut listener'larý temizle
            backButton.onClick.RemoveAllListeners();
            // Yeni listener ekle
            backButton.onClick.AddListener(GoBackToMainMenu);
            Debug.Log("Back Button event'i baðlandý");
        }
        else
        {
            Debug.LogError("Back Button atanmamýþ!");
        }

        if (sfxSlider != null)
        {
            sfxSlider.onValueChanged.RemoveAllListeners();
            sfxSlider.onValueChanged.AddListener(OnSFXChanged);
            Debug.Log("SFX Slider event'i baðlandý");
        }

        if (musicSlider != null)
        {
            musicSlider.onValueChanged.RemoveAllListeners();
            musicSlider.onValueChanged.AddListener(OnMusicChanged);
            Debug.Log("Music Slider event'i baðlandý");
        }
    }

    public void OnSFXChanged(float value)
    {
        sfxVolume = value;
        SaveSettings();
        Debug.Log("SFX Volume deðiþti: " + value);
    }

    public void OnMusicChanged(float value)
    {
        musicVolume = value;
        SaveSettings();
        Debug.Log("Music Volume deðiþti: " + value);
    }

    public void GoBackToMainMenu()
    {
        Debug.Log("GoBackToMainMenu fonksiyonu çaðrýldý!");
        Debug.Log("Yüklenecek sahne: " + mainMenuSceneName);

        // Sahne var mý kontrol et
        if (Application.CanStreamedLevelBeLoaded(mainMenuSceneName))
        {
            SceneManager.LoadScene(mainMenuSceneName);
        }
        else
        {
            Debug.LogError("Sahne bulunamadý: " + mainMenuSceneName +
                          ". Build Settings'te bu sahne var mý kontrol edin!");
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
        Debug.Log("Ayarlar yüklendi - SFX: " + sfxVolume + ", Music: " + musicVolume);
    }

    // Test için public fonksiyon
    public void TestButton()
    {
        Debug.Log("Test butonu çalýþýyor!");
    }
}
