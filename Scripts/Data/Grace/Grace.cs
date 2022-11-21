using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Grace
{
    public int graceKey = -1;
    public string graceKorName = null;
    public float weightedValue = 0f;
    public List<int> nextComponents = new List<int>();
    public int nextComponent1 = -1;
    public int nextComponent2 = -1;
    public int nextComponent3 = -1;
    public Grace(int _graceKey, string _graceKorName, float _weightedValue)
    {
        this.graceKey = _graceKey;
        this.graceKorName = _graceKorName;
        this.weightedValue = _weightedValue;
    }
}
 