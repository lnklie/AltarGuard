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
    public int resultWho1 = -1;
    public int resultWho2 = -1;
    public int resultWhat1 = -1;
    public int resultWhat2 = -1;
    public int resultValue1 = -1;
    public int resultValue2 = -1;
    public int resultValueIsPercent1 = -1;
    public int resultValueIsPercent2 = -1;
    public int resultHow1 = -1;
    public int resultHow2 = -1;
    public int relationOfVariables = -1;

    public CompleteGrace(int conditionWho, int conditionWhat, int conditionValue, int conditionHow, int resultWho1, int resultWho2, int resultWhat1, int resultWhat2, int resultValue1, int resultValue2, int resultValueIsPercent1, int resultValueIsPercent2, int resultHow1, int resultHow2, int relationOfVariables)
    {
        this.conditionWho = conditionWho;
        this.conditionWhat = conditionWhat;
        this.conditionValue = conditionValue;
        this.conditionHow = conditionHow;
        this.resultWho1 = resultWho1;
        this.resultWho2 = resultWho2;
        this.resultWhat1 = resultWhat1;
        this.resultWhat2 = resultWhat2;
        this.resultValue1 = resultValue1;
        this.resultValue2 = resultValue2;
        this.resultValueIsPercent1 = resultValueIsPercent1;
        this.resultValueIsPercent2 = resultValueIsPercent2;
        this.resultHow1 = resultHow1;
        this.resultHow2 = resultHow2;
        this.relationOfVariables = relationOfVariables;
    }
}
