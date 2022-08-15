using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CraftNecessaryItemSlot : MonoBehaviour
{
    [SerializeField]
    private int slotIndex = -1;
    [SerializeField]
    private Item necessaryItem = null;
    [SerializeField]
    private bool isNecessaryItem = false;

    [SerializeField]
    private Image[] necessaryItemImage = null;

    [SerializeField]
    private TextMeshProUGUI countText = null;
    [SerializeField]
    private Sprite defalutSprite = null;
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
    public void Awake()
    {
        necessaryItemImage = GetComponentsInChildren<Image>();
        countText = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void InitNecessaryItem()
    {

        isNecessaryItem = false;
        necessaryItem = null;
        necessaryItemImage[1].rectTransform.sizeDelta = new Vector2(100f, 100f);
        necessaryItemImage[1].sprite = defalutSprite;

    }
    public void InitNecessaryItemCount()
    {
         countText.text = 00.ToString();
    }
    public void SetNecessaryItem(int _itemKey)
    {
        if (_itemKey != -1)
        {
            isNecessaryItem = true;
            necessaryItem = DatabaseManager.Instance.SelectItem(_itemKey);
            necessaryItemImage[1].sprite = DatabaseManager.Instance.SelectItem(_itemKey).singleSprite;
        }
    }
    public void SetNecessaryItemCount(int _itemCount)
    {
        countText.text = _itemCount.ToString();
    }
    public void SetNecessaryItemCountColor(bool _bool)
    {
        if(!_bool)
        {
            countText.color = new Color(255f, 0f, 0f);
        }
        else
        {
            countText.color = new Color(0f, 0f, 255f);
        }
    }
    public void SelectNecessaryItem()
    {
        if (isNecessaryItem)
        {
            UIManager.Instance.SelectRegisterNecessaryItem(necessaryItem, slotIndex);
        }
        else
        {
        }
    }
    public void SelectNecessaryItemInfo()
    {
        if (isNecessaryItem)
        {
            UIManager.Instance.SelectNecessaryItemInfo(necessaryItem);
        }
        else
        {
            Debug.Log("����ִ� ������");
        }
    }
}
