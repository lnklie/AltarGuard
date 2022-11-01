using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CraftNecessaryItemInventorySlot : MonoBehaviour
{
    [SerializeField] private Item necessaryRegisterItem = null;
    [SerializeField] private Image[] necessaryRegisterItemImage = null;
    [SerializeField] private TextMeshProUGUI countText = null;
    [SerializeField] private bool isNecessaryInventoryItem = false;
    [SerializeField] private Sprite defaultSprite = null;
    public Item NecessaryRegisterItem { get { return necessaryRegisterItem; } set { necessaryRegisterItem = value; } }

    public void Awake()
    {
        necessaryRegisterItemImage = GetComponentsInChildren<Image>();
        countText = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void InitSlot()
    {
        necessaryRegisterItem = null;
        isNecessaryInventoryItem = false;
        necessaryRegisterItemImage[1].sprite = defaultSprite;
        necessaryRegisterItemImage[1].rectTransform.sizeDelta = new Vector2(100f, 100f);
        countText.text = "00";

    }
    public void SetSlot(Item _item)
    {
        necessaryRegisterItem = _item;
        isNecessaryInventoryItem = true;
        necessaryRegisterItemImage[1].sprite = _item.singleSprite;
        ActiveItemCount();
    }
    private void ActiveItemCount()
    {
        // 소모품이나 쌓이는 아이템이면 count 텍스트를 활성화
        if (necessaryRegisterItem.itemKey / 1000 < 9)
        {
            EnableItemCount(false);
        }
        else
        {
            EnableItemCount(true);
            countText.text = necessaryRegisterItem.count.ToString();
        }
    }
    public void EnableItemCount(bool _bool)
    {
        countText.enabled = _bool;
    }
    public void SetRegisterNecessaryItem(Item _item)
    {
        necessaryRegisterItem = _item;
    }
    public void SelectInventoryItem()
    {
        if (isNecessaryInventoryItem)
            UIManager.Instance.SelectRegisterNecessaryItemPanel(NecessaryRegisterItem);
        else
            Debug.Log("비어 있는 인벤토리");
    }
}
