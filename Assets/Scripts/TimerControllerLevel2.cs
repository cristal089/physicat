using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class TimerControllerLevel2 : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;

    //controle de cor do temporizador
    Color normalColor = Color.white; //cor padrao do temporizador
     Color warningColor = Color.yellow; //cor abaixo dos 30 segundos
     Color dangerColor = Color.red; //cor abaixo dos 15 segundos
     Color flashColor; //cor quando o jogador atinge um obstaculo
     float flashDuration = 0.2f; //tempo de flash do temporizador ao atingir um obstaculo
    bool isFlashing = false;

    //controle de derrota ou vitoria da fase
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

        //para desbloquear a conquista o jogador deve sobreviver por 20 segundos no total
        if (!achievementShown && _timeSurvived >= 20f && !achievementManager.IsUnlocked("20s"))
        {
            achievementManager.UnlockAchievement("20s");
            achievementShown = true;
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


}
