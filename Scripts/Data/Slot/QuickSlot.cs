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
    [SerializeField]
    private Button autoUseButton = null;
    [SerializeField]
    private bool isAutoUse = false;

    #region Property
    public bool IsAutoUse
    {
        get { return isAutoUse; }
        set { isAutoUse = value; }
    }
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
    private void Start()
    {
        ResetSlot();
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
    public void ResetSlot()
    {
        // 슬롯 리셋
        curItem = null;
        ItemImages[1].sprite = uiMask;
        itemCount.text = "00";
        isAutoUse = false;
        SetActiveAutoUseButton(false);
        EnableItemCount(false);
    }
    public void SetSlot()
    {
        // 슬롯 세팅
        itemImages[1].sprite = curItem.singleSprite;
        itemImages[1].rectTransform.sizeDelta = new Vector2(100f, 100f);

        ActiveItemCount();
    }
    private void ActiveItemCount()
    {
        // 소모품이나 쌓이는 아이템이면 count 텍스트를 활성화
        if (curItem.itemKey / 1000 < 9)
        {
            EnableItemCount(false);
        }
        else
        {
            SetActiveAutoUseButton(true);
            EnableItemCount(true);
            UpdateItemCount();
        }
    }
    public void UpdateQuickSlotItem()
    {
        UpdateItemCount();
        if (curItem.count <= 0)
        {
            ResetSlot();
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
    public void SetIsAutoUseButton()
    {
        isAutoUse = !isAutoUse;
        SetAutoUseButtonText();
    }
    public void SetAutoUseButtonText()
    {
        if (isAutoUse)
            autoUseButton.GetComponentInChildren<TextMeshProUGUI>().text = "사용중";
        else
            autoUseButton.GetComponentInChildren<TextMeshProUGUI>().text = "자동 사용";
    }
    public void SetActiveAutoUseButton(bool _bool)
    {
        autoUseButton.gameObject.SetActive(_bool);
    }
    public void UseItem()
    {
        if (isItemRegistered && !isCoolTime)
        {
            isCoolTime = true;
            coolTime = curItem.coolTime * 1f;
            UIManager.Instance.UseQuickSlotItem(curItem,index);
        }
    }
}
