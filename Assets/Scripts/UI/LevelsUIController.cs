using UnityEngine;
using UnityEngine.SceneManagement;

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
        public void OpenNotebook2()
        {
            SceneManager.LoadScene("Level2Explanation", LoadSceneMode.Single);
        }

        public void Level2()
        {
            SceneManager.LoadScene("Level2Dialogue", LoadSceneMode.Single);
        }

        public void OpenNotebook3()
        {
            SceneManager.LoadScene("Level2Explanation", LoadSceneMode.Single);
        }

        public void Level3()
        {
            SceneManager.LoadScene("GameLevel3", LoadSceneMode.Single);
        }

        public void Back()
        {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
    }
}
