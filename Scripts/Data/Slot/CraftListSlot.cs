using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftListSlot : MonoBehaviour
{
    [SerializeField]
    private CraftRecipe craftRecipe = null;
    
    public CraftRecipe CraftRecipe
    {
        get { return craftRecipe; }
        set { craftRecipe = value; }
    }
    
    public void SelectCraptRecipe()
    {
        UIManager.Instance.SelectCraftRecipe(craftRecipe);
    }
}
