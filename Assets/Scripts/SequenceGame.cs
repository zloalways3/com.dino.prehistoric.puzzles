using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SequenceGame : MonoBehaviour
{
    [SerializeField] private GameObject[] spritePrefabs;
    [SerializeField] private Button mixButton;
    [SerializeField] private Button readyButton;
    [SerializeField] private Transform gridParent;
    [SerializeField] private GameObject WinPanel;
    [SerializeField] private GameObject LousePanel;
    
    private readonly List<GameObject> _spawnedSprites = new List<GameObject>();
    private readonly List<int> _correctSequence = new List<int>();
    private readonly List<int> _currentSequence = new List<int>();
    private bool _isCanMove;

    private void Start()
    {
        SetupLevel(PlayerPrefs.GetInt(ConstGlobalVaults.LEVEL_SAVE_PREFS, 0));
        mixButton.onClick.AddListener(MixSprites);
        readyButton.onClick.AddListener(CheckSequence);
        readyButton.gameObject.SetActive(false);
    }

    private void SetupLevel(int level)
    {
        foreach (var sprite in _spawnedSprites)
        {
            Destroy(sprite);
        }
        _spawnedSprites.Clear();
        _correctSequence.Clear();
        _currentSequence.Clear();

        int linerow, columnСols;
        
        switch (level)
        {
            case 1: linerow = 1; columnСols = 3; break;
            case 2: linerow = 2; columnСols = 2; break;
            case 3: linerow = 2; columnСols = 3; break;
            case 4: linerow = 2; columnСols = 4; break;
            case 5: linerow = 3; columnСols = 3; break;
            default:return;
        }
        
        const float cellWidth = 1f;
        const float cellHeight = 1f;
        
        var startX = -columnСols * cellWidth / 2 + cellWidth / 2;
        var startY = linerow * cellHeight / 2 - cellHeight / 2;

        for (var numberIndex = 0; numberIndex < linerow * columnСols; numberIndex++)
        {
            var spriteIndex = numberIndex % spritePrefabs.Length;
            var spriteObject = Instantiate(spritePrefabs[spriteIndex], gridParent);
            spriteObject.transform.localScale = new Vector3(0.00571132265f,0.0207035504f,0.00374533795f);
            
            var transformSprite = spriteObject.GetComponent<RectTransform>();
            var row = numberIndex / columnСols;
            var col = numberIndex % columnСols;

            var xPos = startX + col * cellWidth;
            var yPos = startY - row * cellHeight;
            
            transformSprite.anchoredPosition = new Vector2(xPos, yPos);

            var button = spriteObject.GetComponent<Button>();
            button.onClick.AddListener(() => SwapSprite(spriteObject));
            _spawnedSprites.Add(spriteObject);
            _correctSequence.Add(spriteIndex);
            _currentSequence.Add(spriteIndex);
        }
    }

    public void OnMixButtonPressed()
    {
        MixSprites();
        
        _isCanMove = true;
    }
    private void MixSprites()
    {
        for (var currentSequenceCount = _currentSequence.Count - 1; currentSequenceCount > 0; currentSequenceCount--)
        {
            var rangeNumber = Random.Range(0, currentSequenceCount + 1);
            
            (_currentSequence[currentSequenceCount], _currentSequence[rangeNumber]) = (_currentSequence[rangeNumber], _currentSequence[currentSequenceCount]);
            (_spawnedSprites[currentSequenceCount].transform.localPosition, _spawnedSprites[rangeNumber].transform.localPosition) = (_spawnedSprites[rangeNumber].transform.localPosition, _spawnedSprites[currentSequenceCount].transform.localPosition);
        }

        mixButton.gameObject.SetActive(false);
        readyButton.gameObject.SetActive(true);
    }

    private int _lastIndex = -1;

    private void SwapSprite(GameObject sprite)
    {
        if (!_isCanMove)
            return;

        var indexA = _spawnedSprites.IndexOf(sprite);
        if (indexA < 0) 
            return;

        if (_lastIndex == -1)
        {
            _lastIndex = indexA;
            return;
        }

        if (_lastIndex == indexA)
            return;
        
        (_spawnedSprites[_lastIndex].transform.localPosition, _spawnedSprites[indexA].transform.localPosition) = (_spawnedSprites[indexA].transform.localPosition, _spawnedSprites[_lastIndex].transform.localPosition);
        (_currentSequence[_lastIndex], _currentSequence[indexA]) = (_currentSequence[indexA], _currentSequence[_lastIndex]);
        
        _lastIndex = -1;
    }

    private void CheckSequence()
    {
        if (_currentSequence.Count != _correctSequence.Count)
            return;

        if (_currentSequence.Where((t, i) => t != _correctSequence[i]).Any())
        {
            ShowLoseMenu();
            return;
        }

        ShowWinMenu();
    }

    private void ShowWinMenu()
    {
        WinPanel.SetActive(true);
    }

    private void ShowLoseMenu()
    {
        LousePanel.SetActive(true);
    }
}