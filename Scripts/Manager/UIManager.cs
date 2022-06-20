using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
==============================
 * ���������� : 2022-06-10
 * �ۼ��� : Inklie
 * ���ϸ� : UIManager.cs
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

    [Header("MercenaryManager")]
    [SerializeField]
    private MercenaryManager mercenaryManager = null;

    [Header("Player")]
    [SerializeField]
    private PlayerStatus player = null;

    [Header("Boss")]
    [SerializeField]
    private BossEnemyStatus bossEnemy = null;

    [Header("Altar")]
    [SerializeField]
    private AltarStatus altar = null;

    private List<GameObject> mercenary = new List<GameObject>();
    [SerializeField]
    private List<EquipmentController> characterList = new List<EquipmentController>();

    [SerializeField]
    private InventoryPanelController inventoryPanelController = null;    
    [SerializeField]
    private ProfilePanelController profilePanelController = null;
    [SerializeField]
    private StatusPanelController statusPanelController = null;
    [SerializeField]
    private AltarInfoPanelController altarInfoPanelController = null;

    private void Awake()
    {
        characterList.Add(player.GetComponent<EquipmentController>());
    }
    private void Start()
    {
        ChangePlayerUIItemImage();
        UpdatePlayerProfile();
        for(int i = 0; i < mercenary.Count; i++)
        {
            if(mercenary[i] != null)
            {
                ChangeMercenaryUIItemImage(i);
                UpdateMercenaryProfile(i);
                AddMercenaryEC(mercenary[i]);
            }
        }
    }

    private void Update()
    {
        if (bossEnemy != null)
        {
            profilePanelController.BossUpdate(bossEnemy);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            if(mercenaryManager.Mercenarys.Count < 4)
                mercenaryManager.AddNewMercenary();
        }
    }
    public void AddMercenary(GameObject _mercenary)
    {
        mercenary.Add(_mercenary);
        AddMercenaryEC(_mercenary);
    }
    public void AddMercenaryEC(GameObject _mercenary)
    {
        characterList.Add(_mercenary.GetComponent<EquipmentController>());
    }
    public void SetBossEnemy()
    {
        bossEnemy = enemySpawner.CurBoss.GetComponent<BossEnemyStatus>();
    }
    public void ChangePlayerUIItemImage()
    {
        profilePanelController.ChangePlayerUIItemImage(characterList);
    }
    public void ChangeMercenaryUIItemImage(int _index)
    {
        profilePanelController.ChangeMercenaryUIItemImage(characterList, _index);
    }
    public void UpdatePlayerProfile()
    {
        profilePanelController.UpdatePlayerProfile(player);
    }
    public void UpdateMercenaryProfile(int _index)
    {
        profilePanelController.UpdateMercenaryProfile(mercenary[_index - 1], mercenary.Count);
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
    public void SetActiveCharactersProfile(int _index, bool _bool)
    {
        profilePanelController.Profiles[_index].SetActive(_bool);
    }
    public void TakeOffSelectItemBtn()
    {
        // ������ ������ ����
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
        // ���â���� ĳ���� ����
        inventoryPanelController.SelectCharacterInEquipment(characterList,_isUp);
    }
    public void SetActiveEquipCharacterBox(bool _bool)
    {
        inventoryPanelController.SetActiveEquipCharacterBox(_bool);
    }
    public int GetMercenaryNum()
    {
        return mercenary.Count;
    }
    public void SelectSlotItem(Item _item)
    {
        // ���Կ� ������ ������ 
        inventoryPanelController.SelectSlotItem(_item);
    }
    public void SetActiveItemInfo(bool _bool)
    {
        // ������ ����â Ȱ��ȭ ����
        inventoryPanelController.SetActiveItemInfo(_bool);
    }
    public void SetActiveCheckDiscard(bool _bool)
    {
        // ������ ����â Ȱ��ȭ ����
        inventoryPanelController.SetActiveCheckDiscard(_bool);
    }
    public void SetActiveCheckDiscardAmount(bool _bool)
    {
        // ������ ����â Ȱ��ȭ ����
        inventoryPanelController.SetActiveCheckDiscardAmount(_bool);
    }
    public void SelectCharacterInStatus(bool _isUp)
    {
        statusPanelController.SelectCharacterInStatus(characterList,_isUp);
    }
    public void DiscardSelectAmountItem()
    {
        inventoryPanelController.DiscardSelectAmountItem();
    }
    public void UpdateStatus()
    {
        statusPanelController.UpdateStatusText();
    }
    public void UpdateAltarInfo()
    {
        altarInfoPanelController.UpdateAltarInfo();
    }
    public void UpgradeAltar(int _index)
    {
        altarInfoPanelController.UpgradeAltarStatus(_index);
    }
    public void StatusUp(int _index)
    {
        // ���� ��
        statusPanelController.StatusUp(_index);
    }

    public void ActiveUIBtn(int _index)
    {
        // UI Ȱ��ȭ 
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
        else if(_index == 2)
        {
            altarInfoPanelController.SetAltar(altar);
            altarInfoPanelController.ActiveAltarInfo();
        }
    }

    public void DeactiveUIBtn(int _index)
    {
        // UI ��Ȱ��ȭ
        if (_index == 0)
        {
            inventoryPanelController.DeactiveInventory();
        }
        else if (_index == 1)
        {
            statusPanelController.DeactiveStatus();
        }
        else if (_index == 2)
        {
            altarInfoPanelController.DeactiveAltarInfo();
        }
    }
}