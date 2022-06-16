using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
/*
==============================
 * 최종수정일 : 2022-06-05
 * 작성자 : Inklie
 * 파일명 : EnemySlime.cs
==============================
*/
public class EnemySlime : Enemy
{
    public EnemySlime(string _name, int _enemyKey, int _hp, int _mp, int _str, int _dex, int _wiz, float _seeRange, float _atkRange, float _speed, float _atkSpeed, int _defeatExp, int _itemDropKey1, int _itemDropKey2, int _itemDropKey3, int _itemDropKey4, int _itemDropKey5, float _itemDropProb1, float _itemDropProb2, float _itemDropProb3, float _itemDropProb4, float _itemDropProb5) : base(_name, _enemyKey, _hp, _mp, _str, _dex, _wiz, _seeRange, _atkRange, _speed, _atkSpeed, _defeatExp, _itemDropKey1, _itemDropKey2, _itemDropKey3, _itemDropKey4, _itemDropKey5, _itemDropProb1, _itemDropProb2, _itemDropProb3, _itemDropProb4, _itemDropProb5)
    {
    }
}