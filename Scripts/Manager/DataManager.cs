using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using UnityEngine.Networking;
using System.IO;
/*
==============================
 * 최종수정일 : 2022-06-05
 * 작성자 : Inklie
 * 파일명 : DataManager.cs
==============================
*/

public class DataManager : MonoBehaviour
{
    private string path = null;

    [SerializeField]
    private PlayerStatus playerStatus = null ;
    private void Start()
    {
        path = Path.Combine(Application.dataPath, "database.json");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F8))
            JsonSave();
        else if(Input.GetKeyUp(KeyCode.F9))
            JsonLoad();
    }
    public void JsonLoad()
    {
        // 데이터 로드
        Player playerData = new Player();
        if (!File.Exists(path))
        {
            Debug.Log("처음 로드");
            playerStatus.ObjectName = "플레이어";
            playerStatus.Str = 5;
            playerStatus.Dex = 5;
            playerStatus.Wiz = 5;
            playerStatus.Luck = 5;
            playerStatus.StatusPoint = 5;
            playerStatus.CurExp = 0;
            playerStatus.CurLevel = 1;
            playerStatus.Money = 0;
            playerStatus.Stage = 1;
            
            JsonSave();
        }
        else
        {
            Debug.Log(" 있는 로드");
            string loadJson = File.ReadAllText(path);
            playerData = JsonUtility.FromJson<Player>(loadJson);
            if (playerData != null)
            {
                playerStatus.ObjectName = playerData.objectName;
                playerStatus.Str = playerData.str;
                playerStatus.Dex = playerData.dex;
                playerStatus.Wiz = playerData.wiz;
                playerStatus.Luck = playerData.luck;
                playerStatus.StatusPoint = playerData.statusPoint;
                playerStatus.CurExp = playerData.exp;
                playerStatus.CurLevel = playerData.level;
                playerStatus.Money = playerData.money;
                playerStatus.Stage = playerData.stage;
                playerStatus.CheckEquipItems = playerData.checkEquipItems;
                for (int i = 0; i < playerData.decoItems.Count; i++)
                {
                    InventoryManager.Instance.AddItem(InventoryManager.Instance.InventroyDecorationItems,playerData.decoItems[i]);
                }
                for (int i = 0; i < playerData.weaponItems.Count; i++)
                {
                    InventoryManager.Instance.AddItem(InventoryManager.Instance.InventroyWeaponItems, playerData.weaponItems[i]);
                }
                for (int i = 0; i < playerData.equipmentItems.Count; i++)
                {
                    InventoryManager.Instance.AddItem(InventoryManager.Instance.InventroyEquipmentItems, playerData.equipmentItems[i]);
                }
                for (int i = 0; i < playerData.consumablesItems.Count; i++)
                {
                    InventoryManager.Instance.AddItem(InventoryManager.Instance.InventroyConsumableItems, playerData.consumablesItems[i]);
                }
                for (int i = 0; i < playerData.miscellaneousItems.Count; i++)
                {
                    InventoryManager.Instance.AddItem(InventoryManager.Instance.InventroyMiscellaneousItems, playerData.miscellaneousItems[i]);
                }
                for (int i = 0; i < 2; i++)
                {
                    if (playerData.checkEquipItems[i])
                        playerStatus.EquipmentController.ChangeEquipment(playerData.equipedItems[i]);
                    
                    else
                        playerStatus.EquipmentController.RemoveEquipment(i);
                }
                for (int i = 2; i < 7; i++)
                {
                    if (playerData.checkEquipItems[i])
                        playerStatus.EquipmentController.ChangeEquipment(playerData.equipedItems[i]);
                    else
                        playerStatus.EquipmentController.RemoveEquipment(i);
                }
                for (int i = 7; i < 9; i++)
                {
                    if (playerData.checkEquipItems[i])
                        playerStatus.EquipmentController.ChangeEquipment(playerData.equipedItems[i]);
                    else
                        playerStatus.EquipmentController.RemoveEquipment(i);
                }
                for (int j = 0; j < playerData.mercenaries.Length; j++)
                {
                    playerStatus.Mercenarys[j].ObjectName = playerData.mercenaries[j].objectName;
                    playerStatus.Mercenarys[j].Str = playerData.mercenaries[j].str;
                    playerStatus.Mercenarys[j].Dex = playerData.mercenaries[j].dex;
                    playerStatus.Mercenarys[j].Wiz = playerData.mercenaries[j].wiz;
                    playerStatus.Mercenarys[j].Luck = playerData.mercenaries[j].luck;
                    playerStatus.Mercenarys[j].StatusPoint = playerData.mercenaries[j].statusPoint;
                    playerStatus.Mercenarys[j].CurExp = playerData.mercenaries[j].exp;
                    playerStatus.Mercenarys[j].CurLevel = playerData.mercenaries[j].level;
                    for (int i = 0; i < 2; i++)
                    {
                        if (playerData.mercenaries[j].checkEquipItems[i])
                            playerStatus.Mercenarys[j].EquipmentController.ChangeEquipment(playerData.mercenaries[j].equipedItems[i]);
                    }
                    for (int i = 2; i < 7; i++)
                    {
                        if (playerData.mercenaries[j].checkEquipItems[i])
                            playerStatus.Mercenarys[j].EquipmentController.ChangeEquipment(playerData.mercenaries[j].equipedItems[i]);
                    }
                    for (int i = 7; i < 9; i++)
                    {
                        if (playerData.mercenaries[j].checkEquipItems[i])
                            playerStatus.Mercenarys[j].EquipmentController.ChangeEquipment(playerData.mercenaries[j].equipedItems[i]);
                    }
                }
            }
        }
    }

    public void JsonSave()
    {
        // 데이터 세이브
        Debug.Log("Save");
        Player playerData = new Player();

        playerData.objectName = playerStatus.ObjectName;
        playerData.str = playerStatus.Str;
        playerData.dex = playerStatus.Dex;
        playerData.wiz = playerStatus.Wiz;
        playerData.luck = playerStatus.Luck;
        playerData.statusPoint = playerStatus.StatusPoint;
        playerData.exp = playerStatus.CurExp;
        playerData.level = playerStatus.CurLevel;
        playerData.money = playerStatus.Money; ;
        playerData.stage = playerStatus.Stage;
        playerData.checkEquipItems = playerStatus.CheckEquipItems;
        AddItemList(playerData.decoItems, InventoryManager.Instance.InventroyDecorationItems);
        AddItemList(playerData.weaponItems, InventoryManager.Instance.InventroyWeaponItems);
        AddItemList(playerData.equipmentItems, InventoryManager.Instance.InventroyEquipmentItems);
        AddItemList(playerData.consumablesItems, InventoryManager.Instance.InventroyConsumableItems);
        AddItemList(playerData.miscellaneousItems, InventoryManager.Instance.InventroyMiscellaneousItems);

        for (int i = 0; i < playerData.equipedItems.Length; i++)
        {
            if(playerData.checkEquipItems[i])
                playerData.equipedItems[i] = playerStatus.EquipmentController.EquipItems[i];
        
        }
        playerData.mercenaries = new Character[playerStatus.Mercenarys.Length];
        for (int i = 0; i < playerStatus.Mercenarys.Length; i++)
        {
            Character character = new Character();
            character.objectName = playerStatus.Mercenarys[i].ObjectName;
            character.str = playerStatus.Mercenarys[i].Str;
            character.dex = playerStatus.Mercenarys[i].Dex;
            character.wiz = playerStatus.Mercenarys[i].Wiz;
            character.luck = playerStatus.Mercenarys[i].Luck;
            character.statusPoint = playerStatus.Mercenarys[i].StatusPoint;
            character.exp = playerStatus.Mercenarys[i].CurExp;
            character.level = playerStatus.Mercenarys[i].CurLevel;
            character.checkEquipItems = playerStatus.Mercenarys[i].CheckEquipItems;
            for (int j = 0; j < playerData.equipedItems.Length; j++)
            {
                if(character.checkEquipItems[j])
                    character.equipedItems[j] = playerStatus.Mercenarys[i].EquipmentController.EquipItems[j];
                
            }
            playerData.mercenaries[i] = character;
        }

        string json = JsonUtility.ToJson(playerData,true);

        File.WriteAllText(path, json);
    }
    public void AddItemList(List<Item> dataItem, List<Item> Inventory)
    {
        // 아이템 리스트 추가
        for (int i = 0; i < Inventory.Count; i++)
        {
            dataItem.Add(Inventory[i]);
        }
    }
}

