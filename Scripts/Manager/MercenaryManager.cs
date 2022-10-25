using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MercenaryManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> mercenarys = new List<GameObject>();

    [SerializeField]
    private GameObject characterPrefab = null;
    [SerializeField]
    private GameObject revialZone = null;
    public List<GameObject> Mercenarys{get { return mercenarys; }set { mercenarys = value; } }

    private void Start()
    {
        EquipMercenaryItem();
    }
    public void EquipMercenaryItem()
    {
        for(int i = 0; i < mercenarys.Count; i++)
        {

            mercenarys[i].GetComponent<EquipmentController>().ChangeEquipment(InventoryManager.Instance.AcquireItem(DatabaseManager.Instance.SelectItem(1003)));
            mercenarys[i].GetComponent<EquipmentController>().ChangeEquipment(InventoryManager.Instance.AcquireItem(DatabaseManager.Instance.SelectItem(2003)));
            mercenarys[i].GetComponent<EquipmentController>().ChangeEquipment(InventoryManager.Instance.AcquireItem(DatabaseManager.Instance.SelectItem(3003)));
            mercenarys[i].GetComponent<EquipmentController>().ChangeEquipment(InventoryManager.Instance.AcquireItem(DatabaseManager.Instance.SelectItem(4003)));
            mercenarys[i].GetComponent<EquipmentController>().ChangeEquipment(InventoryManager.Instance.AcquireItem(DatabaseManager.Instance.SelectItem(5003)));
            mercenarys[i].GetComponent<EquipmentController>().ChangeEquipment(InventoryManager.Instance.AcquireItem(DatabaseManager.Instance.SelectItem(6003)));
            mercenarys[i].GetComponent<EquipmentController>().ChangeEquipment(InventoryManager.Instance.AcquireItem(DatabaseManager.Instance.SelectItem(7003)));
            mercenarys[i].GetComponent<EquipmentController>().ChangeEquipment(InventoryManager.Instance.AcquireItem(DatabaseManager.Instance.SelectItem(8003)));
            for (int j = 0; j < mercenarys[i].GetComponent<EquipmentController>().EquipItems.Length; j++)
                mercenarys[i].GetComponent<EquipmentController>().EquipItems[j].equipCharNum = mercenarys[i].GetComponent<AllyStatus>().AllyNum;
        }
    }
}
