using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoolValueText_panelUI : MonoBehaviour
{
    [SerializeField] private Image _boolIndicator;
    [SerializeField] private TextMeshProUGUI _valueText;
    [SerializeField] private TextMeshProUGUI _nameText;

    public void SetValues(bool isProficient, string skillValue)
    {
        if(isProficient) _boolIndicator.color = Color.black;
        _valueText.text = skillValue;
    }
    public void SetValues(bool isProficient, string skillValue, string nameText)
    {
        if(isProficient) _boolIndicator.color = Color.black;
        _valueText.text = skillValue;
        _nameText.text = nameText;
    }
    public void SetValuesNameAndBool(bool isProficient, string skillValue, string nameText)
    {
        if(isProficient) _boolIndicator.color = Color.black;
        _valueText.text = skillValue;
        _nameText.text = nameText;
    }
}
