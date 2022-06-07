using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
==============================
 * 최종수정일 : 2022-06-07
 * 작성자 : Inklie
 * 파일명 : UIManager.cs
==============================
*/
public class UIManager : SingletonManager<UIManager>
{


    [Header("EnemySpawner")]
    [SerializeField]
    private EnemySpawner enemySpawner = null;

    [Header("StageManager")]
    [SerializeField]
    private StageManager stageManager = null;

    [Header("Player")]
    [SerializeField]
    private PlayerStatus player = null;

    [Header("Boss")]
    [SerializeField]
    private EnemyStatus bossEnemy = null;

    [Header("UIImages")]
    [SerializeField]
    private GameObject[] UIImages = null; 


    [Header("StateImages & StateTexts")]
    [SerializeField]
    private Image[] playerStateImages = null;
    [SerializeField]
    private Image[] mercenaryStateImages = null;
    [SerializeField]
    private Image[] bossStateImages = null;
    [SerializeField]
    private Text[] playerTexts = null;
    [SerializeField]
    private Text[] mercenaryTexts = null;
    [SerializeField]
    private Text[] bossTexts = null;

    [Header("ProfileImages")]
    [SerializeField]
    private Image[] playerProfileImages = null;
    [SerializeField]
    private Image[] mercenaryAProfileImages = null;
    [SerializeField]
    private Image[] mercenaryBProfileImages = null;
    [SerializeField]
    private Image[] mercenaryCProfileImages = null;
    [SerializeField]
    private Image[] mercenaryDProfileImages = null;

    [Header("Default")]
    [SerializeField]
    private Sprite UIMask;

    [Header("ItemInfo")]
    [SerializeField]
    private GameObject itemInfo = null;
    private Text[] iteminfoText = null;

    [Header("Status")]
    [SerializeField]
    private Text[] statusTexts = null;
    [SerializeField]
    private Button[] statusButtons = null;  

    [Header("Buttons")]
    [SerializeField]
    private Button[] inventoryButtons = null;

    [Header("Equip")]
    [SerializeField]
    private Button[] equipCharactersBtn = null;
    [SerializeField]
    private Text equipmentNameText = null;

    private bool isItemSelect = false;
    private int selectNum = 0;
    private int selectInventoryIndex = 0;
    private Item selectItem = null;
    private EquipmentController selectCharacterEqipment = null;
    private CharacterStatus selectCharStatus = null;
    private List<EquipmentController> characterList = new List<EquipmentController>();
    private InventorySlot[] inventorySlots = null;
    private EquipmentSlot[] equipmentSlots = null;
    private CharacterStatus[] mercenary = null;

    private void Awake()
    {
        inventorySlots = UIImages[0].GetComponentsInChildren<InventorySlot>();
        equipmentSlots = UIImages[1].GetComponentsInChildren<EquipmentSlot>();
        iteminfoText = itemInfo.GetComponentsInChildren<Text>();
        mercenary = player.Mercenarys;
        selectCharacterEqipment = player.GetComponent<EquipmentController>();
        selectCharStatus = player.GetComponent<CharacterStatus>();

        characterList.Add(player.GetComponent<EquipmentController>());
        for(int i =0; i < mercenary.Length; i++)
            characterList.Add(mercenary[i].GetComponent<EquipmentController>());
    }
    private void Start()
    {
        ChangePlayerUIItemImage();
        ChangeMercenaryUIItemImage(0);
        ChangeMercenaryUIItemImage(1);
        ChangeMercenaryUIItemImage(2);
    }
    private void Update()
    {
        if (stageManager.IsStart == true)
            bossEnemy = enemySpawner.CurBoss.GetComponent<EnemyStatus>();

        if (bossEnemy != null)
        {
            BossUpdate();
        }
        UpdatePlayerProfile();
        UpdateMercenaryProfile();

        if (isItemSelect)
            UpdateItemInfo();

        ChangePlayerUIItemImage();
        ChangeMercenaryUIItemImage(0);
        ChangeMercenaryUIItemImage(1);
        ChangeMercenaryUIItemImage(2);
    }
    #region "보스 업데이트"
    private void BossUpdate()
    {
        bossTexts[0].text = bossEnemy.CurHp.ToString() + " / " + bossEnemy.MaxHp.ToString();
        bossTexts[1].text = bossEnemy.ObjectName.ToString();

        bossStateImages[0].fillAmount = bossEnemy.CurHp / bossEnemy.MaxHp;
    }
    #endregion

