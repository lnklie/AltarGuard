using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
==============================
 * 최종수정일 : 2022-06-08
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


    private CharacterStatus[] mercenary = null;
    private List<EquipmentController> characterList = new List<EquipmentController>();

    [SerializeField]
    private InventoryPanelController inventoryPanelController = null;    
    [SerializeField]
    private ProfilePanelController profilePanelController = null;
    [SerializeField]
    private StatusPanelController statusPanelController = null;

    private void Awake()
    {
        mercenary = player.Mercenarys;

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
            profilePanelController.BossUpdate(bossEnemy);
        }
        profilePanelController.UpdatePlayerProfile(player);
        profilePanelController.UpdateMercenaryProfile(mercenary);

        
        ChangeMercenaryUIItemImage(0);
        ChangeMercenaryUIItemImage(1);
        ChangeMercenaryUIItemImage(2);
    }

    public void ChangePlayerUIItemImage()
    {
        profilePanelController.ChangePlayerUIItemImage(characterList);
    }
    public void ChangeMercenaryUIItemImage(int _index)
    {
        profilePanelController.ChangeMercenaryUIItemImage(characterList,_index);
    }
    public void InventorySlotChange(int _index)
    {
        inventoryPanelController.InventorySlotChange(_index);
    }
    public void EquipBtn(int _character)
    {
        inventoryPanelController.Equip(characterList, _character);
    }
    public void UpdateEquipmentName()
    {
        inventoryPanelController.UpdateEquipmentName();
    }
    public void TakeOffSelectItemBtn()
    {
        // 선택한 아이템 해제
        inventoryPanelController.TakeOff(characterList);
    }
    public void UseSelectItemBtn()
    {
        inventoryPanelController.UseSelectItem(player);
    }
    public void DiscardSelectItemBtn()
    {
        inventoryPanelController.DiscardSelectItem();
    }
    public void SelectCharacterInEquipmentBtn(bool _isUp)
    {
        // 장비창에서 캐릭터 선택
        inventoryPanelController.SelectCharacterInEquipment(characterList,_isUp);
    }
    public void SetActiveEquipCharacterBox(bool _bool)
    {
        inventoryPanelController.SetActiveEquipCharacterBox(_bool);
    }
    public int GetMercenaryNum()
    {
        return mercenary.Length;
    }
    public void SelectSlotItem(Item _item)
    {
        // 슬롯에 선택한 아이템 
        inventoryPanelController.SelectSlotItem(_item);
    }
    public void SetActiveItemInfo(bool _bool)
    {
        // 아이템 정보창 활성화 여부
        inventoryPanelController.SetActiveItemInfo(_bool);
    }
    public void SelectCharacterInStatus(bool _isUp)
    {
        statusPanelController.SelectCharacterInStatus(characterList,_isUp);
    }

    public void UpdateStatus()
    {
        statusPanelController.UpdateStatusText();
    }
    public void StatusUp(int _index)
    {
        // 스텟 업
        statusPanelController.StatusUp(_index);
    }

    public void ActiveUIBtn(int _index)
    {
        // UI 활성화 
        if(_index == 0)
        {
            inventoryPanelController.SetPlayer(player);
            inventoryPanelController.ActiveInventory();
        }
        else if(_index == 1)
        {
            statusPanelController.SetPlayer(player);
            statusPanelController.ActiveStatus();
        }
    }

    public void DeactiveUIBtn(int _index)
    {
        // UI 비활성화
        if (_index == 0)
        {
            inventoryPanelController.DeactiveInventory();
        }
        else if (_index == 1)
        {
            statusPanelController.DeactiveStatus();
        }
    }
}