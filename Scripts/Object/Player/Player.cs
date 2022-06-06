using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
==============================
 * ���������� : 2022-06-05
 * �ۼ��� : Inklie
 * ���ϸ� : Player.cs
==============================
*/
[System.Serializable]
public class Player : Character
{
    public int money = 0;
    public int stage = 0;
    public int mercenaryCount = 0;
    public Character[] mercenaries = null;
    public List<Item> decoItems = new List<Item>();
    public List<Item> weaponItems = new List<Item>();
    public List<Item> equipmentItems = new List<Item>();
    public List<Item> consumablesItems = new List<Item>();
    public List<Item> miscellaneousItems = new List<Item>();
}
