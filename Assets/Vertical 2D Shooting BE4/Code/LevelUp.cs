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

            // �ߺ����� �ʴ� 3���� �ε��� ����
            if (ran[0] != ran[1] && ran[1] != ran[2] && ran[0] != ran[2])
                break;
        }

        for (int index = 0; index < ran.Length; index++)
        {
            Skill ranSkill = skills[ran[index]];

            // ������ ��� ������ �ƴ� �ٸ� ��ų�� �������� ����
            if (ranSkill.level + 1 == ranSkill.skillData.grade.Length)
            {
                bool foundReplacement = false;

                // ������ �ƴ� �ٸ� ��ų�� �����ϰ� ����
                List<Skill> nonMaxSkills = new List<Skill>();
                foreach (Skill skill in skills)
                {
                    if (skill.level + 1 < skill.skillData.grade.Length && !System.Array.Exists(ran, r => skills[r] == skill))
                    {
                        nonMaxSkills.Add(skill);
                    }
                }

                // ������ �ƴ� ��ų�� �ִ� ��� ���� ����
                if (nonMaxSkills.Count > 0)
                {
                    Skill replacementSkill = nonMaxSkills[Random.Range(0, nonMaxSkills.Count)];
                    replacementSkill.gameObject.SetActive(true);
                    foundReplacement = true;
                }

                // ������ �ƴ� ��ü ��ų�� ������ �Һ� ������ Ȱ��ȭ
                if (!foundReplacement)
                {
                    skills[8].gameObject.SetActive(true); // �Һ� ������ Ȱ��ȭ
                }
            }
            else
            {
                ranSkill.gameObject.SetActive(true); // �Ϲ� ������ Ȱ��ȭ
            }
        }
        GameManager.instance.GetExp(0);
    }
}
