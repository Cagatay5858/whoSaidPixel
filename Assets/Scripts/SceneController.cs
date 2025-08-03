using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

    private List<int> randomSceneIndices = new List<int>();
    private int currentSceneIndex = 0;

    void Awake()
    {
        // Singleton oluþtur
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Sahne deðiþse de yok olma
            PrepareSceneList();
        }
        else
        {
            Destroy(gameObject); // Çift kopyayý önle
        }
    }

    private void PrepareSceneList()
    {
        randomSceneIndices.Clear();
        for (int i = 1; i <= 9; i++) // 1-9 arasý sahneler
            randomSceneIndices.Add(i);

        ShuffleList(randomSceneIndices);
    }

    private void ShuffleList(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(i, list.Count);
            int temp = list[i];
            list[i] = list[rand];
            list[rand] = temp;
        }
    }

    public void LoadNextScene()
    {
        if (currentSceneIndex < randomSceneIndices.Count)
        {
            int nextSceneIndex = randomSceneIndices[currentSceneIndex];
            currentSceneIndex++;
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            SceneManager.LoadScene(10); // Final sahnesi
        }
    }
}
