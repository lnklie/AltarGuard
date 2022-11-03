using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Lines
{
    public string script = null;
    public float scriptSpeed = 0f;
    public float scriptAniSpeed = 0f;
    public float timeLag = 0f;
    public Lines(string _script, float _scriptSpeed, float _scriptAniSpeed, float _timeLag)
    {
        this.script = _script;
        this.scriptSpeed = _scriptSpeed;
        this.scriptAniSpeed = _scriptAniSpeed;
        this.timeLag = _timeLag;
    }
}
