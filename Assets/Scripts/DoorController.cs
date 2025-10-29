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
        Debug.Log("Você venceu!");
        Time.timeScale = 0f;
        SceneManager.LoadScene("LevelWon", LoadSceneMode.Single);
    }
}
