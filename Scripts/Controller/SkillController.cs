using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    [SerializeField]
    private List<Skill> skills = new List<Skill>();
    // 얻기, 없애기, 사용하기, 스킬 레벨업,

    public void AquireSkill(int _skillKey)
    {
        skills.Add(DatabaseManager.Instance.SelectSkill(_skillKey));
    }

    public void RemoveSkill(Skill _skill)
    {
        if (skills.IndexOf(_skill) != -1)
            skills.Remove(_skill);
        else
            Debug.Log("없는 스킬");
    }

    public void UseSkill(Skill _skill)
    {
        
    }
}
