using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LogPanelController : MonoBehaviour
{
    [SerializeField] private List<LogText> logTexts = new List<LogText>();
    [SerializeField] private int curActiveIndex = 0;
    [SerializeField] private int logSizeLevel = 2;
    [SerializeField] private Image Panel = null;
    [SerializeField] private ScrollController scrollController = null;

    public ScrollController ScrollController { get { return scrollController; } }   
    private void Start()
    {
        SetLogPanelSize();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
            SetLog(" okok  " + curActiveIndex.ToString());
    }
    public void SetLogPanelSize()
    {
        if (logSizeLevel == 0)
        {
            Panel.rectTransform.sizeDelta = new Vector3(700, 60f);
            scrollController.SetLimitRectPos(500, -280f);
        }
        else if (logSizeLevel == 1)
        {
            Panel.rectTransform.sizeDelta = new Vector3(700, 175f);
            scrollController.SetLimitRectPos(500, -165f);
        }
        else if (logSizeLevel == 2)
        {
            Panel.rectTransform.sizeDelta = new Vector3(700, 350f);
            scrollController.SetLimitRectPos(500, 10f);
        }
        else if(logSizeLevel == 3)
        {
            Panel.rectTransform.sizeDelta = new Vector3(700, 500f);
            scrollController.SetLimitRectPos(500, 160f);
        }
        scrollController.InitRectPos();
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
    public void SetLogSizeLevel(bool _bool)
    {
        if(_bool)
        {
            if (logSizeLevel != 3)
                logSizeLevel++;
        }
        else
        {
            if (logSizeLevel != 0)
                logSizeLevel-- ;
        }
        SetLogPanelSize();
    }
}
