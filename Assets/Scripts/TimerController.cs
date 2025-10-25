using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;

    //controle de cor do temporizador
    [SerializeField] Color normalColor = Color.white; //cor padrao do temporizador
    [SerializeField] Color warningColor = Color.yellow; //cor abaixo dos 30 segundos
    [SerializeField] Color dangerColor = Color.red; //cor abaixo dos 15 segundos
    [SerializeField] Color flashColor; //cor quando o jogador atinge um obstaculo
    [SerializeField] float flashDuration = 0.2f; //tempo de flash do temporizador ao atingir um obstaculo

    bool isFlashing = false;

    void Update()
    {
        if (remainingTime > 0) 
        {
            if (!isFlashing && remainingTime >= 30)
                timerText.color = normalColor;
            else if (!isFlashing && remainingTime >= 15)
                timerText.color = warningColor;
            else if (!isFlashing)
                timerText.color = dangerColor;

                remainingTime -= Time.deltaTime;
        }
        else if (remainingTime <= 0)
        {
            remainingTime = 0;
            //GameOver
            SceneManager.LoadScene("GameOver");
        }
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void SubtractTime(float seconds)
    {
        remainingTime -= seconds;
        if (remainingTime < 0)
            remainingTime = 0;

        //faz o temporizador piscar de vermelho quando o jogador atinge obstaculos
        StopAllCoroutines(); //para qualquer flash anterior
        StartCoroutine(FlashRed());
    }

    IEnumerator FlashRed()
    {
        isFlashing = true;
        Color currentColor = timerText.color;
        timerText.color = flashColor;

        yield return new WaitForSeconds(flashDuration);

        timerText.color = currentColor;
        isFlashing = false;
    }
}
