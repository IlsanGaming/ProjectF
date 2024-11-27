using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Skill",menuName ="Scriptable Skill/SkillData")]
public class SkillData : ScriptableObject
{
    
    public enum SkillType { Skill1,Skill2,Skill3,Skill4,Pspeed,Bspeed,Aspeed,Weight,Heal};
    [Header("MainInfo")]
    public SkillType skillType;
    public int skillId;
    public string skillName;
    public string skillDesc;
    public Sprite skillIcon;
    public string[] grade = { "�����", "������", "������", "�����", "������", "������", "��������" };
}
