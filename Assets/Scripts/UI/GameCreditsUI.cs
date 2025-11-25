using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCreditsUI : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
