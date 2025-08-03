using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Oyun genelinde ses yönetimini saðlayan Singleton AudioManager
/// MainAudioMixer ile entegre çalýþýr
/// </summary>
public class AudioManager : MonoBehaviour
{
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer mainAudioMixer; // MainAudioMixer'ýnýzý buraya sürükleyin

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    // Singleton pattern
    public static AudioManager Instance { get; private set; }

    // Ses seviyeleri (0-1 arasý)
    [Range(0f, 1f)] public float masterVolume = 1f;
    [Range(0f, 1f)] public float sfxVolume = 1f;
    [Range(0f, 1f)] public float musicVolume = 1f;

    // PlayerPrefs anahtarlarý
    private const string MASTER_VOLUME_KEY = "MasterVolume";
    private const string SFX_VOLUME_KEY = "SFXVolume";
    private const string MUSIC_VOLUME_KEY = "MusicVolume";

    // AudioMixer parameter isimleri (Exposed Parameters'daki isimlerle ayný olmalý)
    private const string MASTER_VOLUME_PARAM = "MasterVolume";
    private const string SFX_VOLUME_PARAM = "SFXVolume";
    private const string MUSIC_VOLUME_PARAM = "MusicVolume";

    private void Awake()
    {
        // Singleton kontrolü
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
        SetupAudioSources(); // AudioSource'larý AudioMixer'a baðla
    }

    /// <summary>
    /// AudioSource'larý doðru AudioMixerGroup'lara baðlar
    /// </summary>
    private void SetupAudioSources()
    {
        if (mainAudioMixer != null)
        {
            // AudioMixerGroup'larý bul
            AudioMixerGroup[] groups = mainAudioMixer.FindMatchingGroups("Master");

            if (groups.Length > 0)
            {
                // SFX ve Music group'larýný bul
                AudioMixerGroup sfxGroup = System.Array.Find(groups, group => group.name == "SFX");
                AudioMixerGroup musicGroup = System.Array.Find(groups, group => group.name == "Music");

                // AudioSource'larý baðla
                if (sfxSource != null && sfxGroup != null)
                    sfxSource.outputAudioMixerGroup = sfxGroup;

                if (musicSource != null && musicGroup != null)
                    musicSource.outputAudioMixerGroup = musicGroup;
            }
        }
    }

    /// <summary>
    /// Kaydedilmiþ ses ayarlarýný PlayerPrefs'ten yükler
    /// </summary>
    private void LoadAudioSettings()
    {
        masterVolume = PlayerPrefs.GetFloat(MASTER_VOLUME_KEY, 1f);
        sfxVolume = PlayerPrefs.GetFloat(SFX_VOLUME_KEY, 1f);
        musicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, 1f);
    }

    /// <summary>
    /// Ses ayarlarýný PlayerPrefs'e kaydeder
    /// </summary>
    public void SaveAudioSettings()
    {
        PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, masterVolume);
        PlayerPrefs.SetFloat(SFX_VOLUME_KEY, sfxVolume);
        PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, musicVolume);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Ses ayarlarýný AudioMixer'a uygular
    /// </summary>
    private void ApplyAudioSettings()
    {
        if (mainAudioMixer == null) return;

        // Volume deðerlerini decibel'e çevir (-80 ile 0 arasý)
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
    /// Müzik ses seviyesini ayarlar
    /// </summary>
    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        ApplyAudioSettings();
        SaveAudioSettings();
    }

    /// <summary>
    /// Ses efekti çalar
    /// </summary>
    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (clip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clip, volume);
        }
    }

    /// <summary>
    /// Müzik çalar (loop)
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
    /// Müziði durdurur
    /// </summary>
    public void StopMusic()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }
}
