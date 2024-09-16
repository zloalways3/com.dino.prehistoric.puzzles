using UnityEngine;
using UnityEngine.UI;

public class RankRouter : MonoBehaviour
{
    [SerializeField] private Button[] _levelsButtonGame;

    private MainUnblocker _mainUnblocker;

    private void Start()
    {
        _mainUnblocker = FindObjectOfType<MainUnblocker>();
        _mainUnblocker.СonceptLevelsButtons();
        AddPaletteLevelsButton();
    }

    private void AddPaletteLevelsButton()
    {
        for (var indexNumber = 0; indexNumber < ConstGlobalVaults.LEVELS_GENERAL; indexNumber++)
        {
            if (indexNumber == 0 || _mainUnblocker.IsLevelUnlocked(indexNumber))
            {
                var levelIndexNumber = indexNumber;
                _levelsButtonGame[indexNumber].onClick.AddListener(() => _mainUnblocker.ChangeStageLevel(levelIndexNumber));
            }
            else
            {
                _levelsButtonGame[indexNumber].interactable = false;
            }
        }
    }
}