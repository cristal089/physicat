using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class AchievementManager : MonoBehaviour
{
    [SerializeField] AchievementController achievementController; //pop-up da conquista

    [SerializeField] List<Achievement> achievements = new List<Achievement>();

    //registra as conquistas
    void Start()
    {
        if (achievements.Count == 0)
        {
            achievements.Add(new Achievement("20s"));

            achievements.Add(new Achievement("30s"));
        }
        LoadAchievements();
    }

    public void UnlockAchievement(string id)
    {
        Achievement a = achievements.Find(x => x.id == id);

        if (a != null && !a.unlocked)
        {
            a.unlocked = true;
            SaveAchievements();

            //mostra o pop-up
            achievementController.ShowAchievement();
        }
    }

    public bool IsUnlocked(string id)
    {
        Achievement a = achievements.Find(x => x.id == id);
        return a != null && a.unlocked;
    }

    //"save and load" das conquistas ja desbloqueadas
    void SaveAchievements()
    {
        foreach (var a in achievements)
        {
            PlayerPrefs.SetInt(a.id, a.unlocked ? 1 : 0);
        }
        PlayerPrefs.Save();
    }

    void LoadAchievements()
    {
        foreach (var a in achievements)
        {
            a.unlocked = PlayerPrefs.GetInt(a.id, 0) == 1;
        }
    }
}
