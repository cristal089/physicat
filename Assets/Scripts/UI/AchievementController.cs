using UnityEngine;
using TMPro;
using System.Collections;

public class AchievementController : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;

    [SerializeField] float fadeDuration = 0.5f;
    [SerializeField] float displayTime = 3f;

    AudioSource _audio;

    void Start()
    {
        canvasGroup.alpha = 0;
        _audio = GetComponent<AudioSource>();
    }

    public void ShowAchievement()
    {
        StopAllCoroutines();
        StartCoroutine(ShowMessageRoutine());
    }

    private IEnumerator ShowMessageRoutine()
    {
        _audio.enabled = true;

        //animacao de fade-in do popup
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            canvasGroup.alpha = t / fadeDuration;
            yield return null;
        }
        canvasGroup.alpha = 1;

        yield return new WaitForSeconds(displayTime);

        //animacao de fade-out do popup
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            canvasGroup.alpha = 1 - (t / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 0;
    }
}
