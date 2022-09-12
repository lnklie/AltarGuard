using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameScript
{
    public int scriptKey = -1;

    public int actorNum0 = -1;
    public int actorNum1 = -1;
    public int actorNum2 = -1;
    public int actorNum3 = -1;
    public int actorNum4 = -1;
    public int actorNum5 = -1;
    public int actorNum6 = -1;
    public int actorNum7 = -1;
    public int actorNum8 = -1;
    public int actorNum9 = -1;

    public string script0 = null;
    public string script1 = null;
    public string script2 = null;
    public string script3 = null;
    public string script4 = null;
    public string script5 = null;
    public string script6 = null;
    public string script7 = null;
    public string script8 = null;
    public string script9 = null;

    public float scriptSpeed0 = 0f;
    public float scriptSpeed1 = 0f;
    public float scriptSpeed2 = 0f;
    public float scriptSpeed3 = 0f;
    public float scriptSpeed4 = 0f;
    public float scriptSpeed5 = 0f;
    public float scriptSpeed6 = 0f;
    public float scriptSpeed7 = 0f;
    public float scriptSpeed8 = 0f;
    public float scriptSpeed9 = 0f;

    public float scriptAniSpeed0 = 0f;
    public float scriptAniSpeed1 = 0f;
    public float scriptAniSpeed2 = 0f;
    public float scriptAniSpeed3 = 0f;
    public float scriptAniSpeed4 = 0f;
    public float scriptAniSpeed5 = 0f;
    public float scriptAniSpeed6 = 0f;
    public float scriptAniSpeed7 = 0f;
    public float scriptAniSpeed8 = 0f;
    public float scriptAniSpeed9 = 0f;

    public float timeLag0 = 0f;
    public float timeLag1 = 0f;
    public float timeLag2 = 0f;
    public float timeLag3 = 0f;
    public float timeLag4 = 0f;
    public float timeLag5 = 0f;
    public float timeLag6 = 0f;
    public float timeLag7 = 0f;
    public float timeLag8 = 0f;


    public GameScript(int _scriptKey, int _actorNum0, int _actorNum1, int _actorNum2, int _actorNum3, int _actorNum4, int _actorNum5, int _actorNum6, int _actorNum7, int _actorNum8, int _actorNum9,
        string _script0, string _script1, string _script2, string _script3, string _script4, string _script5, string _script6, string _script7, string _script8, string _script9, 
        float _scriptSpeed0, float _scriptSpeed1, float _scriptSpeed2, float _scriptSpeed3, float _scriptSpeed4, float _scriptSpeed5, float _scriptSpeed6, float _scriptSpeed7, float _scriptSpeed8, float _scriptSpeed9,
        float _scriptAniSpeed0, float _scriptAniSpeed1, float _scriptAniSpeed2, float _scriptAniSpeed3, float _scriptAniSpeed4, float _scriptAniSpeed5, float _scriptAniSpeed6, float _scriptAniSpeed7, float _scriptAniSpeed8, float _scriptAniSpeed9,
        float _timeLag0, float _timeLag1, float _timeLag2, float _timeLag3, float _timeLag4, float _timeLag5, float _timeLag6, float _timeLag7, float _timeLag8)
    {
        this.scriptKey = _scriptKey;
        this.actorNum0 = _actorNum0;
        this.actorNum1 = _actorNum1;
        this.actorNum2 = _actorNum2;
        this.actorNum3 = _actorNum3;
        this.actorNum4 = _actorNum4;
        this.actorNum5 = _actorNum5;
        this.actorNum6 = _actorNum6;
        this.actorNum7 = _actorNum7;
        this.actorNum8 = _actorNum8;
        this.actorNum9 = _actorNum9;

        this.script0 = _script0;
        this.script1 = _script1;
        this.script2 = _script2;
        this.script3 = _script3;
        this.script4 = _script4;
        this.script5 = _script5;
        this.script6 = _script6;
        this.script7 = _script7;
        this.script8 = _script8;
        this.script9 = _script9;

        this.scriptSpeed0 = _scriptSpeed0;
        this.scriptSpeed1 = _scriptSpeed1;
        this.scriptSpeed2 = _scriptSpeed2;
        this.scriptSpeed3 = _scriptSpeed3;
        this.scriptSpeed4 = _scriptSpeed4;
        this.scriptSpeed5 = _scriptSpeed5;
        this.scriptSpeed6 = _scriptSpeed6;
        this.scriptSpeed7 = _scriptSpeed7;
        this.scriptSpeed8 = _scriptSpeed8;
        this.scriptSpeed9 = _scriptSpeed9;

        this.scriptAniSpeed0 = _scriptAniSpeed0;
        this.scriptAniSpeed1 = _scriptAniSpeed1;
        this.scriptAniSpeed2 = _scriptAniSpeed2;
        this.scriptAniSpeed3 = _scriptAniSpeed3;
        this.scriptAniSpeed4 = _scriptAniSpeed4;
        this.scriptAniSpeed5 = _scriptAniSpeed5;
        this.scriptAniSpeed6 = _scriptAniSpeed6;
        this.scriptAniSpeed7 = _scriptAniSpeed7;
        this.scriptAniSpeed8 = _scriptAniSpeed8;
        this.scriptAniSpeed9 = _scriptAniSpeed9;

        this.timeLag0 = _timeLag0;
        this.timeLag1 = _timeLag1;
        this.timeLag2 = _timeLag2;
        this.timeLag3 = _timeLag3;
        this.timeLag4 = _timeLag4;
        this.timeLag5 = _timeLag5;
        this.timeLag6 = _timeLag6;
        this.timeLag7 = _timeLag7;
        this.timeLag8 = _timeLag8;
    }
}
