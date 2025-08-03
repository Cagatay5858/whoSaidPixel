using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic; // Listeler i�in

/// <summary>
/// Cutscene sahnesini y�neten script.
/// Resimleri s�rayla g�sterir, m�zik �alar ve sonunda rastgele bir oyun sahnesine ge�er.
/// </summary>
public class CutsceneManager : MonoBehaviour
{
    [Header("Cutscene Settings")]
    [SerializeField] private Image cutsceneImageDisplay; // Cutscene resimlerinin g�sterilece�i UI Image
    [SerializeField] private Sprite[] cutsceneImages; // Hikaye resimleri dizisi
    [SerializeField] private float imageDisplayDuration = 3f; // Her resmin g�sterilme s�resi
    [SerializeField] private AudioClip cutsceneMusic; // Cutscene s�ras�nda �alacak m�zik

    [Header("Scene Management")]
    [SerializeField] private List<string> gameScenes; // Oyunun ba�layabilece�i rastgele sahneler

    private int currentCutsceneImageIndex = 0; // Mevcut cutscene resminin indeksi

    void Start()
    {
        Debug.Log("CutsceneManager ba�lat�ld�.");

        // Cutscene Image'� ba�lang��ta gizle (e�er Inspector'da aktif b�rak�ld�ysa)
        if (cutsceneImageDisplay != null)
        {
            cutsceneImageDisplay.gameObject.SetActive(false);
        }

        // Cutscene'i ba�lat
        StartCoroutine(PlayCutscene());
    }

    /// <summary>
    /// Hikaye resimlerini s�rayla g�steren Coroutine.
    /// </summary>
    private IEnumerator PlayCutscene()
    {
        // M�zik �al (AudioManager varsa)
        if (AudioManager.Instance != null && cutsceneMusic != null)
        {
            AudioManager.Instance.PlayMusic(cutsceneMusic, true); // M�zi�i d�ng�de �al
            Debug.Log($"Cutscene m�zi�i �al�n�yor: {cutsceneMusic.name}");
        }
        else if (AudioManager.Instance == null)
        {
            Debug.LogError("AudioManager sahneye yerle�tirilmemi� veya ba�lat�lmam��! Cutscene m�zi�i �al�nam�yor.");
        }
        else if (cutsceneMusic == null)
        {
            Debug.LogWarning("Cutscene m�zi�i atanmam��!");
        }

        if (cutsceneImages == null || cutsceneImages.Length == 0)
        {
            Debug.LogWarning("Cutscene resimleri atanmam��! Direkt oyun sahnesine ge�iliyor.");
            LoadRandomGameScene();
            yield break; // Coroutine'i sonland�r
        }

        if (cutsceneImageDisplay == null)
        {
            Debug.LogError("Cutscene Image Display atanmam��! Cutscene oynat�lam�yor.");
            LoadRandomGameScene();
            yield break;
        }

        // Cutscene Image'� g�r�n�r yap
        cutsceneImageDisplay.gameObject.SetActive(true);

        // Her resmi s�rayla g�ster
        for (currentCutsceneImageIndex = 0; currentCutsceneImageIndex < cutsceneImages.Length; currentCutsceneImageIndex++)
        {
            cutsceneImageDisplay.sprite = cutsceneImages[currentCutsceneImageIndex];
            Debug.Log($"Cutscene resmi g�steriliyor: {currentCutsceneImageIndex + 1}/{cutsceneImages.Length}");
            yield return new WaitForSeconds(imageDisplayDuration);
        }

        // Cutscene bittikten sonra Image'� gizle
        cutsceneImageDisplay.gameObject.SetActive(false);

        // Rastgele bir oyun sahnesi y�kle
        LoadRandomGameScene();
    }

    /// <summary>
    /// Tan�mlanm�� oyun sahnelerinden rastgele birini y�kler.
    /// </summary>
    private void LoadRandomGameScene()
    {
        if (gameScenes == null || gameScenes.Count == 0)
        {
            Debug.LogError("Oyun sahneleri listesi bo�! L�tfen Build Settings'e sahne ekleyin ve CutsceneManager'da atay�n.");
            return;
        }

        // Rastgele bir sahne se�
        int randomIndex = Random.Range(0, gameScenes.Count);
        string sceneToLoad = gameScenes[randomIndex];

        Debug.Log($"Rastgele se�ilen oyun sahnesi: {sceneToLoad}");

        // Sahnenin Build Settings'te olup olmad���n� kontrol et
        if (Application.CanStreamedLevelBeLoaded(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogError($"Sahne bulunamad�: {sceneToLoad}. L�tfen Build Settings'te bu sahnenin ekli oldu�undan emin olun.");
        }
    }
}
