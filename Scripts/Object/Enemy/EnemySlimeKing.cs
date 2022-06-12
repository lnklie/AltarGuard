using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
/*
==============================
 * ���������� : 2022-06-05
 * �ۼ��� : Inklie
 * ���ϸ� : EnemySlimeKing.cs
==============================
*/
public class EnemySlimeKing : Enemy
{
    public EnemySlimeKing(string _name, int _hp, int _damage, float _seeRange, float _atkRange, float _speed, float _atkSpeed, int _defeatExp, int _itemDropKey1, int _itemDropKey2, int _itemDropKey3, int _itemDropKey4, int _itemDropKey5, float _itemDropProb1, float _itemDropProb2, float _itemDropProb3, float _itemDropProb4, float _itemDropProb5) : base(_name, _hp, _damage, _seeRange, _atkRange, _speed, _atkSpeed, _defeatExp, _itemDropKey1, _itemDropKey2, _itemDropKey3, _itemDropKey4, _itemDropKey5, _itemDropProb1, _itemDropProb2, _itemDropProb3, _itemDropProb4, _itemDropProb5)
    {
        enemyType = EnemyType.SlimeKing;
        controller = AssetDatabase.LoadAssetAtPath(path, typeof(SlimeKingAIController)) as SlimeKingAIController;
    }
}