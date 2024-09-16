using TMPro;
using UnityEngine;

public class ChangingActionText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textUp;

    public void DownButtonText()
    {
        _textUp.text = "Restore the correct sequence of elements";
    }
}