    #region "프로필 업데이트"
    private void UpdatePlayerProfile()
    {
        string[] infoText = new string[]
            {
            player.CurHp.ToString() + " / " + player.MaxHp.ToString(),
            player.CurMp.ToString() + " / " + player.MaxMp.ToString(),
            player.CurExp.ToString() + " / " + player.MaxExp.ToString(),
            "Lv. " + player.CurLevel.ToString(),
            player.ObjectName
        };
        float[] infoImage ={
            player.CurHp / player.MaxHp ,
            player.CurMp / player.MaxMp,
            (float)player.CurExp / player.MaxExp
        };
        for(int i = 0; i < 5; i++)
        {
            playerTexts[i].text = infoText[i];
        }
        for (int i = 0; i < 3; i++)
        {
            playerStateImages[i].fillAmount = infoImage[i];
        }
    }
    public void UpdateMercenaryProfile()
    {
        for (int i = 0; i < mercenary.Length; i++)
        {
            string[] infoText = {
            mercenary[i].CurHp.ToString() + " / " + mercenary[i].MaxHp.ToString(),
            mercenary[i].CurMp.ToString() + " / " + mercenary[i].MaxMp.ToString(),
            mercenary[i].CurExp.ToString() + " / " + mercenary[i].MaxExp.ToString(),
            "Lv. " + mercenary[i].CurLevel.ToString()};
            float[] infoImage = {
            mercenary[i].CurHp / mercenary[i].MaxHp ,
            mercenary[i].CurMp / mercenary[i].MaxMp,
            (float)mercenary[i].CurExp / mercenary[i].MaxExp
        };

            for(int j = 0; j < 4; j++)
            {
                mercenaryTexts[4 * i + j].text = infoText[j];
            }
            for (int j = 0; j < 3; j++)
            {
                mercenaryStateImages[3 * i + j].fillAmount = infoImage[j];
            }
        };
    }
    public void ChangePlayerUIItemImage()
    {
        // 플레이어 프로필 UI 변경하기
        // 머리
        if (characterList[0].CheckEquipItems[0])
        {
            playerProfileImages[0].sprite = characterList[0].EquipItems[0].spList[0];
        }
        else
            playerProfileImages[0].sprite = UIMask;

        // 얼굴장식
        if (characterList[0].CheckEquipItems[1])
            playerProfileImages[1].sprite = characterList[0].EquipItems[1].spList[0];
        else
            playerProfileImages[1].sprite = UIMask;
        // 위옷
        if (characterList[0].CheckEquipItems[2])
        {
            playerProfileImages[2].sprite = characterList[0].EquipItems[2].spList[0];
            playerProfileImages[3].sprite = characterList[0].EquipItems[2].spList[1];
            playerProfileImages[4].sprite = characterList[0].EquipItems[2].spList[2];
        }
        else
        {
            playerProfileImages[2].sprite = UIMask;
            playerProfileImages[3].sprite = UIMask;
            playerProfileImages[4].sprite = UIMask;
        }
        // 투구
        if (characterList[0].CheckEquipItems[4])
            playerProfileImages[5].sprite = characterList[0].EquipItems[4].spList[0];
        else
            playerProfileImages[5].sprite = UIMask;
        // 갑옷
        if (characterList[0].CheckEquipItems[5])
        {
            playerProfileImages[6].sprite = characterList[0].EquipItems[5].spList[0];
            playerProfileImages[7].sprite = characterList[0].EquipItems[5].spList[1];
            playerProfileImages[8].sprite = characterList[0].EquipItems[5].spList[2];
        }
        else
        {
            playerProfileImages[6].sprite = UIMask;
            playerProfileImages[7].sprite = UIMask;
            playerProfileImages[8].sprite = UIMask;
        }
    }
    public void ChangeMercenaryUIItemImage(int _index)
    {
        // 용병 프로필 UI 변경하기
        if (_index == 0)
        {
            // 머리
            if (characterList[_index + 1].CheckEquipItems[0])
                mercenaryAProfileImages[0].sprite = characterList[_index + 1].EquipItems[0].spList[0];
            else
                mercenaryAProfileImages[0].sprite = UIMask;
            // 얼굴장식
            if (characterList[_index + 1].CheckEquipItems[1])
                mercenaryAProfileImages[1].sprite = characterList[_index + 1].EquipItems[1].spList[0];
            else
                mercenaryAProfileImages[1].sprite = UIMask;
            // 위옷
            if (characterList[_index + 1].CheckEquipItems[2])
            {
                mercenaryAProfileImages[2].sprite = characterList[_index + 1].EquipItems[2].spList[0];
                mercenaryAProfileImages[3].sprite = characterList[_index + 1].EquipItems[2].spList[1];
                mercenaryAProfileImages[4].sprite = characterList[_index + 1].EquipItems[2].spList[2];
            }
            else
            {
                mercenaryAProfileImages[2].sprite = UIMask;
                mercenaryAProfileImages[3].sprite = UIMask;
                mercenaryAProfileImages[4].sprite = UIMask;
            }
            // 투구
            if (characterList[_index + 1].CheckEquipItems[4])
                mercenaryAProfileImages[5].sprite = characterList[_index + 1].EquipItems[4].spList[0];
            else
                mercenaryAProfileImages[5].sprite = UIMask;
            // 갑옷
            if (characterList[_index + 1].CheckEquipItems[5])
            {
                mercenaryAProfileImages[6].sprite = characterList[_index + 1].EquipItems[5].spList[0];
                mercenaryAProfileImages[7].sprite = characterList[_index + 1].EquipItems[5].spList[1];
                mercenaryAProfileImages[8].sprite = characterList[_index + 1].EquipItems[5].spList[2];
            }
            else
            {
                mercenaryAProfileImages[6].sprite = UIMask;
                mercenaryAProfileImages[7].sprite = UIMask;
                mercenaryAProfileImages[8].sprite = UIMask;
            }
        }
        else if (_index == 1)
        {
            // 머리
            if (characterList[_index + 1].CheckEquipItems[0])
                mercenaryBProfileImages[0].sprite = characterList[_index + 1].EquipItems[0].spList[0];
            else
                mercenaryBProfileImages[0].sprite = UIMask;
            // 얼굴장식
            if (characterList[_index + 1].CheckEquipItems[1])
                mercenaryBProfileImages[1].sprite = characterList[_index + 1].EquipItems[1].spList[0];
            else
                mercenaryBProfileImages[1].sprite = UIMask;
            // 위옷
            if (characterList[_index + 1].CheckEquipItems[2])
            {
                mercenaryBProfileImages[2].sprite = characterList[_index + 1].EquipItems[2].spList[0];
                mercenaryBProfileImages[3].sprite = characterList[_index + 1].EquipItems[2].spList[1];
                mercenaryBProfileImages[4].sprite = characterList[_index + 1].EquipItems[2].spList[2];
            }
            else
            {
                mercenaryBProfileImages[2].sprite = UIMask;
                mercenaryBProfileImages[3].sprite = UIMask;
                mercenaryBProfileImages[4].sprite = UIMask;
            }
            // 투구
            if (characterList[_index + 1].CheckEquipItems[4])
                mercenaryBProfileImages[5].sprite = characterList[_index + 1].EquipItems[4].spList[0];
            else
                mercenaryBProfileImages[5].sprite = UIMask;
            // 갑옷
            if (characterList[_index + 1].CheckEquipItems[5])
            {
                mercenaryBProfileImages[6].sprite = characterList[_index + 1].EquipItems[5].spList[0];
                mercenaryBProfileImages[7].sprite = characterList[_index + 1].EquipItems[5].spList[1];
                mercenaryBProfileImages[8].sprite = characterList[_index + 1].EquipItems[5].spList[2];
            }
            else
            {
                mercenaryBProfileImages[6].sprite = UIMask;
                mercenaryBProfileImages[7].sprite = UIMask;
                mercenaryBProfileImages[8].sprite = UIMask;
            }
        }
        if (_index == 2)
        {
            // 머리
            if (characterList[_index + 1].CheckEquipItems[0])
                mercenaryCProfileImages[0].sprite = characterList[_index + 1].EquipItems[0].spList[0];
            else
                mercenaryCProfileImages[0].sprite = UIMask;
            // 얼굴장식
            if (characterList[_index + 1].CheckEquipItems[1])
                mercenaryCProfileImages[1].sprite = characterList[_index + 1].EquipItems[1].spList[0];
            else
                mercenaryCProfileImages[1].sprite = UIMask;
            // 위옷
            if (characterList[_index + 1].CheckEquipItems[2])
            {
                mercenaryCProfileImages[2].sprite = characterList[_index + 1].EquipItems[2].spList[0];
                mercenaryCProfileImages[3].sprite = characterList[_index + 1].EquipItems[2].spList[1];
                mercenaryCProfileImages[4].sprite = characterList[_index + 1].EquipItems[2].spList[2];
            }
            else
            {
                mercenaryCProfileImages[2].sprite = UIMask;
                mercenaryCProfileImages[3].sprite = UIMask;
                mercenaryCProfileImages[4].sprite = UIMask;
            }
            // 투구
            if (characterList[_index + 1].CheckEquipItems[4])
                mercenaryCProfileImages[5].sprite = characterList[_index + 1].EquipItems[4].spList[0];
            else
                mercenaryCProfileImages[5].sprite = UIMask;
            // 갑옷
            if (characterList[_index + 1].CheckEquipItems[5])
            {
                mercenaryCProfileImages[6].sprite = characterList[_index + 1].EquipItems[5].spList[0];
                mercenaryCProfileImages[7].sprite = characterList[_index + 1].EquipItems[5].spList[1];
                mercenaryCProfileImages[8].sprite = characterList[_index + 1].EquipItems[5].spList[2];
            }
            else
            {
                mercenaryCProfileImages[6].sprite = UIMask;
                mercenaryCProfileImages[7].sprite = UIMask;
                mercenaryCProfileImages[8].sprite = UIMask;
            }
        }
        if (_index == 3)
        {
            // 머리
            if (characterList[_index + 1].CheckEquipItems[0])
                mercenaryDProfileImages[0].sprite = characterList[_index + 1].EquipItems[0].spList[0];
            else
                mercenaryDProfileImages[0].sprite = UIMask;
            // 얼굴장식
            if (characterList[_index + 1].CheckEquipItems[1])
                mercenaryDProfileImages[1].sprite = characterList[_index + 1].EquipItems[1].spList[0];
            else
                mercenaryDProfileImages[1].sprite = UIMask;
            // 위옷
            if (characterList[_index + 1].CheckEquipItems[2])
            {
                mercenaryDProfileImages[2].sprite = characterList[_index + 1].EquipItems[2].spList[0];
                mercenaryDProfileImages[3].sprite = characterList[_index + 1].EquipItems[2].spList[1];
                mercenaryDProfileImages[4].sprite = characterList[_index + 1].EquipItems[2].spList[2];
            }
            else
            {
                mercenaryDProfileImages[2].sprite = UIMask;
                mercenaryDProfileImages[3].sprite = UIMask;
                mercenaryDProfileImages[4].sprite = UIMask;
            }
            // 투구
            if (characterList[_index + 1].CheckEquipItems[4])
                mercenaryDProfileImages[5].sprite = characterList[_index + 1].EquipItems[4].spList[0];
            else
                mercenaryDProfileImages[5].sprite = UIMask;
            // 갑옷
            if (characterList[_index + 1].CheckEquipItems[5])
            {
                mercenaryDProfileImages[6].sprite = characterList[_index + 1].EquipItems[5].spList[0];
                mercenaryDProfileImages[7].sprite = characterList[_index + 1].EquipItems[5].spList[1];
                mercenaryDProfileImages[8].sprite = characterList[_index + 1].EquipItems[5].spList[2];
            }
            else
            {
                mercenaryDProfileImages[6].sprite = UIMask;
                mercenaryDProfileImages[7].sprite = UIMask;
                mercenaryDProfileImages[8].sprite = UIMask;
            }
        }
    }
    #endregion

