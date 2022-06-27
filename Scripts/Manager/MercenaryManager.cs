using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MercenaryManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> mercenarys = new List<GameObject>();
    public List<GameObject> Mercenarys
    {
        get { return mercenarys; }
        set { mercenarys = value; }
    }

    [SerializeField]
    private GameObject characterPrefab = null;
    [SerializeField]
    private GameObject revialZone = null;
    public void AddNewMercenary()
    {
        GameObject _newCharacter = Instantiate(characterPrefab, this.transform);
        EquipmentController _equipmentController = _newCharacter.GetComponentInChildren<EquipmentController>();
        MercenaryStatus _mercenaryStatus = _newCharacter.GetComponentInChildren<MercenaryStatus>();
        _mercenaryStatus.gameObject.transform.position = revialZone.transform.position;
        _mercenaryStatus.MercenaryNum = mercenarys.Count + 1;
        _equipmentController.ChangeEquipment(DatabaseManager.Instance.SelectItem(0));
        _equipmentController.ChangeEquipment(DatabaseManager.Instance.SelectItem(1000));
        _equipmentController.ChangeEquipment(DatabaseManager.Instance.SelectItem(2000));
        _equipmentController.ChangeEquipment(DatabaseManager.Instance.SelectItem(3000));
        _equipmentController.ChangeEquipment(DatabaseManager.Instance.SelectItem(4000));
        _equipmentController.ChangeEquipment(DatabaseManager.Instance.SelectItem(5000));
        _equipmentController.ChangeEquipment(DatabaseManager.Instance.SelectItem(6000));
        _equipmentController.ChangeEquipment(DatabaseManager.Instance.SelectItem(7000));
        for(int i = 0; i < _equipmentController.EquipItems.Length; i ++)
        {
            if (_equipmentController.CheckEquipItems[i])
                _equipmentController.EquipItems[i].equipCharNum = _mercenaryStatus.MercenaryNum;
        }
        mercenarys.Add(_newCharacter);
        UIManager.Instance.AddMercenary(_mercenaryStatus);
        UIManager.Instance.SetActiveCharactersProfile(_mercenaryStatus.MercenaryNum, true);
        UIManager.Instance.ChangeMercenaryUIItemImage(_mercenaryStatus.MercenaryNum);
        UIManager.Instance.UpdateMercenaryProfile(_mercenaryStatus.MercenaryNum);
    }
}
