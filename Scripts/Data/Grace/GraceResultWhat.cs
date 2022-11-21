using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GraceResultWhat : Grace
{
    public GraceResultWhat(int _graceKey, string _graceKorName, float _weightedValue, int _nextResultHow1, int _nextResultHow2, int _nextResultHow3) : base(_graceKey, _graceKorName, _weightedValue)
    {
        nextComponent1 = _nextResultHow1;
        nextComponent2 = _nextResultHow2;
        nextComponent3 = _nextResultHow3;

        if (nextComponent1 != -1)
            nextComponents.Add(nextComponent1);
        if (nextComponent2 != -1)
            nextComponents.Add(nextComponent2);
        if (nextComponent3 != -1)
            nextComponents.Add(nextComponent3);
    }
}
