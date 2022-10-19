using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonManager<GameManager>
{
    [SerializeField] private string gameVersion = null;
    [SerializeField] private float sleepModeTime = 0f;
    [SerializeField] private ESleepModeTime EsleepModetime = ESleepModeTime.TurnOff;

    public string GameVersion { get { return gameVersion; } set { gameVersion = value; } }
    public ESleepModeTime SleepModeTime { get { return EsleepModetime; } set { EsleepModetime = value; } }
    private void Update()
    {
        if (EsleepModetime != ESleepModeTime.TurnOff)
            sleepModeTime += Time.deltaTime;
    }
}
