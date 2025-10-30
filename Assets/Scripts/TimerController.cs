using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class TimerController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;

    //controle de cor do temporizador
    [SerializeField] Color normalColor = Color.white; //cor padrao do temporizador
    [SerializeField] Color warningColor = Color.yellow; //cor abaixo dos 30 segundos
    [SerializeField] Color dangerColor = Color.red; //cor abaixo dos 15 segundos
    [SerializeField] Color flashColor; //cor quando o jogador atinge um obstaculo
    [SerializeField] float flashDuration = 0.2f; //tempo de flash do temporizador ao atingir um obstaculo
    bool isFlashing = false;

    //controle de derrota ou vitoria da fase
    [SerializeField] GameObject doorPrefab;
    [SerializeField] Transform doorSpawnPoint;
    [SerializeField] bool doorSpawned = false;
    [SerializeField] float totalTime = 60f;
    float _timeRemaining;
    float _timeSurvived;

    //controle da conquista desbloqueada
    [SerializeField] AchievementManager achievementManager;
    [SerializeField] AchievementController achievementController;
    private bool achievementShown = false;

    void Awake()
    {
        Time.timeScale = 1f;
    }

    void Start()
    {
        _timeRemaining = totalTime;
        _timeSurvived = 0f;
    }

    void Update()
    {
        float delta = Time.deltaTime;

        //registra quanto tempo o jogador sobreviveu na fase
        _timeSurvived += delta;

        if (_timeRemaining > 0) 
        {
            if (!isFlashing && _timeRemaining >= 30)
                timerText.color = normalColor;
            else if (!isFlashing && _timeRemaining >= 15)
                timerText.color = warningColor;
            else if (!isFlashing)
                timerText.color = dangerColor;

            _timeRemaining -= delta;
        }

        //para desbloquear a conquista o jogador deve sobreviver por 30 segundos no total
        if (!achievementShown && _timeSurvived >= 30f && !achievementManager.IsUnlocked("30s"))
        {
            achievementManager.UnlockAchievement("30s");
            achievementShown = true;
        }

        //para vencer o jogador deve sobreviver por 45 segundos no total
        if (!doorSpawned && _timeSurvived >= 45f)
        {
            ObstacleSpawner spawner = FindFirstObjectByType<ObstacleSpawner>();
            if (spawner != null)
                spawner.StopAllCoroutines();
            SpawnDoor();
        }
        
        //o jogador perde se nao sobreviver por 50 segundos e seu tempo acabar
        if (_timeRemaining <= 0)
        {
            _timeRemaining = 0;
            //game over
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
        }
        int minutes = Mathf.FloorToInt(_timeRemaining / 60);
        int seconds = Mathf.FloorToInt(_timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void SubtractTime(float seconds)
    {
        _timeRemaining -= seconds;
        if (_timeRemaining < 0)
            _timeRemaining = 0;

        //faz o temporizador piscar de vermelho quando o jogador atinge obstaculos
        StopAllCoroutines(); //faz parar qualquer flash anterior
        StartCoroutine(FlashRed());
    }

    IEnumerator FlashRed()
    {
        isFlashing = true;
        Color currentColor = timerText.color;
        timerText.color = flashColor;

        yield return new WaitForSecondsRealtime(flashDuration);

        timerText.color = currentColor;
        isFlashing = false;
    }

    void SpawnDoor()
    {
        Instantiate(doorPrefab, doorSpawnPoint.position, Quaternion.identity);
        doorSpawned = true;
    }
}
