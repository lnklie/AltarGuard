using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GraceConditionWho : Grace
{
    public GraceConditionWho(int _graceKey, string _graceKorName, float _weightedValue) : base(_graceKey, _graceKorName,_weightedValue)
    {
    }
}
