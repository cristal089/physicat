using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerLevel3 : MonoBehaviour
{
    Rigidbody2D _rb;
    Collider2D _collider2D;
    Animator _animator;

    [SerializeField] float baseSpeed;
    float _currentSpeed;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider2D = GetComponentInChildren<Collider2D>();

        _animator = GetComponent<Animator>();
        _animator.SetBool("jump", false);
        _animator.enabled = true;

        _currentSpeed = baseSpeed;
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        _rb.linearVelocity = new Vector2(-_currentSpeed, 0f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Planet"))
        {
            //SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
            print("game over");
        }
    }
}
