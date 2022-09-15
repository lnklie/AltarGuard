using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillSlot : MonoBehaviour
{
    [SerializeField] private Skill curSkill = null;
    [SerializeField] private Sprite defaultSprite = null;
    [SerializeField] private Image coolTimeImage = null;
    [SerializeField] private Image skillImage = null;

    private void Update()
    {
        if (curSkill.isCoolTime)
        {
            coolTimeImage.gameObject.SetActive(true);
            coolTimeImage.fillAmount = curSkill.coolTime / curSkill.maxCoolTime;
        }
        else
            coolTimeImage.gameObject.SetActive(false);
    }

    public void InitSlot()
    {
        curSkill = null;
        skillImage.sprite = defaultSprite;
    }

    public void SetSlot(Skill _skill)
    {
        curSkill = _skill;
        skillImage.sprite = curSkill.singleSprite;
    }

    public void UseSkill()
    {
        UIManager.Instance.UseSkill(curSkill);
    }
}
