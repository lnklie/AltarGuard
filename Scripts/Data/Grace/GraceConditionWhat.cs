using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GraceConditionWhat : Grace
{


    public GraceConditionWhat(int _graceKey, string _graceKorName, float _weightedValue, int _nextConditionHow1, int _nextConditionHow2, int _nextConditionHow3) : base(_graceKey, _graceKorName, _weightedValue)
    {
        nextComponent1 = _nextConditionHow1;
        nextComponent2 = _nextConditionHow2;
        nextComponent3 = _nextConditionHow3;
        if (nextComponent1 != -1)
            nextComponents.Add(nextComponent1);
        if (nextComponent2 != -1)
            nextComponents.Add(nextComponent2);
        if (nextComponent3 != -1)
            nextComponents.Add(nextComponent3);
    }

}
