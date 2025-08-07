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
        for (int i = 3; i <= 10; i++) // 1-9 arasý sahneler
            randomSceneIndices.Add(i);

        ShuffleList(randomSceneIndices);
    }

    private void ShuffleList(List<int> list)
    {
        for (int i = 3; i < list.Count; i++)
        {
            int rand = Random.Range(i, 10);
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
            SceneManager.LoadScene(11); // Final sahnesi
        }
    }
}
