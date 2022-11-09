using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance = null;
    public float[] graceProbs = new float[4];
    public float[] skillProbs = new float[4];
    private void Awake()
    {
        Instance = this;
    }
    int Choose(float[] probs)
    {

        float total = 0;

        foreach (float elem in probs)
        {
            total += elem;
        }

        float randomPoint = Random.value * total;

        for (int i = 0; i < probs.Length; i++)
        {
            if (randomPoint < probs[i])
            {
                return i;
            }
            else
            {
                randomPoint -= probs[i];
            }
        }
        return probs.Length - 1;
    }
    public Skill ChooseRandomSkill()
    {
        Skill _skill = DatabaseManager.Instance.SelectSkill(Random.Range(0, DatabaseManager.Instance.skillList.Count));

        return _skill;
    }
    public Item CreateItem(Item _item)
    {
        for(int i = 0; i < Choose(graceProbs); i++)
        {
            _item.grace.Add(GraceManager.Instance.CreateRandomGrace());
        }
        for (int i = 0; i < Choose(skillProbs); i++)
        {
            _item.skills.Add(ChooseRandomSkill());
        }
        return _item;
    }
}
