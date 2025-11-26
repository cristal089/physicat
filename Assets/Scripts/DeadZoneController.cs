using UnityEngine;
using UnityEngine.InputSystem;

public class TurboZoneController : MonoBehaviour
{
    Collider2D _collider2D;

    [SerializeField] GameObject speechBubble;

    bool speechActive = false;

    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (speechActive && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("key pressed");
            speechBubble.SetActive(false);
            speechActive = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.CompareTag("Player"))
        {
            speechBubble.SetActive(true);
            speechActive = true;
        }
    }
}
