using UnityEngine;
using UnityEngine.SceneManagement; //Certifique esse namespace esta sendo usado

namespace UI
{
    public class LevelsUIController : MonoBehaviour
    {
        public void Level1()
        {
            SceneManager.LoadScene("GameScene");
        }
        
        public void Back()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
