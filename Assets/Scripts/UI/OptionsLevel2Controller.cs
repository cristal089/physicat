using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace UI
{
    public class OptionsLevel2Controller : MonoBehaviour
    {
        public GameObject panel;
        public Image panelImage;
        public float tempoMostrar = 1.5f;

        public void WrongOption()
        {
            // Pausa o jogo
            Time.timeScale = 0f;

            // Ativa o painel e deixa vermelho
            if (panel != null)
                panel.SetActive(true);

            if (panelImage != null)
                panelImage.color = Color.red;

            // Reduz o tempo
            TimerController timer = FindFirstObjectByType<TimerController>();
            if (timer != null)
                timer.SubtractTime(5f);
        }

        public void RightOption()
        {
            StartCoroutine(AcertoPainel());
        }

        private IEnumerator AcertoPainel()
        {
            // Painel verde
            if (panel != null)
                panel.SetActive(true);

            if (panelImage != null)
                panelImage.color = Color.green;

            // Pausa o jogo
            Time.timeScale = 0f;

            yield return new WaitForSecondsRealtime(tempoMostrar);

            // Fecha painel
            if (panel != null)
                panel.SetActive(false);

            // Retoma o jogo
            Time.timeScale = 1f;
        }
    }
}
