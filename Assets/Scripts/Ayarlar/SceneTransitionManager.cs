using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// Sahne geçiþlerini yöneten manager
/// Smooth geçiþler ve loading screen'ler için kullanýlabilir
/// </summary>
public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance { get; private set; }

    [Header("Transition Settings")]
    [SerializeField] private float transitionDuration = 0.5f;
    [SerializeField] private CanvasGroup fadeCanvasGroup;

    private bool isTransitioning = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Fade efekti ile sahne geçiþi yapar
    /// </summary>
    public void LoadSceneWithFade(string sceneName)
    {
        if (!isTransitioning)
        {
            StartCoroutine(LoadSceneCoroutine(sceneName));
        }
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        isTransitioning = true;

        // Fade out
        if (fadeCanvasGroup != null)
        {
            yield return StartCoroutine(FadeCoroutine(0f, 1f));
        }

        // Sahneyi yükle
        SceneManager.LoadScene(sceneName);

        // Fade in
        if (fadeCanvasGroup != null)
        {
            yield return StartCoroutine(FadeCoroutine(1f, 0f));
        }

        isTransitioning = false;
    }

    private IEnumerator FadeCoroutine(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / transitionDuration);
            fadeCanvasGroup.alpha = alpha;
            yield return null;
        }

        fadeCanvasGroup.alpha = endAlpha;
    }
}
