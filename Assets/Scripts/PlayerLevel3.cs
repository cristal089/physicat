using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerLevel3 : MonoBehaviour
{
    Rigidbody2D _rb;
    Collider2D _collider2D;
    Animator _animator;

    [SerializeField] float baseSpeed;
    float _currentSpeed;

    [SerializeField] float normalTurbo = 6f;
    //o turbo sera maior se o jogador ativa-lo quando a personagem indicar
    [SerializeField] float bonusTurbo = 12f;
    [SerializeField] float turboDecay = 10f;

    float _turboVelocity = 0f;
    bool _isInTurboZone = false;

    [SerializeField] GameObject speechBubble;
    Coroutine turboRoutine;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider2D = GetComponentInChildren<Collider2D>();

        _animator = GetComponent<Animator>();
        _animator.SetBool("normalTurbo", false);
        _animator.SetBool("bonusTurbo", false);
        _animator.enabled = true;

        _currentSpeed = baseSpeed;
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Turbo();

            //if (speechActive)
            //{
            //    speechBubble.SetActive(false);
            //    speechActive = false;
            //}
        }
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        //movimento base negativo - da a impressao que o planeta esta se aproximando
        float baseMove = -_currentSpeed;

        //movimento considerando o turbo usado pelo jogador
        float finalX = baseMove + _turboVelocity;

        _rb.linearVelocity = new Vector2(finalX, 0f);

        //reduz o turbo com o tempo
        _turboVelocity = Mathf.MoveTowards(_turboVelocity, 0f, turboDecay * Time.fixedDeltaTime);
    }

    public void Turbo()
    {
        if (turboRoutine != null)
            StopCoroutine(turboRoutine);

        _animator.SetBool("bonusTurbo", false);
        _animator.SetBool("normalTurbo", false);

        if (_isInTurboZone)
        {
            _animator.SetBool("bonusTurbo", true);
            _turboVelocity = bonusTurbo;
            turboRoutine = StartCoroutine(StopTurboAnimation(1.5f));
        }
        else
        {
            _animator.SetBool("normalTurbo", true);
            _turboVelocity = normalTurbo;
            turboRoutine = StartCoroutine(StopTurboAnimation(0.5f));
        }
    }

    IEnumerator StopTurboAnimation(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        _animator.SetBool("bonusTurbo", false);
        _animator.SetBool("normalTurbo", false);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TurboZone"))
        {
            _isInTurboZone = true;
            if (speechBubble != null)
                speechBubble.SetActive(true);
        }

        if (collision.CompareTag("Planet"))
        {
            //SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
            print("game over");
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("TurboZone"))
        {
            _isInTurboZone = false;
            if (speechBubble != null)
                speechBubble.SetActive(false);
        }
    }
}
