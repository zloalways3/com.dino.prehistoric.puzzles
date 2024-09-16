using System;
using TMPro;
using UnityEngine;

public class ChangingLevelText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _textLevels;

    private void Awake()
    {
        foreach (var VARIABLE in _textLevels)
        {
            VARIABLE.text = PlayerPrefs.GetInt(ConstGlobalVaults.LEVEL_SAVE_PREFS, 0) + " level";
        }
    }
}
