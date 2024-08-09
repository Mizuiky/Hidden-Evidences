using UnityEngine;
using TMPro;
using System;

public class DialogOption : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public static Action<int> onChooseOption;
    private int optionIndex;
    public int OptionIndex { get { return optionIndex; } }

    public void Init()
    {
        Reset();
    }

    public void Reset()
    {
        SetText("");
        SetIndex(-1);
    }

    public void SetText(string optionText)
    {
        text.text = optionText;
    }

    public void SetIndex(int index)
    {
        optionIndex = index;
    }

    public void SendOptionIndex()
    {
        onChooseOption?.Invoke(optionIndex);
    }

    public void EnableOption(bool enable)
    {
        this.gameObject.SetActive(enable);
    }
}
