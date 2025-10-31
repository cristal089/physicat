using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    private AudioSource audioSource;

    public AudioClip menuMusic;
    public AudioClip notebookReadingMusic;
    public AudioClip level1Music;
    public AudioClip level2Music;
    public AudioClip levelWonMusic;

    private void Awake()
    {
        //garante que so existira um music manager ativo
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();

        if (!audioSource.isPlaying && menuMusic != null)
        {
            audioSource.clip = menuMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //troca de musica automaticamente entre as scenes
        if (scene.name == "MainMenu" || scene.name == "LevelsMenu" || scene.name == "GameOver")
        {
            PlayMusic(menuMusic);
        }
        else if (scene.name == "Level1Explanation")
        {
            PlayMusic(notebookReadingMusic);
        }
        else if (scene.name == "Level1Dialogue" || scene.name == "GameLevel1")
        {
            PlayMusic(level1Music, 0.5f,0.5f);
        }
        else if (scene.name == "Level2Dialogue" || scene.name == "GameLevel2")
        {
            PlayMusic(level2Music, 0.5f, 0.5f);
        }
        else if (scene.name == "LevelWon")
        {
            PlayMusic(levelWonMusic);
        }
    }

    public void PlayMusic(AudioClip newClip, float fadeDuration = 0.5f, float volume = 1f)
    {
        //caso a mesma musica ja esteja tocando nao faz nada
        if (audioSource.clip == newClip && audioSource.isPlaying)
            return;

        StopAllCoroutines();
        StartCoroutine(FadeToNewTrack(newClip, fadeDuration, volume));
    }

    private IEnumerator FadeToNewTrack(AudioClip newClip, float duration, float volume)
    {
        float startVolume = audioSource.volume;

        //fade-out da musica atual
        for (float t = 0; t < duration; t += Time.unscaledDeltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t / duration);
            yield return null;
        }

        audioSource.clip = newClip;
        audioSource.Play();

        //fade-in da proxima musica
        for (float t = 0; t < duration; t += Time.unscaledDeltaTime)
        {
            audioSource.volume = Mathf.Lerp(0f, volume, t / duration);
            yield return null;
        }
        audioSource.volume = volume;
    }
}
