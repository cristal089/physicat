using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelWonController : MonoBehaviour
{
    void Awake()
    {
        Time.timeScale = 1f;
    }

    public void Continue()
    {
        SceneManager.LoadScene("LevelsMenu", LoadSceneMode.Single);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
