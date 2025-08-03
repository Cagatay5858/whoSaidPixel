using UnityEngine;
using UnityEngine.UI; // UI elementleri için
using UnityEngine.SceneManagement; // Sahne geçiþleri için
using System.Collections.Generic; // Listeler için
using TMPro; // TextMeshPro kullanýyorsanýz

/// <summary>
/// Ana Menü UI'ýný yöneten script.
/// Buton fonksiyonlarýný içerir.
/// </summary>
public class MainMenuUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Button startButton; // Baþla butonu
    [SerializeField] private Button settingsButton; // Ayarlar butonu
    [SerializeField] private Button exitButton; // Çýkýþ butonu

    [Header("Scene Management")]
    [SerializeField] private string cutsceneSceneName = "Cutscene"; // Yeni cutscene sahnesinin adý
    [SerializeField] private string settingsSceneName = "Ayarlar"; // Ayarlar sahnesinin adý

    void Start()
    {
        // Butonlara týklama event'lerini baðla
        SetupButtonEvents();
        Debug.Log("MainMenuUI baþlatýldý.");
    }

    /// <summary>
    /// Butonlarýn OnClick event'lerini baðlar.
    /// </summary>
    private void SetupButtonEvents()
    {
        if (startButton != null)
        {
            startButton.onClick.RemoveAllListeners(); // Önceki listener'larý temizle
            startButton.onClick.AddListener(OnStartButtonClicked);
            Debug.Log("Baþla butonu event'i baðlandý.");
        }
        else
        {
            Debug.LogError("Baþla butonu atanmamýþ!");
        }

        if (settingsButton != null)
        {
            settingsButton.onClick.RemoveAllListeners();
            settingsButton.onClick.AddListener(OnSettingsButtonClicked);
            Debug.Log("Ayarlar butonu event'i baðlandý.");
        }
        else
        {
            Debug.LogError("Ayarlar butonu atanmamýþ!");
        }

        if (exitButton != null)
        {
            exitButton.onClick.RemoveAllListeners();
            exitButton.onClick.AddListener(OnExitButtonClicked);
            Debug.Log("Çýkýþ butonu event'i baðlandý.");
        }
        else
        {
            Debug.LogError("Çýkýþ butonu atanmamýþ!");
        }
    }

    /// <summary>
    /// Baþla butonuna týklandýðýnda çaðrýlýr.
    /// Cutscene sahnesini yükler.
    /// </summary>
    private void OnStartButtonClicked()
    {
        Debug.Log("Baþla butonuna týklandý! Cutscene sahnesine geçiliyor...");
        // Buton ses efekti çal (AudioManager varsa)
        if (AudioManager.Instance != null)
        {
            // AudioManager.Instance.PlaySFX(buttonClickSound); // Ýsteðe baðlý
        }

        // Cutscene sahnesini yükle
        if (!string.IsNullOrEmpty(cutsceneSceneName))
        {
            SceneManager.LoadScene(cutsceneSceneName);
        }
        else
        {
            Debug.LogError("Cutscene sahne adý atanmamýþ!");
        }
    }

    /// <summary>
    /// Ayarlar butonuna týklandýðýnda çaðrýlýr.
    /// Ayarlar sahnesini yükler.
    /// </summary>
    private void OnSettingsButtonClicked()
    {
        Debug.Log("Ayarlar butonuna týklandý! Ayarlar sahnesine geçiliyor...");
        // Buton ses efekti çal (AudioManager varsa)
        if (AudioManager.Instance != null)
        {
            // AudioManager.Instance.PlaySFX(buttonClickSound); // Ýsteðe baðlý
        }

        // Ayarlar sahnesini yükle
        if (!string.IsNullOrEmpty(settingsSceneName))
        {
            SceneManager.LoadScene(settingsSceneName);
        }
        else
        {
            Debug.LogError("Ayarlar sahne adý atanmamýþ!");
        }
    }

    /// <summary>
    /// Çýkýþ butonuna týklandýðýnda çaðrýlýr.
    /// Oyundan çýkar.
    /// </summary>
    private void OnExitButtonClicked()
    {
        Debug.Log("Çýkýþ butonuna týklandý! Oyundan çýkýlýyor...");
        // Buton ses efekti çal (AudioManager varsa)
        if (AudioManager.Instance != null)
        {
            // AudioManager.Instance.PlaySFX(buttonClickSound); // Ýsteðe baðlý
        }

        // Editörde çalýþýrken Application.Quit çalýþmaz, Debug.Log ile belirtiriz.
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit(); // Oyundan çýk
#endif
    }

    private void OnDestroy()
    {
        // Script yok edildiðinde event listener'larý temizle
        if (startButton != null)
            startButton.onClick.RemoveAllListeners();
        if (settingsButton != null)
            settingsButton.onClick.RemoveAllListeners();
        if (exitButton != null)
            exitButton.onClick.RemoveAllListeners();
    }
}
