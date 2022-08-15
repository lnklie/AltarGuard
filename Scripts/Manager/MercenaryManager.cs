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
    public List<GameObject> Mercenarys
    {
        get { return mercenarys; }
        set { mercenarys = value; }
    }
    public void AddNewMercenary()
    {
        GameObject _newCharacter = Instantiate(characterPrefab, this.transform);
        EquipmentController _equipmentController = _newCharacter.GetComponentInChildren<EquipmentController>();
        MercenaryStatus _mercenaryStatus = _newCharacter.GetComponentInChildren<MercenaryStatus>();
        _mercenaryStatus.gameObject.transform.position = revialZone.transform.position;
        _mercenaryStatus.MercenaryNum = mercenarys.Count;

        for(int i = 0; i < _equipmentController.EquipItems.Length; i ++)
        {
            if (_equipmentController.CheckEquipItems[i])
                _equipmentController.EquipItems[i].equipCharNum = _mercenaryStatus.MercenaryNum;
        }
        mercenarys.Add(_newCharacter);
        UIManager.Instance.AddMercenary(_mercenaryStatus);
        UIManager.Instance.SetActiveCharactersProfile(_mercenaryStatus.MercenaryNum + 1, true);
        UIManager.Instance.UpdateMercenaryProfile(_mercenaryStatus.MercenaryNum);
    }
}
