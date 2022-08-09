using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftNecessaryItemSlot : MonoBehaviour
{
    [SerializeField]
    private Item necessaryItem = null;
    [SerializeField]
    private bool isNecessaryItem = false;
    public Item NecessaryItem
    {
        get { return necessaryItem; }
        set { necessaryItem = value; }
    }
    public bool IsNecessaryItem
    {
        get { return isNecessaryItem; }
        set { isNecessaryItem = value; }
    }
    public void SelectNecessaryItem()
    {
        if (necessaryItem != null)
        {
            UIManager.Instance.SelectNecessaryItem(necessaryItem);
        }
        else
        {
            Debug.Log("비어있는 아이템");
        }
    }
}