    #region "인벤토리와 장비창"
    public void InventoryReset()
    {
        // 인벤토리 슬롯 리셋
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i].SlotReset();
        }
    }
    public void InventorySlotChange(int _index)
    {
        // 인벤토리 슬롯 바꾸기 
        InventoryReset();
        itemInfo.SetActive(false);

        if (_index == 0)
        {
            for (int i = 0; i < InventoryManager.Instance.InventroyWeaponItems.Count; i++)
            {
                InventoryManager.Instance.SortInventory(InventoryManager.Instance.InventroyWeaponItems);
                inventorySlots[i].CurItem = InventoryManager.Instance.InventroyWeaponItems[i];
                inventorySlots[i].IsItemStateChange = true;
                inventorySlots[i].SlotSetting();
                inventorySlots[i].EnableItemCount(false);
            }
        }
        if(_index == 1)
        {
            for (int i = 0; i < InventoryManager.Instance.InventroyEquipmentItems.Count; i++)
            {
                InventoryManager.Instance.SortInventory(InventoryManager.Instance.InventroyEquipmentItems);
                inventorySlots[i].CurItem = InventoryManager.Instance.InventroyEquipmentItems[i];
                inventorySlots[i].IsItemStateChange = true;
                inventorySlots[i].SlotSetting();
                inventorySlots[i].EnableItemCount(false);
            }
        }
        if(_index == 2)
        {
            for (int i = 0; i < InventoryManager.Instance.InventroyConsumableItems.Count; i++)
            {
                InventoryManager.Instance.SortInventory(InventoryManager.Instance.InventroyConsumableItems);
                inventorySlots[i].CurItem = InventoryManager.Instance.InventroyConsumableItems[i];
                inventorySlots[i].IsItemStateChange = true;
                inventorySlots[i].SlotSetting();
            }
        }
        if (_index == 3)
        {
            for (int i = 0; i < InventoryManager.Instance.InventroyMiscellaneousItems.Count; i++)
            {
                InventoryManager.Instance.SortInventory(InventoryManager.Instance.InventroyMiscellaneousItems);
                inventorySlots[i].CurItem = InventoryManager.Instance.InventroyMiscellaneousItems[i];
                inventorySlots[i].IsItemStateChange = true;
                inventorySlots[i].SlotSetting();

            }
        }
        if (_index == 4)
        {
            for (int i = 0; i < InventoryManager.Instance.InventroyDecorationItems.Count; i++)
            {
                InventoryManager.Instance.SortInventory(InventoryManager.Instance.InventroyDecorationItems);
                inventorySlots[i].CurItem = InventoryManager.Instance.InventroyDecorationItems[i];
                inventorySlots[i].IsItemStateChange = true;
                inventorySlots[i].SlotSetting();
                inventorySlots[i].EnableItemCount(false);
            }
        }
        selectInventoryIndex = _index;
    }
    public void Equip(int _character)
    {
        // 장착하기 버튼
        if (characterList[_character].CheckEquipItems[selectItem.itemType])
            TakeOff(_character, characterList[_character].EquipItems[selectItem.itemType]);
        selectItem.equipCharNum = _character;
        GetInventorySlot(selectItem).IsItemStateChange = true;
        characterList[_character].ChangeEquipment(selectItem);
        SetActiveEquipCharacterBox(false);
        SetActiveItemInfo(false);
        if (_character == 0)
            ChangePlayerUIItemImage();
        else
            ChangeMercenaryUIItemImage(_character - 1);
        ChangeEquipmentImage();
    }


    public void SetActiveEquipCharacterBox(bool _bool)
    {
        // 장착 캐릭터 선택하기 버튼 활성화
        equipCharactersBtn[0].gameObject.SetActive(_bool);
        for (int i = 0; i < mercenary.Length; i++)
            equipCharactersBtn[i + 1].gameObject.SetActive(_bool);
    } 
    public bool IsEquipingCharacter(EquipmentController _char,Item _equipItem)
    {
        // 현재 캐릭터가 해당 아이템의 부위를 착용중인지 체크
        if (_char.CheckEquipItems[_equipItem.itemType])
            return true;
        else
            return false;
    }
    public void TakeOffSelectItem()
    {
        // 선택한 아이템 해제
        TakeOff(selectItem.equipCharNum, selectItem);
        SetActiveItemInfo(false);
        ChangeEquipmentImage();
    }
    public void TakeOff(int _character,Item _item)
    {
        // 나중에 매개변수 빼고 변수로 쓰기 
        if (_item.isEquip)
        {
            GetInventorySlot(_item).IsItemStateChange = true;
            //마찬가지로 무기/ 방패 다시
            characterList[_character].TakeOffEquipment(_item);
            _item.isEquip = false;
            _item.equipCharNum = -1;

            if (_character == 0)
                ChangePlayerUIItemImage();
            else
                ChangeMercenaryUIItemImage(_character - 1);
        }
        else
            Debug.Log("착용중이 아님");
    }
    public void UseSelectItem()
    {
        InventoryManager.Instance.UseItem(player, selectItem);
        SetActiveItemInfo(false);
        if (GetInventorySlot(selectItem).CurItem.count > 0)
            GetInventorySlot(selectItem).UpdateItemCount();
        else
        {
            InventorySlotChange(2);
        }
    }
    public void DiscardSelectItem()
    {
        InventoryManager.Instance.DiscardItem(selectItem);
        InventorySlotChange(selectInventoryIndex);
    }
    public void SelectSlotItem(Item _item) 
    {
        // 슬롯에 선택한 아이템 
        selectItem = _item;
        isItemSelect = true;
    }
    public InventorySlot GetInventorySlot(Item _item)
    {
        // 슬롯 선택 하기
        InventorySlot slot = null;
        for(int i =0; i< inventorySlots.Length; i++)
        {
            if(inventorySlots[i].CurItem == _item)
                slot = inventorySlots[i];
        }
        return slot;
    }
    public void InitEquipment()
    {
        // 장비창 리셋
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            equipmentSlots[i].CurItem = null;
            equipmentSlots[i].ItemImages[1].sprite = UIMask;
        }
    }
    public void ChangeEquipmentImage()
    {
        // 장비창 이미지 바꾸기
        for (int i = 0; i < selectCharacterEqipment.EquipItems.Length; i++)
        {
            if (selectCharacterEqipment.CheckEquipItems[i])
            {
                equipmentSlots[i].ItemImages[1].sprite = selectCharacterEqipment.EquipItems[i].singleSprite;
                equipmentSlots[i].SlotSetting(selectCharacterEqipment.EquipItems[i]);
            }
            else
            {
                TakeOffEquipmentImage(i);
            }
        }

    }
    public void TakeOffEquipmentImage(int _index)
    {
        // 장비 해제시 장비창 이미지 변경
        equipmentSlots[_index].ItemImages[1].sprite = UIMask;
        equipmentSlots[_index].InitImageSize();
    }
    public void InventoryButtonReset()
    {
        for(int i = 0; i < inventoryButtons.Length;i++)
        {
            inventoryButtons[i].gameObject.SetActive(false);
        }
    }
    public void UpdateItemInfo()
    {
        // 아이템 정보창 업데이트
        isItemSelect = false;
        InventoryButtonReset();
        SetActiveItemInfo(true);
        inventoryButtons[3].gameObject.SetActive(true)  ;
        if (selectItem.itemType == (int)ItemType.Consumables)
        {
            inventoryButtons[2].gameObject.SetActive(true);
        }
        else
        {
            if (selectItem.equipCharNum != -1)
            {
                inventoryButtons[1].gameObject.SetActive(true);
            }
            else
            {
                inventoryButtons[0].gameObject.SetActive(true);
            }
        }    
            iteminfoText[0].text = selectItem.itemName;
            iteminfoText[1].text = KeyToItemType(selectItem.itemKey);
        switch (selectItem.itemKey / 1000)
        {
            case 0:
                iteminfoText[2].text = "This is Hair";
                break;
            case 1:
                iteminfoText[2].text = "This is FaceHair";
                break;
            case 2:
                iteminfoText[2].text = "DefensivePower : " + selectItem.defensivePower;
                break;
            case 3:
                iteminfoText[2].text = "DefensivePower : " + selectItem.defensivePower;
                break;
            case 4:
                iteminfoText[2].text = "DefensivePower : " + selectItem.defensivePower;
                break;
            case 5:
                iteminfoText[2].text = "DefensivePower : " + selectItem.defensivePower;
                break;
            case 6:
                iteminfoText[2].text = "DefensivePower : " + selectItem.defensivePower;
                break;
            case 7:
                iteminfoText[2].text =
                    "PysicalDamage : " + selectItem.physicalDamage + "\n" +
                    "magicalDamage : " + selectItem.magicalDamage + "\n" +
                    "AttackRange : " + ((Weapon)selectItem).atkRange + "\n" +
                    "AttackDistance : " + ((Weapon)selectItem).atkDistance + "\n" +
                    "WeaponType : " + ((Weapon)selectItem).weaponType;
                break;
            case 8:
                iteminfoText[2].text =
                    "PysicalDamage : " + selectItem.physicalDamage + "\n" +
                    "magicalDamage : " + selectItem.magicalDamage + "\n" +
                    "AttackRange : " + ((Weapon)selectItem).atkRange + "\n" +
                    "AttackDistance : " + ((Weapon)selectItem).atkDistance + "\n" +
                    "WeaponType : " + ((Weapon)selectItem).weaponType +
                    "DefensivePower : " + selectItem.defensivePower;
                break;
            case 9:
                iteminfoText[2].text =
                    "PysicalDamage : " + selectItem.physicalDamage + "\n" +
                    "magicalDamage : " + selectItem.magicalDamage + "\n" +
                    "AttackRange : " + ((Weapon)selectItem).atkRange + "\n" +
                    "AttackDistance : " + ((Weapon)selectItem).atkDistance + "\n" +
                    "WeaponType : " + ((Weapon)selectItem).weaponType;
                break;
            case 10:
                iteminfoText[2].text =
                    "PysicalDamage : " + selectItem.physicalDamage + "\n" +
                    "magicalDamage : " + selectItem.magicalDamage + "\n" +
                    "AttackRange : " + ((Weapon)selectItem).atkRange + "\n" +
                    "AttackDistance : " + ((Weapon)selectItem).atkDistance + "\n" +
                    "WeaponType : " + ((Weapon)selectItem).weaponType;
                break;
            case 11:
                iteminfoText[2].text =
                    "Value : " + selectItem.value + "\n";
                break;
            case 12:
                iteminfoText[2].text =
                    "PysicalDamage : " + selectItem.physicalDamage + "\n" +
                    "magicalDamage : " + selectItem.magicalDamage + "\n" +
                    "AttackRange : " + ((Weapon)selectItem).atkRange + "\n" +
                    "AttackDistance : " + ((Weapon)selectItem).atkDistance + "\n" +
                    "WeaponType : " + ((Weapon)selectItem).weaponType;
                break;
        }
    }
    public string KeyToItemType(int _key)
    {
        // 키를 아이템 타입으로 변경
        string _itemtype = null;
        switch(_key/1000)
        {
            case 0:
                _itemtype = "Hair";
                break;
            case 1:
                _itemtype = "FaceHair";
                break;
            case 2:
                _itemtype = "Cloth";
                break;
            case 3:
                _itemtype = "Pant";
                break;
            case 4:
                _itemtype = "Helmet";
                break;
            case 5:
                _itemtype = "Armor";
                break;
            case 6:
                _itemtype = "Back";
                break;
            case 7:
                _itemtype = "Sword";
                break;
            case 8:
                _itemtype = "Shield";
                break;
            case 9:
                _itemtype = "Bow";
                break;
            case 10:
                _itemtype = "Wand";
                break;
        }
        return _itemtype;
    }
    public void SetActiveItemInfo(bool _bool)
    {
        // 아이템 정보창 활성화 여부
        itemInfo.SetActive(_bool);
    }
    public void UpdateEquipmentName()
    {
        // 장비창 캐릭터 이름 변경
        equipmentNameText.text = selectCharStatus.ObjectName.ToString();
    }
    public void SelectCharacterInEquipment(bool _isUp)
    {
        // 장비창에서 캐릭터 선택
        InitEquipment();
        if (_isUp)
        {
            selectNum++;
            if (selectNum == mercenary.Length + 1)
                selectNum = 0;
        }
        else
        {
            selectNum--;
            if (selectNum < 0)
                selectNum = mercenary.Length;
        }
        selectCharacterEqipment = characterList[selectNum];
        selectCharStatus = characterList[selectNum].GetComponent<CharacterStatus>();
        UpdateEquipmentName();
        ChangeEquipmentImage();
    }

    #endregion

    #region "상태창"
    public void UpdateStatusText()
    {
        // 상태 텍스트 업데이트
        string[] status = {
            "HP : " + selectCharStatus.MaxHp.ToString(),
            "MP : " + selectCharStatus.MaxMp.ToString(),
            "Physical Damage : " + selectCharStatus.PhysicalDamage.ToString(),
            "Magical Damage : " + selectCharStatus.MagicalDamage.ToString(),
            "Defensive : " + selectCharStatus.DefensivePower.ToString(),
            "Speed : " + selectCharStatus.Speed.ToString(),
            "Attack Speed : " + selectCharStatus.AtkSpeed.ToString(),
            "Drop Probability : " + selectCharStatus.DropProbability.ToString(),
            "ItemRarity : " + selectCharStatus.ItemRarity.ToString(),
            selectCharStatus.ObjectName.ToString(),
            "Str : " + selectCharStatus.Str.ToString(),
            "Dex : " + selectCharStatus.Dex.ToString(),
            "Int : " + selectCharStatus.Wiz.ToString(),
            "Luck : " + selectCharStatus.Luck.ToString(),
            "Point : " + selectCharStatus.StatusPoint.ToString(),
            "Level : " + selectCharStatus.CurLevel.ToString()
        };

        for(int i = 0; i < statusTexts.Length; i++)
        {
            statusTexts[i].text = status[i];
        }
        if(selectCharStatus.StatusPoint > 0)
        {
            for (int i = 0; i < statusButtons.Length; i++)
                statusButtons[i].gameObject.SetActive(true);
        }
        else
        {
            for (int i = 0; i < statusButtons.Length; i++)
                statusButtons[i].gameObject.SetActive(false);
        }
    }
    public void SelectCharacterInStatus(bool _isUp)
    {
        // 스테이터스 창 캐릭터 선택
        if (_isUp)
        {
            selectNum++;
            if (selectNum == mercenary.Length + 1)
                selectNum = 0;
        }
        else
        {
            selectNum--;
            if (selectNum < 0)
                selectNum = mercenary.Length;
        }
        selectCharacterEqipment = characterList[selectNum];
        UpdateStatusText();
    }
    public void StatusUp(int _index)
    {
        // 스텟 업
        selectCharacterEqipment.GetComponent<CharacterStatus>().UpStatus(_index);
        UpdateStatusText();
    }
    #endregion
    public void UIActiveButton(int _index)
    {
        // UI 활성화 
        UIImages[_index].SetActive(true);
        if(_index == 1)
        {
            selectCharacterEqipment = characterList[selectNum];
            ChangeEquipmentImage();
        }
    }

    public void UIDeactiveButton(int _index)
    {
        // UI 비활성화
        UIImages[_index].SetActive(false);
        if (_index == 2)
        {
            SetActiveEquipCharacterBox(false);
        }
    }
}
