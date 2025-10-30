using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

namespace UI
{
    public class OptionsLevel2Controller : MonoBehaviour
    {
        public GameObject panel; // arraste o painel aqui
        public Image panelImage; // arraste o componente Image do painel
        public float tempoMostrar = 1.5f; // tempo em segundos antes de fechar

        public void WrongOption()
        {
            StartCoroutine(MostrarPainel(Color.red)); // painel vermelho
            TimerController timer = FindFirstObjectByType<TimerController>();
            if (timer != null)
                timer.SubtractTime(2f);
        }

        public void RightOption()
        {
            StartCoroutine(MostrarPainel(Color.green)); // painel verde
        }

        private IEnumerator MostrarPainel(Color cor)
        {
            // pausa o jogo
            Time.timeScale = 0f;

            // ativa o painel e muda a cor
            if (panel != null)
            {
                panel.SetActive(true);
                if (panelImage != null)
                    panelImage.color = cor;
            }

            // espera um tempo "em tempo real" (ignora o Time.timeScale)
            yield return new WaitForSecondsRealtime(tempoMostrar);

            // desativa o painel
            if (panel != null)
                panel.SetActive(false);

            // volta o tempo do jogo ao normal
            Time.timeScale = 1f;
        }
    }
}
