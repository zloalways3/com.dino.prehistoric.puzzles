using UnityEngine;

public class ExitElysium : MonoBehaviour
{ 
    public void ExitingUniverse()
    {
#if UNITY_EDITOR
        
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}