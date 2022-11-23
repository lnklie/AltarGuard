using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField] private Item curItem;
    [SerializeField] private SpriteRenderer itemImage = null;
    [SerializeField] private SpriteRenderer itemRankRing = null;
    [SerializeField] private Sprite[] itemRankRings = new Sprite[5];
    private bool isItem = false;


    public Item CurItem { get { return curItem; } }
    public bool IsItem { get { return isItem; } set { isItem = value; }}

    private void Awake()
    {
    }

    public void SetItem(Item _item)
    {
        // 아이템 세팅
        curItem = _item;

        if (itemImage != null)
        {
            itemImage.sprite = _item.singleSprite;
            itemRankRing.sprite = itemRankRings[_item.itemRank];
        }
        else
            Debug.Log("이미지 비어있음");
    }
}
