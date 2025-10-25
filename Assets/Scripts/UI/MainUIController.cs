using UnityEngine;
using UnityEngine.SceneManagement; //Certifique esse namespace esta sendo usado

namespace UI
{
    public class MainUIController : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene("LevelsMenu");
        }

        public void Exit()
        {
            Application.Quit();//Só funciona em uma build fora do editor.
        }
    }
}