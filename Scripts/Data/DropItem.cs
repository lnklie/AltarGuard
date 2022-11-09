using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    private SpriteRenderer spriteRenderer = null;
    [SerializeField] private Item curItem;
    private bool isItem = false;


    public Item CurItem { get { return curItem; } }
    public bool IsItem { get { return isItem; } set { isItem = value; }}

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetItem(Item _item)
    {
        // 아이템 세팅
        curItem = _item;

        if (spriteRenderer != null)
            spriteRenderer.sprite = _item.singleSprite;
        else
            Debug.Log("이미지 비어있음");
    }
}
