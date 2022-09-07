using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class QuickSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemCount = null;
    [SerializeField] private TextMeshProUGUI checkEquip = null;
    [SerializeField] private Image itemImage = null;
    [SerializeField] private Image coolTimeImage = null;
    [SerializeField] private Item curItem = null;
    [SerializeField] private bool isItemRegistered = false;
    [SerializeField] private Sprite uiMask = null;
    [SerializeField] private int index = 0;

    [SerializeField] private Button autoUseButton = null;
    [SerializeField] private bool isAutoUse = false;

    #region Property
    public bool IsAutoUse { get { return isAutoUse; } set { isAutoUse = value; } }
    public Item CurItem { get { return curItem; } set { curItem = value; } }
    public bool IsItemRegistered { get { return isItemRegistered; } set { isItemRegistered = value; } }
    #endregion
    private void Awake()
    {
        itemCount = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Start()
    {
        ResetSlot();
    }
    private void Update()
    {
        
        if(isItemRegistered && CurItem.isCoolTime)
        {
            CurItem.coolTime -= Time.deltaTime;
            coolTimeImage.fillAmount = CurItem.coolTime / curItem.maxCoolTime;
            if (CurItem.coolTime <= 0f)
            {
                CurItem.coolTime = 0f;
                CurItem.isCoolTime = false;
            }
        }
    }
    public void ResetSlot()
    {
        // 슬롯 리셋
        curItem = null;
        itemImage.sprite = uiMask;
        itemCount.text = "00";
        isAutoUse = false;
        checkEquip.gameObject.SetActive(false);
        SetActiveAutoUseButton(false);
        EnableItemCount(false);
    }
    public void SetSlot()
    {
        // 슬롯 세팅
        itemImage.sprite = curItem.singleSprite;
        itemImage.rectTransform.sizeDelta = new Vector2(100f, 100f);
        if (curItem.isEquip)
            checkEquip.gameObject.SetActive(true);
        else
            checkEquip.gameObject.SetActive(false);
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
        if (isItemRegistered && !CurItem.isCoolTime)
        {
            CurItem.isCoolTime = true;
            CurItem.coolTime = curItem.maxCoolTime * 1f;
            UIManager.Instance.UseQuickSlotItem(curItem,index);
        }
    }
}
