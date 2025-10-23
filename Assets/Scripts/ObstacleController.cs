using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    Collider2D _collider2D;
    Animator _animator;

    bool _hasCollided = false;

    void Awake()
    {
        _collider2D = GetComponentInChildren<Collider2D>();
        _animator = GetComponent<Animator>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (_hasCollided) return;  //para evitar que o trigger da colisao ocorra duas vezes por engano
        _hasCollided = true;

        _animator.enabled = true;
        _collider2D.enabled = false;

        //verifica se o obstaculo colidiu com o jogador
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //subtrai 2 segundos ao colidir com o jogador
            TimerController timer = FindFirstObjectByType<TimerController>();
            if (timer != null)
                timer.SubtractTime(2f);
        }
        Destroy(gameObject, 0.2f); //apos atingir o solo ou o jogador
    }
}
