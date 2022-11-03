using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonManager<GameManager>
{
    [SerializeField] private string gameVersion = null;
    [SerializeField] private float sleepModeTime = 0f;
    [SerializeField] private ESleepModeTime EsleepModetime = ESleepModeTime.TurnOff;
    [SerializeField] private bool isSleepMode = false;
    public string GameVersion { get { return gameVersion; } set { gameVersion = value; } }
    public ESleepModeTime SleepModeTime { get { return EsleepModetime; } set { EsleepModetime = value; } }
    private void Update()
    {
        if (EsleepModetime != ESleepModeTime.TurnOff || !isSleepMode)
        {
            sleepModeTime += Time.deltaTime;
            SetConditionSleepMode();
        }


        if (Input.anyKey || isSleepMode)
            sleepModeTime = 0f;
    }
    public void LoadScene(int _sceneNum)
    {
        SceneManager.LoadSceneAsync(_sceneNum);
    }
    public void SetConditionSleepMode()
    {
        switch(EsleepModetime)
        {
            case ESleepModeTime.FiveMin:
                if (sleepModeTime >= 300f)
                {
                    OperateSleepMode();
                }
                break;
            case ESleepModeTime.TenMin:
                if (sleepModeTime >= 600f)
                {
                    OperateSleepMode();
                }
                break;
            case ESleepModeTime.ThirtyMin:
                if (sleepModeTime >= 1800f)
                {
                    OperateSleepMode();
                }
                break;
            case ESleepModeTime.SixtyMin:
                if (sleepModeTime >= 3600f)
                {
                    OperateSleepMode();
                }
                break;
        }
    }
    public void OperateSleepMode()
    {
        Debug.Log("���� ��� ����");
        isSleepMode = true;
    }
}
