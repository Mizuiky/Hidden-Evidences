using UnityEngine;
using UnityEngine.SceneManagement;

public class OnTrigger : MonoBehaviour
{
    public int sceneIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            Invoke("LoadSceneById", 0.05f);
    }

    private void LoadSceneById()
    {     
        SceneManager.LoadSceneAsync(sceneIndex);
    }
}
