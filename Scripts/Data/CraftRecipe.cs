using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CraftRecipe
{
    public int recipeKey = -1;
    public string recipeName = null;
    public int completeItemKey = -1;

    public int necessaryItemKey1 = -1;
    public int necessaryItemKey2 = -1;
    public int necessaryItemKey3 = -1;
    public int necessaryItemKey4 = -1;
    public int[] necessaryItemKeies = new int[4];
    public int necessaryItemCount1 = -1;
    public int necessaryItemCount2 = -1;
    public int necessaryItemCount3 = -1;
    public int necessaryItemCount4 = -1;
    public int[] necessaryItemCounts = new int[4];

    public CraftRecipe(int recipeNum, string recipeName, int completeItemKey, int necessaryItemKey1, int necessaryItemKey2, int necessaryItemKey3, int necessaryItemKey4, int necessaryItemCount1, int necessaryItemCount2, int necessaryItemCount3, int necessaryItemCount4)
    {
        this.recipeKey = recipeNum;
        this.recipeName = recipeName;
        this.completeItemKey = completeItemKey;
        this.necessaryItemKey1 = necessaryItemKey1;
        this.necessaryItemKey2 = necessaryItemKey2;
        this.necessaryItemKey3 = necessaryItemKey3;
        this.necessaryItemKey4 = necessaryItemKey4;
        necessaryItemKeies[0] = this.necessaryItemKey1;
        necessaryItemKeies[1] = this.necessaryItemKey2;
        necessaryItemKeies[2] = this.necessaryItemKey3;
        necessaryItemKeies[3] = this.necessaryItemKey4;
        this.necessaryItemCount1 = necessaryItemCount1;
        this.necessaryItemCount2 = necessaryItemCount2;
        this.necessaryItemCount3 = necessaryItemCount3;
        this.necessaryItemCount4 = necessaryItemCount4;
        necessaryItemCounts[0] = this.necessaryItemCount1;
        necessaryItemCounts[1] = this.necessaryItemCount2;
        necessaryItemCounts[2] = this.necessaryItemCount3;
        necessaryItemCounts[3] = this.necessaryItemCount4;
    }
}
