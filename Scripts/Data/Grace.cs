using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Grace
{
    public int graceKey = -1;
    public int graceType = -1;
    public int isActive = 0;
    public string explain = null;
    public int necessaryGraceKey = -1;
    public Grace (int _graceKey, string _explain, int _necessaryGraceKey)
    {
        graceKey = _graceKey;
        explain = _explain;
        necessaryGraceKey = _necessaryGraceKey;
    }
}
