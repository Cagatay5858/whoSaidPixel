using UnityEngine;

public class PlayerSceneTrigger : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneController.Instance.LoadNextScene();
        }
    }
}
