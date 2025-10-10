using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UI.Image;

public class Player : MonoBehaviour
{
    [SerializeField] float jumpForce;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float rayLength = 0.1f;

    Rigidbody2D _rb;
    Collider2D _col;
    Animator _animator;
    bool _isGrounded;
    bool _jumpBtnPressed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponentInChildren<Collider2D>();
        _animator = GetComponent<Animator>();

        _animator.SetBool("jump", false);
        //_animator.speed = 0.3f;
        _animator.enabled = true;
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


    // Update is called once per frame
    void Jump()
    {
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0);
        _rb.AddForceY(jumpForce, ForceMode2D.Impulse);
    }

    void OnJump(InputValue inputValue)
    {
        _jumpBtnPressed = inputValue.isPressed;
    }
}
