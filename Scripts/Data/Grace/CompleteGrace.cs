using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CompleteGrace
{
    public bool isActive = false;
    public string explain = null;

    public int conditionWho = -1;
    public int conditionWhat = -1;
    public int conditionValue = -1;
    public int conditionHow = -1;
    public int resultWho = -1;
    public int resultWhat = -1;
    public int resultValue = -1;
    public int resultValueIsPercent = -1;
    public int resultHow = -1;
    public CompleteGrace(string _explain, int _conditionWho, int _conditionWhat, int _conditionValue , int _conditionHow,
        int _resultWho, int _resultWhat, int _resultValue, int _resultValueIsPercent, int _resultHow)
    {
        this.explain = _explain;
        this.conditionWho = _conditionWho;
        this.conditionWhat = _conditionWhat;
        this.conditionValue = _conditionValue;
        this.conditionHow = _conditionHow;
        this.resultWho = _resultWho;
        this.resultWhat = _resultWhat;
        this.resultValue = _resultValue;
        this.resultValueIsPercent = _resultValueIsPercent;
        this.resultHow = _resultHow;
    }
}
