using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GraceResultTarget : Grace
{
    public GraceResultTarget(int _graceKey, string _graceKorName, float _weightedValue) : base(_graceKey, _graceKorName, _weightedValue)
    {
    }
}
