using UnityEngine;
using UnityEngine.SceneManagement; //Certifique esse namespace esta sendo usado

namespace UI
{
    public class LevelsUIController : MonoBehaviour
    {
        public void Level1()
        {
            SceneManager.LoadScene("Level1Dialogue");
        }

        public void Level2()
        {
            SceneManager.LoadScene("Level2Dialogue");
        }
        
        public void Back()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
