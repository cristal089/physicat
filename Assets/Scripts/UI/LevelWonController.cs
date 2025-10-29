using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelWonController : MonoBehaviour
{
    public void Continue()
    {
        SceneManager.LoadScene("LevelsMenu", LoadSceneMode.Single);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
