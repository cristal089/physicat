using UnityEngine;
using UnityEngine.UI;

public class ButtonUnlocker : MonoBehaviour
{
    [SerializeField] string requiredAchievementId;
    [SerializeField] Button button;
    [SerializeField] GameObject lockIcon;

    void Start()
    {
        if (button == null)
            button = GetComponent<Button>();

        //carrega as conquistas salvas em PlayerPrefs
        bool unlocked = PlayerPrefs.GetInt(requiredAchievementId, 0) == 1;

        button.interactable = unlocked;

        if (lockIcon != null)
            lockIcon.SetActive(!unlocked);
    }
}
