using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeWriterEffect : MonoBehaviour
{
    public TextMeshProUGUI textUI;
    private float delayWriter = 0.03f;
    private string myText = "Welcome to OleaVRPlayer. \nYou can use your virtual hands to select the 360 videos.";

    void Start()
    {
        StartCoroutine("TypeWriter", myText);
    }

    private IEnumerator TypeWriter(string textType)
    {
        textUI.text = "";

        for (int letter = 0; letter < textType.Length; letter++)
        {
            textUI.text = textUI.text + textType[letter];
            yield return new WaitForSeconds(delayWriter);
        }
    }
}
