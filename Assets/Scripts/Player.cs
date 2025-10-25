using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float jumpForce = 10f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float rayLength = 0.1f;
    [SerializeField] int maxJumps = 1;

    Rigidbody2D _rb;
    Collider2D _col;
    Animator _animator;

    int _jumpsLeft;
    bool _jumpBtnPressed;


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
        bool isGrounded = CheckGrounded();

        if (isGrounded)
        {
            //reseta o numero de pulos ao voltar para o chao
            _jumpsLeft = maxJumps;
            _animator.SetBool("jump", false);
        }
        else
        {
            _animator.SetBool("jump", true);
        }

        if (_jumpBtnPressed && _jumpsLeft > 0)
        {
            DoJump();
            _jumpBtnPressed = false;
        }
    }

    bool CheckGrounded()
    {
        //define uma origem para detectar o chao usando Raycast e o collider da personagem
        Vector2 origin = (Vector2)_col.bounds.center;

        //um pouco abaixo do fim do collider
        float offset = _col.bounds.extents.y + 0.05f;

        Vector2 rayOrigin = origin + Vector2.down * offset;

        //retorna se a personagem esta no chao para possibilitar o pulo
        return Physics2D.Raycast(rayOrigin, Vector2.down, rayLength, groundLayer);
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
}
