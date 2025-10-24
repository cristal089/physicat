using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    Rigidbody2D _rb;
    Collider2D _collider2D;
    Animator _animator;

    bool _hasCollided = false;

    [SerializeField] float baseSpeed;
    float _currentSpeed;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _currentSpeed = baseSpeed;

        _collider2D = GetComponentInChildren<Collider2D>();
        _animator = GetComponent<Animator>();
        _animator.enabled = true;
    }

    void FixedUpdate()
    {
        _rb.linearVelocity = new Vector2(-_currentSpeed, _rb.linearVelocity.y);
    }

    void AdjustSpeed(float multiplier)
    {
        _currentSpeed = baseSpeed * multiplier;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (_hasCollided) return; //para evitar que o trigger da colisao ocorra duas vezes

        if (collision.gameObject.CompareTag("MudGround"))
            GetComponent<ObstacleController>().AdjustSpeed(0.6f);
        else if (collision.gameObject.CompareTag("IceGround"))
            GetComponent<ObstacleController>().AdjustSpeed(1.5f);
        else
            GetComponent<ObstacleController>().AdjustSpeed(1f);

        //verifica se o obstaculo colidiu com o jogador
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //subtrai 2 segundos ao colidir com o jogador
            TimerController timer = FindFirstObjectByType<TimerController>();
            if (timer != null)
                timer.SubtractTime(2f);
            _hasCollided = true;
            _animator.enabled = true;
            _collider2D.enabled = false;
            Destroy(gameObject, 0.2f); //apos atingir o jogador
        }
    }
}
