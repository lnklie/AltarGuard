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
    public int str = 5;
    public int dex = 5;
    public int wiz = 5;
    public int luck = 5;
    public bool[] checkEquipItems = { false, false, false, false, false, false, false, false, false };
    public Item[] equipedItems = new Item[9];
}
