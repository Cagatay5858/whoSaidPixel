using UnityEngine;

/// <summary>
/// Belirli bir sahne y�klendi�inde m�zik �almak i�in kullan�l�r.
/// </summary>
public class SceneMusicPlayer : MonoBehaviour
{
    [Header("Music Settings")]
    [SerializeField] private AudioClip sceneMusicClip; // Bu sahneye �zel m�zik dosyas�
    [SerializeField] private bool loopMusic = true; // M�zi�in d�ng�de �al�n�p �al�nmayaca��

    void Start()
    {
        // AudioManager'�n var oldu�undan emin ol
        if (AudioManager.Instance != null)
        {
            // E�er bu sahne i�in bir m�zik atanm��sa
            if (sceneMusicClip != null)
            {
                // AudioManager'a m�zi�i �almas�n� s�yle
                AudioManager.Instance.PlayMusic(sceneMusicClip, loopMusic);
                Debug.Log($"Sahne m�zi�i �al�n�yor: {sceneMusicClip.name}");
            }
            else
            {
                Debug.LogWarning($"Bu sahnede m�zik atanmam��: {gameObject.scene.name}.");
                // �ste�e ba�l�: E�er m�zik atanmam��sa m�zi�i durdur
                // AudioManager.Instance.StopMusic(); 
            }
        }
        else
        {
            Debug.LogError("AudioManager sahneye yerle�tirilmemi� veya ba�lat�lmam��! M�zik �al�nam�yor.");
        }
    }
}
