using System.Collections;
using TMPro;
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
    [SerializeField] GameObject dangerBubble;
    [SerializeField] TextMeshProUGUI amperemeterValue;
    Coroutine turboRoutine;

    [SerializeField] CircuitUI circuit;

    //controle de vitoria da fase
    [SerializeField] GameObject doorPrefab;
    [SerializeField] Transform doorSpawnPoint;
    [SerializeField] bool doorSpawned = false;

    //controle da conquista desbloqueada
    [SerializeField] AchievementManager achievementManager;
    [SerializeField] AchievementController achievementController;
    private bool achievementShown = false;

    //a conquista sera desbloqueada apos usados 2 turbos e o jogador vencera a fase apos usados 3 turbos corretamente
    int _countTurbo = 0;

    //soundeffects
    [SerializeField] AudioClip heyVoiceTurbo;
    [SerializeField] AudioClip heyVoiceDanger;
    [SerializeField] AudioClip normalTurboSound;
    [SerializeField] AudioClip bonusTurboSound;
    [SerializeField] AudioClip resistorExploding;

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
        }
        CheckTurboCount();
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

        if (amperemeterValue.GetParsedText() == "10")
        {
            if (_isInTurboZone)
            {
                _animator.SetBool("bonusTurbo", true);
                AudioSource.PlayClipAtPoint(bonusTurboSound, transform.position);
                _turboVelocity = bonusTurbo;
                AudioSource.PlayClipAtPoint(resistorExploding, transform.position);
                circuit.ExplodeRandomResistor();
                turboRoutine = StartCoroutine(StopTurboAnimation(1.5f));
                _countTurbo++;
            }
            else
            {
                _animator.SetBool("normalTurbo", true);
                AudioSource.PlayClipAtPoint(normalTurboSound, transform.position);
                _turboVelocity = normalTurbo;
                AudioSource.PlayClipAtPoint(resistorExploding, transform.position);
                circuit.ExplodeRandomResistor();
                turboRoutine = StartCoroutine(StopTurboAnimation(0.5f));
            }
        }
        else
        {
            if (dangerBubble != null)
                StartCoroutine(ShowSpeechBubble(0.7f));
            AudioSource.PlayClipAtPoint(resistorExploding, transform.position);
            circuit.ExplodeRandomResistor();
        }
    }

    IEnumerator StopTurboAnimation(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        _animator.SetBool("bonusTurbo", false);
        _animator.SetBool("normalTurbo", false);
    }

    IEnumerator ShowSpeechBubble(float duration)
    {
        AudioSource.PlayClipAtPoint(heyVoiceDanger, transform.position);
        dangerBubble.SetActive(true);
        yield return new WaitForSeconds(duration);
        dangerBubble.SetActive(false);
    }

    void CheckTurboCount()
    {
        if(!achievementShown && _countTurbo >= 2 && !achievementManager.IsUnlocked("2turbo"))
        {
            achievementManager.UnlockAchievement("2turbo");
            achievementShown = true;
        }

        if(!doorSpawned && _countTurbo >= 3)
        {
            ObstacleSpawner spawner = FindFirstObjectByType<ObstacleSpawner>();

            if (spawner != null)
                spawner.StopAllCoroutines();
            SpawnDoor();
        }
    }

    void SpawnDoor()
    {
        Instantiate(doorPrefab, doorSpawnPoint.position, Quaternion.identity);
        doorSpawned = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TurboZone"))
        {
            _isInTurboZone = true;
            if (speechBubble != null)
            {
                speechBubble.SetActive(true);
                AudioSource.PlayClipAtPoint(heyVoiceTurbo, transform.position);
            }
        }

        if (collision.CompareTag("Planet"))
        {
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
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
