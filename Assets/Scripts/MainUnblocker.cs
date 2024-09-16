using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUnblocker : MonoBehaviour
{
    public void OpenAccessLevel(int levelIndex)
    {
        PlayerPrefs.SetInt(ConstGlobalVaults.LEVELS_PROGRESS_PREFS + levelIndex, 1);
        PlayerPrefs.Save();
    }

    public void ChangeStageLevel(int levelIndex)
    {
        PlayerPrefs.SetInt(ConstGlobalVaults.LEVEL_SAVE_PREFS, levelIndex + 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene(ConstGlobalVaults.EPIC_BACKFROP);
    }

    public void СonceptLevelsButtons()
    {
        for (var number = 0; number < ConstGlobalVaults.LEVELS_GENERAL; number++)
        {
            if (!PlayerPrefs.HasKey(ConstGlobalVaults.LEVELS_PROGRESS_PREFS + number))
                PlayerPrefs.SetInt(ConstGlobalVaults.LEVELS_PROGRESS_PREFS + number, number == 0 ? 1 : 0);
        }
        PlayerPrefs.Save();
    }

    public bool IsLevelUnlocked(int levelIndex)
    {
        return PlayerPrefs.GetInt(ConstGlobalVaults.LEVELS_PROGRESS_PREFS + levelIndex, 0) == 1;
    }
}