using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicStudio : MonoBehaviour
{
    [SerializeField] private AudioMixer _musicStudio;
    [SerializeField] private Button _soundOrhestorButton;
    [SerializeField] private Sprite _soundOrhestorOnSprite;
    [SerializeField] private Sprite _soundOrhestorOffSprite;
    [SerializeField] private Button _musicOrhestorButton; 
    [SerializeField] private Sprite _musicOrhestorOnSprite;
    [SerializeField] private Sprite _musicOrhestorOffSprite;

    private bool isSoundOrhestorOn;
    private bool isMusicOrhestorOn;

    private string _soundsIncluded = "soundsIncluded";
    private string _musicIncluded = "musicIncluded";

    private void Start()
    {
        isSoundOrhestorOn = PlayerPrefs.GetInt(_soundsIncluded, 1) == 1;
        isMusicOrhestorOn = PlayerPrefs.GetInt(_musicIncluded, 1) == 1;
        
        SetAudioState();
        
        UpdateButtonVisual();
        _soundOrhestorButton.onClick.AddListener(ChangeSoundOrhestor);
        _musicOrhestorButton.onClick.AddListener(ChangeMusicOrhestor);
    }

    private void ChangeSoundOrhestor()
    {
        isSoundOrhestorOn = !isSoundOrhestorOn;
        SetAudioState();
        UpdateButtonVisual();
        SaveAudioSettings();
    }
    
    private void ChangeMusicOrhestor()
    {
        isMusicOrhestorOn = !isMusicOrhestorOn;
        SetAudioState();
        UpdateButtonVisual();
        SaveAudioSettings();
    }

    private void SetAudioState()
    {
        _musicStudio.SetFloat(ConstGlobalVaults.VIBE_METER, isSoundOrhestorOn ? 0f : -80f);
        _musicStudio.SetFloat(ConstGlobalVaults.NOISE_GAUGE, isMusicOrhestorOn ? 0f : -80f);
    }

    private void UpdateButtonVisual()
    {
        _musicOrhestorButton.image.sprite = isMusicOrhestorOn ? _musicOrhestorOnSprite : _musicOrhestorOffSprite;
        _soundOrhestorButton.image.sprite = isSoundOrhestorOn ? _soundOrhestorOnSprite : _soundOrhestorOffSprite;
    }

    private void SaveAudioSettings()
    {
        PlayerPrefs.SetInt(_soundsIncluded, isSoundOrhestorOn ? 1 : 0);
        PlayerPrefs.SetInt(_musicIncluded, isMusicOrhestorOn ? 1 : 0);
        PlayerPrefs.Save();
    }
}