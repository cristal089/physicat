using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float jumpForce = 10f;
    [SerializeField] int maxJumps = 2;

    Rigidbody2D _rb;
    Collider2D _col;
    Animator _animator;

    int _jumpsLeft;
    bool _jumpBtnPressed;
    bool _isGrounded;
    bool _wasGroundedLastFrame;


    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponentInChildren<Collider2D>();
        _animator = GetComponent<Animator>();

        _animator.SetBool("jump", false);
        _animator.enabled = true;

        _jumpsLeft = maxJumps;
    }

    void Update()
    {
        if (_isGrounded && !_wasGroundedLastFrame)
        {
            //reseta o numero de pulos ao voltar para o chao
            _jumpsLeft = maxJumps;
        }
        _animator.SetBool("jump", !_isGrounded);

        if (_jumpBtnPressed && _jumpsLeft > 0)
        {
            DoJump();
            _jumpBtnPressed = false;
        }
        _wasGroundedLastFrame = _isGrounded;
    }

    void DoJump()
    {
        //reseta a velocidade vertical
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0f);

        //aplica o impulso para pular
        _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        _jumpsLeft--;
    }

    void OnJump(InputValue inputValue)
    {
        _jumpBtnPressed = inputValue.isPressed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            _isGrounded = true;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            _isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            _isGrounded = false;
        }
    }
}
