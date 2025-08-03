using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic; // Listeler için

/// <summary>
/// Cutscene sahnesini yöneten script.
/// Resimleri sýrayla gösterir, müzik çalar ve sonunda rastgele bir oyun sahnesine geçer.
/// </summary>
public class CutsceneManager : MonoBehaviour
{
    [Header("Cutscene Settings")]
    [SerializeField] private Image cutsceneImageDisplay; // Cutscene resimlerinin gösterileceði UI Image
    [SerializeField] private Sprite[] cutsceneImages; // Hikaye resimleri dizisi
    [SerializeField] private float imageDisplayDuration = 3f; // Her resmin gösterilme süresi
    [SerializeField] private AudioClip cutsceneMusic; // Cutscene sýrasýnda çalacak müzik

    [Header("Scene Management")]
    [SerializeField] private List<string> gameScenes; // Oyunun baþlayabileceði rastgele sahneler

    private int currentCutsceneImageIndex = 0; // Mevcut cutscene resminin indeksi

    void Start()
    {
        Debug.Log("CutsceneManager baþlatýldý.");

        // Cutscene Image'ý baþlangýçta gizle (eðer Inspector'da aktif býrakýldýysa)
        if (cutsceneImageDisplay != null)
        {
            cutsceneImageDisplay.gameObject.SetActive(false);
        }

        // Cutscene'i baþlat
        StartCoroutine(PlayCutscene());
    }

    /// <summary>
    /// Hikaye resimlerini sýrayla gösteren Coroutine.
    /// </summary>
    private IEnumerator PlayCutscene()
    {
        // Müzik çal (AudioManager varsa)
        if (AudioManager.Instance != null && cutsceneMusic != null)
        {
            AudioManager.Instance.PlayMusic(cutsceneMusic, true); // Müziði döngüde çal
            Debug.Log($"Cutscene müziði çalýnýyor: {cutsceneMusic.name}");
        }
        else if (AudioManager.Instance == null)
        {
            Debug.LogError("AudioManager sahneye yerleþtirilmemiþ veya baþlatýlmamýþ! Cutscene müziði çalýnamýyor.");
        }
        else if (cutsceneMusic == null)
        {
            Debug.LogWarning("Cutscene müziði atanmamýþ!");
        }

        if (cutsceneImages == null || cutsceneImages.Length == 0)
        {
            Debug.LogWarning("Cutscene resimleri atanmamýþ! Direkt oyun sahnesine geçiliyor.");
            LoadRandomGameScene();
            yield break; // Coroutine'i sonlandýr
        }

        if (cutsceneImageDisplay == null)
        {
            Debug.LogError("Cutscene Image Display atanmamýþ! Cutscene oynatýlamýyor.");
            LoadRandomGameScene();
            yield break;
        }

        // Cutscene Image'ý görünür yap
        cutsceneImageDisplay.gameObject.SetActive(true);

        // Her resmi sýrayla göster
        for (currentCutsceneImageIndex = 0; currentCutsceneImageIndex < cutsceneImages.Length; currentCutsceneImageIndex++)
        {
            cutsceneImageDisplay.sprite = cutsceneImages[currentCutsceneImageIndex];
            Debug.Log($"Cutscene resmi gösteriliyor: {currentCutsceneImageIndex + 1}/{cutsceneImages.Length}");
            yield return new WaitForSeconds(imageDisplayDuration);
        }

        // Cutscene bittikten sonra Image'ý gizle
        cutsceneImageDisplay.gameObject.SetActive(false);

        // Rastgele bir oyun sahnesi yükle
        LoadRandomGameScene();
    }

    /// <summary>
    /// Tanýmlanmýþ oyun sahnelerinden rastgele birini yükler.
    /// </summary>
    private void LoadRandomGameScene()
    {
        if (gameScenes == null || gameScenes.Count == 0)
        {
            Debug.LogError("Oyun sahneleri listesi boþ! Lütfen Build Settings'e sahne ekleyin ve CutsceneManager'da atayýn.");
            return;
        }

        // Rastgele bir sahne seç
        int randomIndex = Random.Range(0, gameScenes.Count);
        string sceneToLoad = gameScenes[randomIndex];

        Debug.Log($"Rastgele seçilen oyun sahnesi: {sceneToLoad}");

        // Sahnenin Build Settings'te olup olmadýðýný kontrol et
        if (Application.CanStreamedLevelBeLoaded(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogError($"Sahne bulunamadý: {sceneToLoad}. Lütfen Build Settings'te bu sahnenin ekli olduðundan emin olun.");
        }
    }
}
