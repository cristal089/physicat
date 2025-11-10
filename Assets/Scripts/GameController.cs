using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    //para fechar o jogo ao apertar esc em qualquer tela
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Application.Quit();
        }
    }
}
