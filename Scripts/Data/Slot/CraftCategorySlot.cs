using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CraftCategorySlot : MonoBehaviour
{
    [SerializeField]
    private CraftListSlot[] craftListSlot = null;
    private bool _bool = false;

    public void SetActiveCraftListSlots()
    {
        _bool = !_bool;
        for(int i = 0; i < craftListSlot.Length; i++)
        {
            CraftRecipe _craftRecipe = DatabaseManager.Instance.craftRecipeList[i];
            craftListSlot[i].gameObject.SetActive(_bool);
            craftListSlot[i].CraftRecipe = _craftRecipe;
            craftListSlot[i].GetComponentInChildren<TextMeshProUGUI>().text = "  - " + _craftRecipe.recipeName;
        }
    }
}
