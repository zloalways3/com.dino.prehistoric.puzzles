using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLaunch : MonoBehaviour
{
    public void LoadingGlobalSceneMain()
    {
        SceneManager.LoadScene(ConstGlobalVaults.LOADING_HAVEN);
    }
    public void LoadingPlaySceneGames()
    {
        SceneManager.LoadScene(ConstGlobalVaults.EPIC_BACKFROP);
    }
        
}