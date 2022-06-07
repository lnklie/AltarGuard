using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * 최종수정일 : 2022-06-05
 * 작성자 : Inklie
 * 파일명 : Enemy.cs
==============================
*/
[System.Serializable]
public class Enemy : Elements
{
    public int hp = 0;
    public int damage = 0;
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public float seeRange;
    public float SeeRange
    {
        get { return seeRange; }
        set { seeRange = value; }
    }

    public float atkRange;
    public float AtkRange
    {
        get { return atkRange; }
        set { atkRange = value; }
    }

    public float speed = 0f;
    public float Speed
    {
        get { return speed; }
    }

    public float atkSpeed = 0f;
    public float AtkSpeed
    {
        get { return atkSpeed; }
        set { atkSpeed = value; }
    }

    public int defeatExp = 0;
    public int DefeatExp
    { 
        get { return defeatExp; }
        set { defeatExp = value; }
    }

    public int[] itemDropKey = { -1,-1,-1,-1,-1 };
    public int[] ItemDropKey
    {
        get { return itemDropKey; }
        set { itemDropKey = value; }    
    }

    public float[] itemDropProb = { 20, 20, 20, 20, 20 };
    public float[] ItemDropProb
    {
        get { return itemDropProb; }
        set { itemDropProb = value; }
    }

    public Sprite singleSprite = null;
    public Enemy(string _name, int _hp, int _damage, float _seeRange, float _atkRange, float _speed, float _atkSpeed, int _defeatExp, 
        int _itemDropKey1, int _itemDropKey2, int _itemDropKey3, int _itemDropKey4, int _itemDropKey5,
        float _itemDropProb1, float _itemDropProb2, float _itemDropProb3, float _itemDropProb4, float _itemDropProb5)
    {
        objectName = _name;
        hp = _hp;
        damage = _damage;
        seeRange = _seeRange;
        atkRange = _atkRange;
        speed = _speed;
        atkSpeed = _atkSpeed;
        defeatExp = _defeatExp;
        itemDropKey[0] = _itemDropKey1;
        itemDropKey[1] = _itemDropKey2;
        itemDropKey[2] = _itemDropKey3;
        itemDropKey[3] = _itemDropKey4;
        itemDropKey[4] = _itemDropKey5;
        itemDropProb[0] = _itemDropProb1;
        itemDropProb[1] = _itemDropProb2;
        itemDropProb[2] = _itemDropProb3;
        itemDropProb[3] = _itemDropProb4;
        itemDropProb[4] = _itemDropProb5;
        singleSprite = Resources.Load("Sprites/13_Consumable/" + objectName, typeof(Sprite)) as Sprite;
    }
}
