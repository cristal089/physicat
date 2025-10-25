using UnityEngine;
using System.Collections;

public class ObstacleController : MonoBehaviour
{
    Rigidbody2D _rb;
    Collider2D _collider2D;
    Animator _animator;

    bool _hasCollided = false;

    [SerializeField] float baseSpeed;
    float _currentSpeed;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] float rayLength = 1f; //para detectar o chao

    [SerializeField] LayerMask playerLayer;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider2D = GetComponentInChildren<Collider2D>();
        _animator = GetComponent<Animator>();

        _currentSpeed = baseSpeed;

        _animator.enabled = true;
    }

    void Update()
    {
        MoveObstacle();
        DetectGround();
    }

    void MoveObstacle()
    {
        //Debug.Log($"{gameObject.name}: {_currentSpeed}");
        _rb.linearVelocity = new Vector2(-_currentSpeed, 0f);
    }
    void DetectGround()
    {
        //cria um "raycast" para baixo para detectar o chao
        Vector2 origin = (Vector2)transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, rayLength, groundLayer);

        if (hit.collider != null)
        {
            string tagName = hit.collider.tag;

            //troca a velocidade dependendo do tipo de chao
            switch (tagName)
            {
                case "MudGround":
                    _currentSpeed = baseSpeed * 0.6f;
                    break;
                case "IceGround":
                    _currentSpeed = baseSpeed * 1.5f;
                    break;
                case "NormalGround":
                    _currentSpeed = baseSpeed * 1f;
                    break;
            }
        }
        //Debug.DrawRay(origin, Vector2.down * rayLength, Color.red);
    }    

    void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.Log($"{gameObject.name} triggered by {collider.name} with tag {collider.tag}");
        if (_hasCollided) return; //para evitar que o trigger da colisao ocorra duas vezes

        //checa se colidiu com o jogador
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (_hasCollided) return;
            _hasCollided = true;

            //subtrai 2 segundos ao colidir com o jogador
            TimerController timer = FindFirstObjectByType<TimerController>();
            if (timer != null)
                timer.SubtractTime(2f);

            _animator.enabled = true;
            _collider2D.enabled = false;

            Destroy(gameObject, 0.1f);
        }
    }
}
