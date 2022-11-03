using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * ���������� : 2022-06-05
 * �ۼ��� : Inklie
 * ���ϸ� : Character.cs
==============================
*/
[System.Serializable]
public class Character : Elements
{
    public int level = 1;
    public int exp = 0;
    public int statusPoint = 0;
    public float str = 5;
    public float dex = 5;
    public float wiz = 5;
    public float luck = 5;
    public bool[] checkEquipItems = { false, false, false, false, false, false, false, false, false };
    public Item[] equipedItems = new Item[9];
}
