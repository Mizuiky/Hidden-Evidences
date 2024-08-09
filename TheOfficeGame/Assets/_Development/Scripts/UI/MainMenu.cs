using UnityEngine;
using UnityEngine.SceneManagement;

namespace JAM.Scene
{
    public class MainMenu : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene("afterMenu");
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}