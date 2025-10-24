using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UI.Image;

public class Player : MonoBehaviour
{
    [SerializeField] float jumpForce;
    [SerializeField] float speedX;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float rayLength = 0.1f;

    Rigidbody2D _rb;
    Collider2D _col;
    Animator _animator;
    bool _isGrounded;
    bool _jumpBtnPressed;
    float _xDir;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponentInChildren<Collider2D>();
        _animator = GetComponent<Animator>();

        _animator.SetBool("jump", false);
        _animator.enabled = true;
    }

    void FixedUpdate()
    {
        Move();
    }

    void Update()
    {
        Vector2 origin = (Vector2)_col.bounds.center;
        float offset = _col.bounds.extents.y + 0.05f; // um pouco abaixo do fim do Collider
        Vector2 rayOrigin = origin + Vector2.down * offset;

        _isGrounded = Physics2D.Raycast(rayOrigin, Vector2.down, rayLength, groundLayer);

        if (_jumpBtnPressed && _isGrounded)
        {
            Jump();
            _jumpBtnPressed = false;
        }

        if (_isGrounded)
        {
            _animator.SetBool("jump", false);
        }
        else
        {
            _animator.SetBool("jump", true);
        }
    }
    
    void Move()
    {
        _rb.linearVelocityX = _xDir * speedX;
    }

    void Jump()
    {
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0);
        _rb.AddForceY(jumpForce, ForceMode2D.Impulse);
    }

    void OnMove(InputValue inputValue)
    {
        _xDir = inputValue.Get<Vector2>().x;
    }

    void OnJump(InputValue inputValue)
    {
        _jumpBtnPressed = inputValue.isPressed;
    }
}
