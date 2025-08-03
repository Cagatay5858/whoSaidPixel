using UnityEngine;
using UnityEngine.UI; // UI elementleri i�in
using UnityEngine.SceneManagement; // Sahne ge�i�leri i�in
using System.Collections.Generic; // Listeler i�in
using TMPro; // TextMeshPro kullan�yorsan�z

/// <summary>
/// Ana Men� UI'�n� y�neten script.
/// Buton fonksiyonlar�n� i�erir.
/// </summary>
public class MainMenuUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Button startButton; // Ba�la butonu
    [SerializeField] private Button settingsButton; // Ayarlar butonu
    [SerializeField] private Button exitButton; // ��k�� butonu

    [Header("Scene Management")]
    [SerializeField] private string cutsceneSceneName = "Cutscene"; // Yeni cutscene sahnesinin ad�
    [SerializeField] private string settingsSceneName = "Ayarlar"; // Ayarlar sahnesinin ad�

    void Start()
    {
        // Butonlara t�klama event'lerini ba�la
        SetupButtonEvents();
        Debug.Log("MainMenuUI ba�lat�ld�.");
    }

    /// <summary>
    /// Butonlar�n OnClick event'lerini ba�lar.
    /// </summary>
    private void SetupButtonEvents()
    {
        if (startButton != null)
        {
            startButton.onClick.RemoveAllListeners(); // �nceki listener'lar� temizle
            startButton.onClick.AddListener(OnStartButtonClicked);
            Debug.Log("Ba�la butonu event'i ba�land�.");
        }
        else
        {
            Debug.LogError("Ba�la butonu atanmam��!");
        }

        if (settingsButton != null)
        {
            settingsButton.onClick.RemoveAllListeners();
            settingsButton.onClick.AddListener(OnSettingsButtonClicked);
            Debug.Log("Ayarlar butonu event'i ba�land�.");
        }
        else
        {
            Debug.LogError("Ayarlar butonu atanmam��!");
        }

        if (exitButton != null)
        {
            exitButton.onClick.RemoveAllListeners();
            exitButton.onClick.AddListener(OnExitButtonClicked);
            Debug.Log("��k�� butonu event'i ba�land�.");
        }
        else
        {
            Debug.LogError("��k�� butonu atanmam��!");
        }
    }

    /// <summary>
    /// Ba�la butonuna t�kland���nda �a�r�l�r.
    /// Cutscene sahnesini y�kler.
    /// </summary>
    private void OnStartButtonClicked()
    {
        Debug.Log("Ba�la butonuna t�kland�! Cutscene sahnesine ge�iliyor...");
        // Buton ses efekti �al (AudioManager varsa)
        if (AudioManager.Instance != null)
        {
            // AudioManager.Instance.PlaySFX(buttonClickSound); // �ste�e ba�l�
        }

        // Cutscene sahnesini y�kle
        if (!string.IsNullOrEmpty(cutsceneSceneName))
        {
            SceneManager.LoadScene(cutsceneSceneName);
        }
        else
        {
            Debug.LogError("Cutscene sahne ad� atanmam��!");
        }
    }

    /// <summary>
    /// Ayarlar butonuna t�kland���nda �a�r�l�r.
    /// Ayarlar sahnesini y�kler.
    /// </summary>
    private void OnSettingsButtonClicked()
    {
        Debug.Log("Ayarlar butonuna t�kland�! Ayarlar sahnesine ge�iliyor...");
        // Buton ses efekti �al (AudioManager varsa)
        if (AudioManager.Instance != null)
        {
            // AudioManager.Instance.PlaySFX(buttonClickSound); // �ste�e ba�l�
        }

        // Ayarlar sahnesini y�kle
        if (!string.IsNullOrEmpty(settingsSceneName))
        {
            SceneManager.LoadScene(settingsSceneName);
        }
        else
        {
            Debug.LogError("Ayarlar sahne ad� atanmam��!");
        }
    }

    /// <summary>
    /// ��k�� butonuna t�kland���nda �a�r�l�r.
    /// Oyundan ��kar.
    /// </summary>
    private void OnExitButtonClicked()
    {
        Debug.Log("��k�� butonuna t�kland�! Oyundan ��k�l�yor...");
        // Buton ses efekti �al (AudioManager varsa)
        if (AudioManager.Instance != null)
        {
            // AudioManager.Instance.PlaySFX(buttonClickSound); // �ste�e ba�l�
        }

        // Edit�rde �al���rken Application.Quit �al��maz, Debug.Log ile belirtiriz.
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit(); // Oyundan ��k
#endif
    }

    private void OnDestroy()
    {
        // Script yok edildi�inde event listener'lar� temizle
        if (startButton != null)
            startButton.onClick.RemoveAllListeners();
        if (settingsButton != null)
            settingsButton.onClick.RemoveAllListeners();
        if (exitButton != null)
            exitButton.onClick.RemoveAllListeners();
    }
}
