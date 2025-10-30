using UnityEngine;
using UnityEngine.SceneManagement; //Certifique esse namespace esta sendo usado

namespace UI
{
    public class LevelsUIController : MonoBehaviour
    {
        void Awake()
        {
            Time.timeScale = 1f;
        }

        public void Level1()
        {
            SceneManager.LoadScene("Level1Dialogue", LoadSceneMode.Single);
        }

        public void OpenNotebook1()
        {
            SceneManager.LoadScene("Level1Explanation", LoadSceneMode.Single);
        }

        public void Level2()
        {
            SceneManager.LoadScene("Level2Dialogue", LoadSceneMode.Single);
        }

        public void Back()
        {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
    }
}
