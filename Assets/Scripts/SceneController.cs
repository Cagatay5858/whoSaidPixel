using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneController : MonoBehaviour
{
    private List<int> randomSceneIndices = new List<int>();
    private int currentSceneIndex = 0;

    void Start()
    {
        // 1-9 arasý sahneleri ekle
        for (int i = 1; i <= 9; i++)
            randomSceneIndices.Add(i);

        // Karýþtýr
        ShuffleList(randomSceneIndices);

        // Ýlk sahneye geç
        LoadNextScene();
    }

    // Listeyi karýþtýrma fonksiyonu
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

    // Sýradaki sahneyi yükle
    public void LoadNextScene()
    {
        if (currentSceneIndex < randomSceneIndices.Count)
        {
            int nextScene = randomSceneIndices[currentSceneIndex];
            currentSceneIndex++;
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            // Son sahne: index 10
            SceneManager.LoadScene(10);
        }
    }
}
