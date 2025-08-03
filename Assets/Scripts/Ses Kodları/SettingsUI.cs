using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Ayarlar menüsü UI kontrolcüsü
/// Ses ayarlarýný yönetir ve sahne geçiþlerini saðlar
/// </summary>
public class SettingsUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Button backButton;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;

    [Header("Scene Settings")]
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    [Header("Audio Feedback")]
    [SerializeField] private AudioClip buttonClickSFX;
    [SerializeField] private AudioClip sliderChangeSFX;

    private void Start()
    {
        InitializeUI();
        SetupEventListeners();
    }

    /// <summary>
    /// UI elementlerini baþlangýç deðerleriyle ayarlar
    /// </summary>
    private void InitializeUI()
    {
        // AudioManager'dan mevcut ses seviyelerini al
        if (AudioManager.Instance != null)
        {
            // Slider deðerlerini mevcut ses seviyelerine ayarla
            if (sfxSlider != null)
                sfxSlider.value = AudioManager.Instance.sfxVolume;

            if (musicSlider != null)
                musicSlider.value = AudioManager.Instance.musicVolume;
        }
        else
        {
            Debug.LogWarning("AudioManager bulunamadý! Ses ayarlarý çalýþmayabilir.");
        }
    }

    /// <summary>
    /// UI elementlerine event listener'larý baðlar
    /// </summary>
    private void SetupEventListeners()
    {
        // Geri butonu
        if (backButton != null)
        {
            backButton.onClick.AddListener(OnBackButtonClicked);
        }

        // SFX Slider
        if (sfxSlider != null)
        {
            sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
        }

        // Music Slider
        if (musicSlider != null)
        {
            musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        }
    }

    /// <summary>
    /// Geri butonu týklandýðýnda çalýþýr
    /// </summary>
    private void OnBackButtonClicked()
    {
        // Ses efekti çal
        PlayButtonSFX();

        // Ana menüye dön
        LoadMainMenu();
    }

    /// <summary>
    /// SFX slider deðeri deðiþtiðinde çalýþýr
    /// </summary>
    private void OnSFXVolumeChanged(float value)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetSFXVolume(value);

            // Slider deðiþiklik ses efekti (çok sýk çalmamasý için throttle edilebilir)
            if (sliderChangeSFX != null)
            {
                AudioManager.Instance.PlaySFX(sliderChangeSFX, 0.3f);
            }
        }
    }

    /// <summary>
    /// Music slider deðeri deðiþtiðinde çalýþýr
    /// </summary>
    private void OnMusicVolumeChanged(float value)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetMusicVolume(value);
        }
    }

    /// <summary>
    /// Buton ses efekti çalar
    /// </summary>
    private void PlayButtonSFX()
    {
        if (AudioManager.Instance != null && buttonClickSFX != null)
        {
            AudioManager.Instance.PlaySFX(buttonClickSFX);
        }
    }

    /// <summary>
    /// Ana menü sahnesini yükler
    /// </summary>
    private void LoadMainMenu()
    {
        // Sahne geçiþi öncesi ayarlarý kaydet
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SaveAudioSettings();
        }

        // Ana menü sahnesini yükle
        SceneManager.LoadScene(mainMenuSceneName);
    }

    /// <summary>
    /// Ayarlarý varsayýlan deðerlere sýfýrlar
    /// </summary>
    public void ResetToDefaults()
    {
        if (sfxSlider != null)
            sfxSlider.value = 1f;

        if (musicSlider != null)
            musicSlider.value = 1f;

        PlayButtonSFX();
    }

    private void OnDestroy()
    {
        // Event listener'larý temizle
        if (backButton != null)
            backButton.onClick.RemoveAllListeners();

        if (sfxSlider != null)
            sfxSlider.onValueChanged.RemoveAllListeners();

        if (musicSlider != null)
            musicSlider.onValueChanged.RemoveAllListeners();
    }
}
