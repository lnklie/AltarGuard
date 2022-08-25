using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Grace
{
    public int graceKey = -1;
    public string graceName = null;
    public bool isActive = false;
    public string explain = null;
    public int necessaryGraceKey = -1;
    public int conditionWho = -1;
    public int conditionWhat = -1;
    public int conditionValue = -1;
    public int conditionHow = -1;
    public int resultWho = -1;
    public int resultTarget = -1;
    public int resultWhat1 = -1;
    public int resultWhat2 = -1;
    public int resultValue1 = -1;
    public int resultValue2 = -1;
    public int resultValue1IsPercent = -1;
    public int resultValue2IsPercent = -1;
    public int resultHow1 = -1;
    public int resultHow2 = -1;
    public Grace(int _graceKey, string _graceName, string _explain, int _necessaryGraceKey, int _conditionWho, int _conditionWhat, int _conditionValue, int _conditionHow,
        int _resultWho, int _resultTarget, int _resultWhat1, int _resultWhat2, int _resultValue1, int _resultValue2, int _resultValue1IsPercent, int _resultValue2IsPercent, int _resultHow1, int _resultHow2)
    {
        this.graceKey = _graceKey;
        this.graceName = _graceName;
        this.explain = _explain;
        this.necessaryGraceKey = _necessaryGraceKey;
        this.conditionWho = _conditionWho;
        this.conditionWhat = _conditionWhat;
        this.conditionValue = _conditionValue;
        this.conditionHow = _conditionHow;
        this.resultWho = _resultWho;
        this.resultTarget = _resultTarget;
        this.resultWhat1 = _resultWhat1;
        this.resultWhat2 = _resultWhat2;
        this.resultValue1 = _resultValue1;
        this.resultValue2 = _resultValue2;
        this.resultValue1IsPercent = _resultValue1IsPercent;
        this.resultValue2IsPercent = _resultValue2IsPercent;
        this.resultHow1 = _resultHow1;
        this.resultHow2 = _resultHow2;
    }
}
