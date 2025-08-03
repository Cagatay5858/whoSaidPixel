using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Ayarlar men�s� UI kontrolc�s�
/// Ses ayarlar�n� y�netir ve sahne ge�i�lerini sa�lar
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
    /// UI elementlerini ba�lang�� de�erleriyle ayarlar
    /// </summary>
    private void InitializeUI()
    {
        // AudioManager'dan mevcut ses seviyelerini al
        if (AudioManager.Instance != null)
        {
            // Slider de�erlerini mevcut ses seviyelerine ayarla
            if (sfxSlider != null)
                sfxSlider.value = AudioManager.Instance.sfxVolume;

            if (musicSlider != null)
                musicSlider.value = AudioManager.Instance.musicVolume;
        }
        else
        {
            Debug.LogWarning("AudioManager bulunamad�! Ses ayarlar� �al��mayabilir.");
        }
    }

    /// <summary>
    /// UI elementlerine event listener'lar� ba�lar
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
    /// Geri butonu t�kland���nda �al���r
    /// </summary>
    private void OnBackButtonClicked()
    {
        // Ses efekti �al
        PlayButtonSFX();

        // Ana men�ye d�n
        LoadMainMenu();
    }

    /// <summary>
    /// SFX slider de�eri de�i�ti�inde �al���r
    /// </summary>
    private void OnSFXVolumeChanged(float value)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetSFXVolume(value);

            // Slider de�i�iklik ses efekti (�ok s�k �almamas� i�in throttle edilebilir)
            if (sliderChangeSFX != null)
            {
                AudioManager.Instance.PlaySFX(sliderChangeSFX, 0.3f);
            }
        }
    }

    /// <summary>
    /// Music slider de�eri de�i�ti�inde �al���r
    /// </summary>
    private void OnMusicVolumeChanged(float value)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetMusicVolume(value);
        }
    }

    /// <summary>
    /// Buton ses efekti �alar
    /// </summary>
    private void PlayButtonSFX()
    {
        if (AudioManager.Instance != null && buttonClickSFX != null)
        {
            AudioManager.Instance.PlaySFX(buttonClickSFX);
        }
    }

    /// <summary>
    /// Ana men� sahnesini y�kler
    /// </summary>
    private void LoadMainMenu()
    {
        // Sahne ge�i�i �ncesi ayarlar� kaydet
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SaveAudioSettings();
        }

        // Ana men� sahnesini y�kle
        SceneManager.LoadScene(mainMenuSceneName);
    }

    /// <summary>
    /// Ayarlar� varsay�lan de�erlere s�f�rlar
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
        // Event listener'lar� temizle
        if (backButton != null)
            backButton.onClick.RemoveAllListeners();

        if (sfxSlider != null)
            sfxSlider.onValueChanged.RemoveAllListeners();

        if (musicSlider != null)
            musicSlider.onValueChanged.RemoveAllListeners();
    }
}
