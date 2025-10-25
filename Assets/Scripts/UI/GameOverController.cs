using UnityEngine;
using UnityEngine.SceneManagement; //Certifique esse namespace esta sendo usado

namespace UI
{
    public class GameOverController : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene("LevelsMenu");
        }

        public void MainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}

