using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LogText : MonoBehaviour
{
    private TextMeshProUGUI logText;
    private void Awake()
    {
        logText = GetComponent<TextMeshProUGUI>();
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
