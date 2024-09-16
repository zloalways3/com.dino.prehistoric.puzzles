using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class SceneLoadingGlobalScene : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _loadingTextLoadingScene;
    [SerializeField] private Image _progressImage;
    private float _cuteTimer;
    private int _cuteCount; 
    private const float _dotInterval = 0.5f;
    private string _textLoadingProgress = "Loading";

    private void Start()
    {
        StartCoroutine(LoadGlobalScenePixel(ConstGlobalVaults.ADVENTURE_BOARD));
    }

    private IEnumerator LoadGlobalScenePixel(string nameScene)
    {
        var asyncProgress = SceneManager.LoadSceneAsync(nameScene);

        asyncProgress.allowSceneActivation = false;

        while (!asyncProgress.isDone)
        {
            FixUpdateLoadTextScene();
            
            if (_progressImage != null)
            {
                _progressImage.fillAmount = asyncProgress.progress / 0.9f;
            }
                
            if (asyncProgress.progress >= 0.9f)
            {
                asyncProgress.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    private void FixUpdateLoadTextScene()
    {
        _cuteTimer += Time.deltaTime;

        if (_cuteTimer >= _dotInterval)
        {
            _cuteCount = (_cuteCount + 1) % 4;
            var cute = new string('.', _cuteCount);
            _loadingTextLoadingScene.text = _textLoadingProgress + cute;
            _cuteTimer = 0f;
        }
    }
}