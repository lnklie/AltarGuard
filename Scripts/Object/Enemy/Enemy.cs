using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-09
 * 작성자 : Inklie
 * 파일명 : Enemy.cs
==============================
*/
[System.Serializable]
public class Enemy : Elements
{
    public int enemyKey = 0;
    public int hp = 0;
    public int mp = 0;
    public int str = 0;
    public int dex = 0;
    public int wiz = 0;
    public float seeRange;
    public float speed = 0f;
    public int defeatExp = 0;
    public int itemDropKey1 = -1;
    public int itemDropKey2 = -1;
    public int itemDropKey3 = -1;
    public int itemDropKey4 = -1;
    public int itemDropKey5 = -1;
    public float itemDropProb1 = 20f;
    public float itemDropProb2 = 20f;
    public float itemDropProb3 = 20f;
    public float itemDropProb4 = 20f;
    public float itemDropProb5 = 20f;

    public Enemy(string _name, int _enemyKey, int _hp, int _mp, int _str, int _dex, int _wiz, float _seeRange, float _speed, int _defeatExp,
        int _itemDropKey1, int _itemDropKey2, int _itemDropKey3, int _itemDropKey4, int _itemDropKey5,
        float _itemDropProb1, float _itemDropProb2, float _itemDropProb3, float _itemDropProb4, float _itemDropProb5)
    {
        objectName = _name;
        enemyKey = _enemyKey;
        str = _str;
        dex = _dex;
        wiz = _wiz;
        hp = _hp;
        mp = _mp;
        seeRange = _seeRange;
        speed = _speed;
        defeatExp = _defeatExp;
        itemDropKey1 = _itemDropKey1;
        itemDropKey2 = _itemDropKey2;
        itemDropKey3 = _itemDropKey3;
        itemDropKey4 = _itemDropKey4;
        itemDropKey5 = _itemDropKey5;
        itemDropProb1 = _itemDropProb1;
        itemDropProb2 = _itemDropProb2;
        itemDropProb3 = _itemDropProb3;
        itemDropProb4 = _itemDropProb4;
        itemDropProb5 = _itemDropProb5;
    }
}
