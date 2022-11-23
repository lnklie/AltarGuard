 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BigGrace : CompleteGrace
{
    public int bigGraceKey = -1;
    public string bigGraceName = null;
    public int necessaryBigGraceKey = -1;

    public BigGrace(int _bigGraceKey, string _bigGraceName, string _explain, int _necessaryBigGraceKey, int conditionWho, int conditionWhat, int conditionValue, int conditionHow, int resultWho1, int resultWho2, int resultWhat1, int resultWhat2, int resultValue1, int resultValue2, int resultValueIsPercent1, int resultValueIsPercent2, int resultHow1, int resultHow2)
        : base(conditionWho, conditionWhat, conditionValue, conditionHow, resultWho1, resultWho2, resultWhat1, resultWhat2, resultValue1, resultValue2, resultValueIsPercent1, resultValueIsPercent2, resultHow1, resultHow2)
    {
        bigGraceKey = _bigGraceKey;
        bigGraceName = _bigGraceName;
        explain = _explain;
        necessaryBigGraceKey = _necessaryBigGraceKey;
    }
}
