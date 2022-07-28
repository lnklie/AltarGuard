using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogText : MonoBehaviour
{
    private Text logText;
    private void Awake()
    {
        logText = GetComponent<Text>();
    }

    public void SetText(string _log)
    {
        logText.text = _log;
    }
    public string GetText()
    {
        return logText.text;
    }
}
