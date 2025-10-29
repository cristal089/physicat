using UnityEngine;
using UnityEngine.SceneManagement; //Certifique esse namespace esta sendo usado

namespace UI
{
    public class GameOverController : MonoBehaviour
    {
        void Awake()
        {
            Time.timeScale = 1f;
        }
        public void StartGame()
        {
            SceneManager.LoadScene("LevelsMenu", LoadSceneMode.Single);
        }

        public void MainMenu()
        {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
    }
}

