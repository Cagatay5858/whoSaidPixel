using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;

    private void Start()
    {
        StartCoroutine(Spawner());
    }
    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(3);
        int i = 1;
        while(i != 2)
        {
            yield return wait;
            int rand = Random.Range(0, enemyPrefabs.Length);
            GameObject enemyToSpawn = enemyPrefabs[rand];

            Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
            i++;
        }
        
        
    }
}
