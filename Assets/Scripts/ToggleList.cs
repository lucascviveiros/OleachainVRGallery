using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleList : MonoBehaviour
{
    [SerializeField]
    private Text textUI;

    [SerializeField]
    private Button buttonList;

    private int index;

    public void SetToggleText(string text, int i)
    {
        index = i;
        string result = text.Substring(text.LastIndexOf('/') + 1);
        textUI.text = result;
    }

    public int GetIndex()
    {
        return index;
    }

    public Button GetButton()
    {
        return buttonList;
    }
}
