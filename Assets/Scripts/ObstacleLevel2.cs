using UnityEngine;
using System.Collections;

public class ObstacleLevel2 : MonoBehaviour
{
    [SerializeField] GameObject painel; 
    public Sprite novoSprite;               // sprite que aparecerá após a colisão
    private SpriteRenderer spriteRenderer;
    Rigidbody2D _rb;
    Collider2D _collider2D;

    bool _hasCollided = false;

    [SerializeField] float baseSpeed;
    float _currentSpeed;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] float rayLength = 1f; // para detectar o chão

    [SerializeField] LayerMask playerLayer;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider2D = GetComponentInChildren<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        _currentSpeed = baseSpeed;


         if (painel != null)
             painel.SetActive(false);
    }

    void Update()
    {
        MoveObstacle();
        DetectGround();
    }

    void MoveObstacle()
    {
        _rb.linearVelocity = new Vector2(-_currentSpeed, 0f);
    }

    void DetectGround()
    {
        // Cria um "raycast" para baixo para detectar o chão
        Vector2 origin = (Vector2)transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, rayLength, groundLayer);

        if (hit.collider != null)
        {
            string tagName = hit.collider.tag;

            // Troca a velocidade dependendo do tipo de chão
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
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (_hasCollided) return; // evita colisões duplas

        // Checa se colidiu com o jogador
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _hasCollided = true;

                
             if (painel != null){
                print("painel abre");
                painel.SetActive(true);

                }

            Time.timeScale = 0f;
            
           

            // if (spriteRenderer != null && novoSprite != null)
            // {
            //     print("sprite novo");
            //     spriteRenderer.sprite = novoSprite;
            // }

            //  Destroy(gameObject, 0.1f);
        }
    }
}
