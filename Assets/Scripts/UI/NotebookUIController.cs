using UnityEngine;
using UnityEngine.SceneManagement;

public class NotebookUIController : MonoBehaviour
{
    [SerializeField] GameObject buttonNext;
    [SerializeField] GameObject buttonPrevious;
    [SerializeField] GameObject page1;
    [SerializeField] GameObject page2;
    public void Next()
    {
        buttonNext.SetActive(false);
        page1.SetActive(false);

        buttonPrevious.SetActive(true);
        page2.SetActive(true);
    }

    public void Previous()
    {
        buttonNext.SetActive(true);
        page1.SetActive(true);

        buttonPrevious.SetActive(false);
        page2.SetActive(false);
    }

    public void Exit()
    {
        SceneManager.LoadScene("LevelsMenu");
    }
}
