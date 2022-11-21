using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillSlot : MonoBehaviour
{
    [SerializeField] private Skill curSkill = null;
    [SerializeField] private Image coolTimeImage = null;
    [SerializeField] private Image skillImage = null;

    private void Update()
    {
        if (curSkill != null && curSkill.isCoolTime)
        {
            coolTimeImage.gameObject.SetActive(true);
            coolTimeImage.fillAmount = curSkill.coolTime / curSkill.maxCoolTime;
        }
        else
            coolTimeImage.gameObject.SetActive(false);
    }

    public void InitSlot()
    {
        skillImage.gameObject.SetActive(false);
        curSkill = null;
        skillImage.sprite = null;
    }

    public void SetSlot(Skill _skill)
    {
        curSkill = _skill;
        skillImage.sprite = curSkill.singleSprite;
        skillImage.gameObject.SetActive(true);
    }

    public void UseSkill(int _index)
    {
        UIManager.Instance.UseSkill(_index);
    }
}
