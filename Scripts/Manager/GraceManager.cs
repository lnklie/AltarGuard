using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraceManager : MonoBehaviour
{
    [SerializeField]
    private List<Grace> graceList = new List<Grace>();
    public List<Grace> GraceList
    {
        get { return graceList; }
    }
    [SerializeField]
    private PlayerStatus playerStatus = null;
    [SerializeField]
    private MercenaryManager mercenaryManager = null;

    private void Update()
    {
        if (playerStatus.EquipmentController.IsChangeItem)
            SetGraceAbility();
    }
    public void AquireGrace(int _key)
    {
        if (!CheckIsActive(_key))
        {
            graceList.Add(DatabaseManager.Instance.SelectGrace(_key));
            graceList[graceList.Count - 1].isActive = 1;
            SetGraceAbility();
        }
        else
            Debug.Log("이미 배운 은총");
    }
    
    public bool CheckIsActive(int _key)
    {
        bool isActive = false;
        Debug.Log("체크하려는 키는 " + _key);
        for(int i = 0; i < graceList.Count; i++)
        {
            if (graceList[i].graceKey == _key)
            {
                if (graceList[i].isActive == 1)
                    isActive = true;
                else
                    isActive = false;
            }
        }
        return isActive;
    }

    public void SetGraceAbility()
    {
        playerStatus.InitGraceStatus();
        for (int i = 0; i < graceList.Count; i++)
        {
            switch(graceList[i].graceKey)
            {
                case 0:
                    if (playerStatus.EquipmentController.EquipItems[7].attackType == "Melee")
                        playerStatus.GracePhysicalDamage += 0.1f;
                    break;
                case 1:
                    if(playerStatus.EquipmentController.EquipItems[7].weaponType == "Sword")
                        playerStatus.GracePhysicalDamage += 0.1f;
                    break;
                case 2:
                    if (playerStatus.EquipmentController.EquipItems[7].weaponType == "Axe" ||
                        playerStatus.EquipmentController.EquipItems[7].weaponType == "Spear")
                        playerStatus.GracePhysicalDamage += 0.1f;
                    break;
                case 3:
                    if (playerStatus.EquipmentController.EquipItems[7].weaponType == "Sword")
                        playerStatus.GraceAttackSpeed += 0.1f;
                    break;
                case 4:
                    if (playerStatus.EquipmentController.EquipItems[7].weaponType == "Sword")
                        playerStatus.GracePhysicalDamage += 0.2f;
                    break;
            }
        }
    }
}
