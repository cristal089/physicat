using UnityEngine;
using UnityEngine.UI;

public class LevelButtonUnlock : MonoBehaviour
{
    [SerializeField] int levelNumber = 1;
    [SerializeField] Button button;
    [SerializeField] GameObject _btnImage;

    void Start()
    {
        if (button == null)
            button = GetComponent<Button>();

        if (levelNumber == 1)
        {
            //a fase 1 e desbloqueada por padrao
            UnlockLevelButton();
            return;
        }

        //confere as PlayerPrefs
        string key = $"Level{levelNumber}Unlocked";
        bool isUnlocked = PlayerPrefs.GetInt(key, 0) == 1;

        if (isUnlocked)
            UnlockLevelButton();
        else
            LockLevelButton();
    }

    void UnlockLevelButton()
    {
        button.interactable = true;
        _btnImage.SetActive(false);
    }

    void LockLevelButton()
    {
        button.interactable = false;
        _btnImage.SetActive(true);
    }
}
