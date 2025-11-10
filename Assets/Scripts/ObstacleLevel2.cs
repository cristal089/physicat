using UnityEngine;
using System.Collections;
using System.Threading.Tasks;


public class ObstacleLevel2 : MonoBehaviour
{
    [SerializeField] GameObject painel; 
 
    Rigidbody2D _rb;
    Collider2D _collider2D;

    [SerializeField] float aceleration;


    bool _hasCollided = false;

    [SerializeField] float baseSpeed;
    float _currentSpeed;

    [SerializeField] LayerMask groundLayer;

    [SerializeField] LayerMask playerLayer;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider2D = GetComponentInChildren<Collider2D>();

        _currentSpeed = baseSpeed;


         if (painel != null)
             painel.SetActive(false);
    }

    void Update()
    {
        MoveObstacle();
    }

    void MoveObstacle()
    {
        _rb.linearVelocity = new Vector2(-_currentSpeed, 0f);
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (_hasCollided) return; // evita colisões duplas

        // Checa se colidiu com o jogador
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _hasCollided = true;


            if (painel != null)
            {
                painel.SetActive(true);

            }

            Time.timeScale = 0f;

            StartCoroutine(AccelerateObstacle());

            // _animator.enabled = true;

            // Destroy(gameObject, 0.5f);

        }
        IEnumerator AccelerateObstacle()
{
    // Acelera gradualmente até atingir a velocidade base + aceleração
    float targetSpeed = baseSpeed + aceleration;

    while (_currentSpeed < targetSpeed)
    {
        _currentSpeed += aceleration * Time.deltaTime;
        yield return null;
    }

    _currentSpeed = targetSpeed;
}

    }
    
}
