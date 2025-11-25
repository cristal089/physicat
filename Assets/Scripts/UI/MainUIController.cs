using UnityEngine;
using UnityEngine.SceneManagement; //Certifique esse namespace esta sendo usado

namespace UI
{
    public class MainUIController : MonoBehaviour
    {
        void Awake()
        {
            Time.timeScale = 1f;
        }

        void Start()
        {
            //apenas para testes, apagar depois
            //PlayerPrefs.DeleteAll();
            //PlayerPrefs.Save();
        }

        public void StartGame()
        {
            SceneManager.LoadScene("LevelsMenu", LoadSceneMode.Single);
        }

        public void GameCredits()
        {
            SceneManager.LoadScene("GameCredits", LoadSceneMode.Single);
        }

        public void Exit()
        {
            Application.Quit(); //so funciona em uma build fora do editor
        }
    }
}