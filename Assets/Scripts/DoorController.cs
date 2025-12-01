using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    [SerializeField] float speed = 3f;
    [SerializeField] LayerMask playerLayer;

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            LevelWon();
        }
    }

    void LevelWon()
    {
        Time.timeScale = 0f;

        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        int nextLevel = currentLevel + 1;

        UnlockNextLevel(nextLevel);
        print("level unlocked");

        SceneManager.LoadScene("LevelWon", LoadSceneMode.Single);
    }

    public void UnlockNextLevel(int nextLevelIndex)
    {
        string key = $"Level{nextLevelIndex}Unlocked";
        PlayerPrefs.SetInt(key, 1);
        PlayerPrefs.Save();
    }
}
