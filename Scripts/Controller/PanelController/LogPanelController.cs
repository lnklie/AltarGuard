using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogPanelController : MonoBehaviour
{
    [SerializeField]
    private List<LogText> logTexts = new List<LogText>();
    [SerializeField]
    private int curActiveIndex = 0;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
            SetLog(" okok  " + curActiveIndex.ToString());
    }

    public void SetLog(string _log)
    {
        string _timeLog = "[" + System.DateTime.Now.ToString()+"] " + _log;
        if(logTexts.Count > curActiveIndex)
        {
            logTexts[curActiveIndex].gameObject.SetActive(true);
            logTexts[curActiveIndex].SetText(_timeLog);
            curActiveIndex++;
        }
        else
        {
            for (int i = 1; i < curActiveIndex; i++)
            {
                logTexts[i - 1].SetText(logTexts[i].GetText());
            }

            logTexts[curActiveIndex - 1].SetText(_timeLog);
        }
    }
}
