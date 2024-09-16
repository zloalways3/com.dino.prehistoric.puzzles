using UnityEngine;

public class SpriteWinGameMaestro : MonoBehaviour
{ 
    private int _operGameLevel;

    private void Awake()
    {
        ReceivingPlayerPrefs();
    }

    private void ReceivingPlayerPrefs()
    {
        _operGameLevel = PlayerPrefs.GetInt(ConstGlobalVaults.LEVEL_SAVE_PREFS, 0);
    }

    

    public void VictoryGamesLevels()
    {
        PlayerPrefs.SetInt(ConstGlobalVaults.LEVEL_SAVE_PREFS, _operGameLevel + 1);
        PlayerPrefs.Save();
        
        var mainUnblocker = FindObjectOfType<MainUnblocker>();
        mainUnblocker.OpenAccessLevel(_operGameLevel);
    }

    
}