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

    public void AquireGrace(int _key)
    {
        graceList.Add(DatabaseManager.Instance.SelectGrace(_key));
        graceList[graceList.Count - 1].isActive = 1;
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
}
