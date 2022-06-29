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

    public Grace (int _graceKey, string _explain)
    {
        graceKey = _graceKey;
        explain = _explain;
    }
}
