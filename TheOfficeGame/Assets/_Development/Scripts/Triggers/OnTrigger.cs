using UnityEngine;
using UnityEngine.SceneManagement;
public class OnTrigger : MonoBehaviour
{
    public string name;
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if(collision.CompareTag("Player"))
        {
            PlayerController ply = collision.gameObject.GetComponent<PlayerController>();
            if (ply != null)
                SceneManager.LoadScene(name);

        }
    }
}
