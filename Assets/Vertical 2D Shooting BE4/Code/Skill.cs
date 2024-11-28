using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    public SkillData skillData; // ScriptableObject�� ����
    public int level; // ���� ��ų�� ����
    Image icon; // ��ų ������
    Text textLevel; // ��ų ���� �ؽ�Ʈ
    Text textDesc;
    Text textName;

    void Awake()
    {
        // UI ������Ʈ �ʱ�ȭ
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = skillData.skillIcon;
        // ������ �ؽ�Ʈ ���� ����
        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0]; // ù ��° �ؽ�Ʈ�� ����
        textName = texts[1];  // �� ��° �ؽ�Ʈ�� �̸�
        textDesc = texts[2];  // �� ��° �ؽ�Ʈ�� ����
    }

    void OnEnable()
    {
        textDesc.text = skillData.skillDesc;
        textName.text = skillData.skillName;
    }
    void LateUpdate()
    {
        textLevel.text = skillData.grade[level];
    }
    public void Onclick()
    {
        switch(skillData.skillType)
        {
            case SkillData.SkillType.Skill1:
                if (level == Player.instance.maxskill1Level)
                {
                    GetComponent<Button>().interactable = false;
                    return;
                }
                Player.instance.skill1Level++;
                break;
            case SkillData.SkillType.Skill2:
                if (level == Player.instance.maxskill2Level)
                {
                    GetComponent<Button>().interactable = false;
                    return;
                }
                Player.instance.skill2Level++;
                break;
            case SkillData.SkillType.Skill3:
                if (level == Player.instance.maxskill3Level)
                {
                    GetComponent<Button>().interactable = false;
                    return;
                }
                Player.instance.skill3Level++;
                break;
            case SkillData.SkillType.Skill4:
                if (level == Player.instance.maxskill4Level)
                {
                    GetComponent<Button>().interactable = false;
                    return;
                }
                Player.instance.skill4Level++;
                break;
            case SkillData.SkillType.Pspeed:
                if (level == Player.instance.maxPspeedLevel)
                {
                    GetComponent<Button>().interactable = false;
                    return;
                }
                Player.instance.PspeedLevel++;
                break;
            case SkillData.SkillType.Bspeed:
                if (level == Player.instance.maxBspeedLevel)
                {
                    GetComponent<Button>().interactable = false;
                    return;
                }
                Player.instance.BspeedLevel++;
                break;
            case SkillData.SkillType.Aspeed:
                if (level == Player.instance.maxAspeedLevel)
                {
                    GetComponent<Button>().interactable = false;
                    return;
                }
                Player.instance.AspeedLevel++;
                break;
            case SkillData.SkillType.Weight:
                if (level == Player.instance.maxWeightLevel)
                {
                    GetComponent<Button>().interactable = false;
                    return;
                }
                Player.instance.WeightLevel++;
                break;
            case SkillData.SkillType.Heal:
                Player.instance.HealthCharge();
                break; 
        }
        level++;
    }
}
