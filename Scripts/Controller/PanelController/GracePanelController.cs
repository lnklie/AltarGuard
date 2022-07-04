using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GracePanelController : MonoBehaviour
{
    [SerializeField]
    private List<GraceSlot> slots = new List<GraceSlot>();
    [SerializeField]
    private List<Button> slotButtons = new List<Button>();
    public delegate bool CheckIsActiveGrace(int _key);
    public delegate void AquireGraceDel(int _key);
    private void Awake()
    {
        slots.AddRange(GetComponentsInChildren<GraceSlot>());       
        for(int i = 0; i < slots.Count; i++)
        {
            slotButtons.Add(slots[i].GetComponent<Button>());
            slots[i].Grace = DatabaseManager.Instance.graceList[i];
        }
    }
    public void ActiveGracePanel(bool _bool)
    {
        this.gameObject.SetActive(_bool);
    }

}
