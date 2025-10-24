using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float jumpHeight = 2f; //o quao alto sera o pulo
    [SerializeField] float jumpDuration = 0.4f; //quanto tempo o pulo gasta (para cima + para baixo)
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float rayLength = 0.1f;

    Rigidbody2D _rb;
    Collider2D _col;
    Animator _animator;

    bool _isGrounded;
    bool _jumpBtnPressed;
    bool _isJumping;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponentInChildren<Collider2D>();
        _animator = GetComponent<Animator>();

        _animator.SetBool("jump", false);
        _animator.enabled = true;
    }

    void Update()
    {
        //define uma origem para detectar o chao usando Raycast e o collider da personagem
        Vector2 origin = (Vector2)_col.bounds.center;
        float offset = _col.bounds.extents.y + 0.05f; //um pouco abaixo do fim do collider
        Vector2 rayOrigin = origin + Vector2.down * offset;

        //detecta se a personagem esta no chao para possibilitar o pulo
        _isGrounded = Physics2D.Raycast(rayOrigin, Vector2.down, rayLength, groundLayer);

        if (_jumpBtnPressed && _isGrounded && !_isJumping)
        {
            StartCoroutine(Jump());
            _jumpBtnPressed = false;
        }

        _animator.SetBool("jump", !_isGrounded);
    }

    IEnumerator Jump()
    {
        _isJumping = true;

        //float halfDuration = jumpDuration / 2f;
        float elapsed = 0f;
        float startY = transform.position.y; //salva a posicao em que a personagem estava antes de pular

        //personagem pula para cima
        //usa movimento parabolico para ficar mais suave
        while (elapsed < jumpDuration)
        {
            float t = elapsed / jumpDuration;
            float heightFactor = 4 * t * (1 - t); //parabola 0->1->0
            float newY = startY + heightFactor * jumpHeight;

            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        //move a personagem de volta para o chao para evitar erros
        transform.position = new Vector3(transform.position.x, startY, transform.position.z);

        _isJumping = false;
    }

    void OnJump(InputValue inputValue)
    {
        _jumpBtnPressed = inputValue.isPressed;
    }
}
