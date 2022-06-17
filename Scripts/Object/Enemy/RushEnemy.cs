using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-05
 * 작성자 : Inklie
 * 파일명 : RushEnemy.cs
==============================
*/
[System.Serializable]
public class RushEnemy : Enemy
{
    public int helmetKey = 0;
    public int armorKey = 0;
    public int pantKey = 0;
    public int weaponKey = 0;

    public RushEnemy(string _name, int _enemyKey, int _hp, int _mp, int _str, int _dex, int _wiz, float _seeRange, float _speed, int _defeatExp, int _itemDropKey1, int _itemDropKey2, int _itemDropKey3, int _itemDropKey4, int _itemDropKey5, float _itemDropProb1, float _itemDropProb2, float _itemDropProb3, float _itemDropProb4, float _itemDropProb5, int _helmetKey, int _armorKey, int _pantKey, int _weaponKey) : base(_name, _enemyKey, _hp, _mp, _str, _dex, _wiz, _seeRange, _speed, _defeatExp, _itemDropKey1, _itemDropKey2, _itemDropKey3, _itemDropKey4, _itemDropKey5, _itemDropProb1, _itemDropProb2, _itemDropProb3, _itemDropProb4, _itemDropProb5)
    {
    }
}