using UnityEngine;

/// <summary>
/// Belirli bir sahne yüklendiðinde müzik çalmak için kullanýlýr.
/// </summary>
public class SceneMusicPlayer : MonoBehaviour
{
    [Header("Music Settings")]
    [SerializeField] private AudioClip sceneMusicClip; // Bu sahneye özel müzik dosyasý
    [SerializeField] private bool loopMusic = true; // Müziðin döngüde çalýnýp çalýnmayacaðý

    void Start()
    {
        // AudioManager'ýn var olduðundan emin ol
        if (AudioManager.Instance != null)
        {
            // Eðer bu sahne için bir müzik atanmýþsa
            if (sceneMusicClip != null)
            {
                // AudioManager'a müziði çalmasýný söyle
                AudioManager.Instance.PlayMusic(sceneMusicClip, loopMusic);
                Debug.Log($"Sahne müziði çalýnýyor: {sceneMusicClip.name}");
            }
            else
            {
                Debug.LogWarning($"Bu sahnede müzik atanmamýþ: {gameObject.scene.name}.");
                // Ýsteðe baðlý: Eðer müzik atanmamýþsa müziði durdur
                // AudioManager.Instance.StopMusic(); 
            }
        }
        else
        {
            Debug.LogError("AudioManager sahneye yerleþtirilmemiþ veya baþlatýlmamýþ! Müzik çalýnamýyor.");
        }
    }
}
