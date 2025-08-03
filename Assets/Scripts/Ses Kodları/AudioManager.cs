using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Oyun genelinde ses y�netimini sa�layan Singleton AudioManager
/// MainAudioMixer ile entegre �al���r
/// </summary>
public class AudioManager : MonoBehaviour
{
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer mainAudioMixer; // MainAudioMixer'�n�z� buraya s�r�kleyin

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    // Singleton pattern
    public static AudioManager Instance { get; private set; }

    // Ses seviyeleri (0-1 aras�)
    [Range(0f, 1f)] public float masterVolume = 1f;
    [Range(0f, 1f)] public float sfxVolume = 1f;
    [Range(0f, 1f)] public float musicVolume = 1f;

    // PlayerPrefs anahtarlar�
    private const string MASTER_VOLUME_KEY = "MasterVolume";
    private const string SFX_VOLUME_KEY = "SFXVolume";
    private const string MUSIC_VOLUME_KEY = "MusicVolume";

    // AudioMixer parameter isimleri (Exposed Parameters'daki isimlerle ayn� olmal�)
    private const string MASTER_VOLUME_PARAM = "MasterVolume";
    private const string SFX_VOLUME_PARAM = "SFXVolume";
    private const string MUSIC_VOLUME_PARAM = "MusicVolume";

    private void Awake()
    {
        // Singleton kontrol�
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadAudioSettings();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ApplyAudioSettings();
        SetupAudioSources(); // AudioSource'lar� AudioMixer'a ba�la
    }

    /// <summary>
    /// AudioSource'lar� do�ru AudioMixerGroup'lara ba�lar
    /// </summary>
    private void SetupAudioSources()
    {
        if (mainAudioMixer != null)
        {
            // AudioMixerGroup'lar� bul
            AudioMixerGroup[] groups = mainAudioMixer.FindMatchingGroups("Master");

            if (groups.Length > 0)
            {
                // SFX ve Music group'lar�n� bul
                AudioMixerGroup sfxGroup = System.Array.Find(groups, group => group.name == "SFX");
                AudioMixerGroup musicGroup = System.Array.Find(groups, group => group.name == "Music");

                // AudioSource'lar� ba�la
                if (sfxSource != null && sfxGroup != null)
                    sfxSource.outputAudioMixerGroup = sfxGroup;

                if (musicSource != null && musicGroup != null)
                    musicSource.outputAudioMixerGroup = musicGroup;
            }
        }
    }

    /// <summary>
    /// Kaydedilmi� ses ayarlar�n� PlayerPrefs'ten y�kler
    /// </summary>
    private void LoadAudioSettings()
    {
        masterVolume = PlayerPrefs.GetFloat(MASTER_VOLUME_KEY, 1f);
        sfxVolume = PlayerPrefs.GetFloat(SFX_VOLUME_KEY, 1f);
        musicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, 1f);
    }

    /// <summary>
    /// Ses ayarlar�n� PlayerPrefs'e kaydeder
    /// </summary>
    public void SaveAudioSettings()
    {
        PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, masterVolume);
        PlayerPrefs.SetFloat(SFX_VOLUME_KEY, sfxVolume);
        PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, musicVolume);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Ses ayarlar�n� AudioMixer'a uygular
    /// </summary>
    private void ApplyAudioSettings()
    {
        if (mainAudioMixer == null) return;

        // Volume de�erlerini decibel'e �evir (-80 ile 0 aras�)
        // 0 volume = -80dB (sessiz), 1 volume = 0dB (maksimum)
        float masterDB = masterVolume > 0.0001f ? Mathf.Log10(masterVolume) * 20 : -80f;
        float sfxDB = sfxVolume > 0.0001f ? Mathf.Log10(sfxVolume) * 20 : -80f;
        float musicDB = musicVolume > 0.0001f ? Mathf.Log10(musicVolume) * 20 : -80f;

        // AudioMixer parametrelerini ayarla
        mainAudioMixer.SetFloat(MASTER_VOLUME_PARAM, masterDB);
        mainAudioMixer.SetFloat(SFX_VOLUME_PARAM, sfxDB);
        mainAudioMixer.SetFloat(MUSIC_VOLUME_PARAM, musicDB);
    }

    /// <summary>
    /// Master ses seviyesini ayarlar
    /// </summary>
    public void SetMasterVolume(float volume)
    {
        masterVolume = Mathf.Clamp01(volume);
        ApplyAudioSettings();
        SaveAudioSettings();
    }

    /// <summary>
    /// SFX ses seviyesini ayarlar
    /// </summary>
    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        ApplyAudioSettings();
        SaveAudioSettings();
    }

    /// <summary>
    /// M�zik ses seviyesini ayarlar
    /// </summary>
    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        ApplyAudioSettings();
        SaveAudioSettings();
    }

    /// <summary>
    /// Ses efekti �alar
    /// </summary>
    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (clip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clip, volume);
        }
    }

    /// <summary>
    /// M�zik �alar (loop)
    /// </summary>
    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        if (clip != null && musicSource != null)
        {
            musicSource.clip = clip;
            musicSource.loop = loop;
            musicSource.Play();
        }
    }

    /// <summary>
    /// M�zi�i durdurur
    /// </summary>
    public void StopMusic()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }
}
