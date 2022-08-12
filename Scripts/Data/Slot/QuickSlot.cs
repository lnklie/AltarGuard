using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class QuickSlot : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI itemCount = null;
    [SerializeField]
    private Image[] itemImages = null;
    [SerializeField]
    private Item curItem = null;
    [SerializeField]
    private bool isItemRegistered = false;
    [SerializeField]
    private Sprite uiMask = null;
    [SerializeField]
    private int index = 0;
    [SerializeField]
    private bool isCoolTime = false;
    [SerializeField]
    private float coolTime = 0f;
    #region Property
    public Image[] ItemImages
    {
        get { return itemImages; }
        set { itemImages = value; }
    }
    public Item CurItem
    {
        get { return curItem; }
        set { curItem = value; }
    }
    public bool IsItemRegistered
    {
        get { return isItemRegistered; }
        set { isItemRegistered = value; }
    }
    #endregion
    private void Awake()
    {
        itemImages = GetComponentsInChildren<Image>();
        itemCount = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Update()
    {
        
        if(isItemRegistered && isCoolTime)
        {
            coolTime -= Time.deltaTime;
            itemImages[2].fillAmount = coolTime / curItem.coolTime;
            if (coolTime <= 0f)
            {
                coolTime = 0f;
                isCoolTime = false;
            }
        }
    }
    public void SlotReset()
    {
        // ���� ����
        curItem = null;
        ItemImages[1].sprite = uiMask;
        itemCount.text = "00";
        EnableItemCount(false);
    }
    public void SlotSetting()
    {
        // ���� ����
        itemImages[1].sprite = curItem.singleSprite;
        itemImages[1].rectTransform.sizeDelta = new Vector2(100f, 100f);

        ActiveItemCount();
    }
    private void ActiveItemCount()
    {
        // �Ҹ�ǰ�̳� ���̴� �������̸� count �ؽ�Ʈ�� Ȱ��ȭ
        if (curItem.itemKey / 1000 < 9)
        {
            EnableItemCount(false);
        }
        else
        {
            EnableItemCount(true);
            UpdateItemCount();
        }
    }
    public void UpdateQuickSlotItem()
    {
        UpdateItemCount();
        if (curItem.count <= 0)
        {
            SlotReset();
            isItemRegistered = false;
        }
        else
            UpdateItemCount();
    }
    public void UpdateItemCount()
    {
        itemCount.text = curItem.count.ToString();
    }
    public void EnableItemCount(bool _bool)
    {
        itemCount.enabled = _bool;
    }
    public void UseItem()
    {
        if (isItemRegistered && !isCoolTime)
        {
            Debug.Log("�ɳ�");
            isCoolTime = true;
            coolTime = curItem.coolTime * 1f;
            UIManager.Instance.UseQuickSlotItem(curItem,index);
        }
    }
}
