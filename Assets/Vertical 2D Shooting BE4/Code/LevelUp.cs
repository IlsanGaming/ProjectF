using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    Skill[] skills;
    void Awake()
    {
        rect= GetComponent<RectTransform>();
        skills =GetComponentsInChildren<Skill>(true);
    }
    public void Show()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);
        AudioManager.instance.EffectBgm(true);
        Next();
        rect.localScale= Vector3.one;
        GameManager.instance.Stop();
    }
    public void Hide()
    {
        rect.localScale=-Vector3.zero;
        GameManager.instance.Resume();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        AudioManager.instance.EffectBgm(true);
    }
    void Next()
    {
        foreach (Skill skill in skills)
        {
            skill.gameObject.SetActive(false);
        }

        int[] ran = new int[3];
        while (true)
        {
            ran[0] = Random.Range(0, skills.Length);
            ran[1] = Random.Range(0, skills.Length);
            ran[2] = Random.Range(0, skills.Length);

            // 중복되지 않는 3개의 인덱스 선택
            if (ran[0] != ran[1] && ran[1] != ran[2] && ran[0] != ran[2])
                break;
        }

        for (int index = 0; index < ran.Length; index++)
        {
            Skill ranSkill = skills[ran[index]];

            // 만렙인 경우 만렙이 아닌 다른 스킬을 랜덤으로 선택
            if (ranSkill.level + 1 == ranSkill.skillData.grade.Length)
            {
                bool foundReplacement = false;

                // 만렙이 아닌 다른 스킬을 랜덤하게 선택
                List<Skill> nonMaxSkills = new List<Skill>();
                foreach (Skill skill in skills)
                {
                    if (skill.level + 1 < skill.skillData.grade.Length && !System.Array.Exists(ran, r => skills[r] == skill))
                    {
                        nonMaxSkills.Add(skill);
                    }
                }

                // 만렙이 아닌 스킬이 있는 경우 랜덤 선택
                if (nonMaxSkills.Count > 0)
                {
                    Skill replacementSkill = nonMaxSkills[Random.Range(0, nonMaxSkills.Count)];
                    replacementSkill.gameObject.SetActive(true);
                    foundReplacement = true;
                }

                // 만렙이 아닌 대체 스킬이 없으면 소비 아이템 활성화
                if (!foundReplacement)
                {
                    skills[8].gameObject.SetActive(true); // 소비 아이템 활성화
                }
            }
            else
            {
                ranSkill.gameObject.SetActive(true); // 일반 아이템 활성화
            }
        }
        GameManager.instance.GetExp(0);
    }
}
