using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { Exp,Time,HealthG,Skill,level,HealthP};
    public InfoType type;

    Text myText;
    Slider mySlider;
    void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }
    void LateUpdate()
    {
        if (GameManager.instance == null)
        {
            Debug.LogError("GameManager.instance�� null�Դϴ�.");
            return;
        }
        if (myText == null && mySlider == null)
        {
            Debug.LogError("myText�� mySlider�� �� �� null�Դϴ�.");
            return;
        }
        if (type == InfoType.HealthG || type == InfoType.HealthP)
        {
            if (Player.instance == null)
            {
                Debug.LogError("Player.instance�� null�Դϴ�.");
                return;
            }
        }
        switch (type)
        {
            case InfoType.Exp:
                // ����ġ �����̴� ������Ʈ
                float curExp = GameManager.instance.exp; // ���� ����ġ
                float maxExp = GameManager.instance.nextExp[Mathf.Min(GameManager.instance.level, GameManager.instance.nextExp.Length - 1)]; // ���� ���������� �ִ� ����ġ
                mySlider.value = curExp / maxExp; // ����ġ ������ �����̴� �� ����
                break;
            case InfoType.Time:
                // ���� �ð� �ؽ�Ʈ ������Ʈ
                float gameTime = GameManager.instance.gameTime; // ���� �ð� ���
                int min = Mathf.FloorToInt(gameTime / 60); // �� ���� ���
                int sec = Mathf.FloorToInt(gameTime % 60); // �� ���� ���
                myText.text = string.Format("{0:D2}:{1:D2}", min, sec); // "MM:SS" �������� �ð� ǥ��
                break;
            case InfoType.HealthG:
                // ü�� �����̴� ������Ʈ
                float curHealth = Player.instance.health; // ���� ü��
                float maxHealth = Player.instance.maxhealth;
                mySlider.value = curHealth / maxHealth; // ü�� ������ �����̴� �� ����
                break;
            case InfoType.HealthP:
                // ü�� �����̴� ������Ʈ
                float cHealth = Player.instance.health; // ���� ü��
                if (cHealth < 0)
                {
                    myText.text = "0%"; // 0 �̸��� ��� ������� ����
                }
                else
                {
                    myText.text = string.Format("{0:F1}%", cHealth); // �Ҽ��� ù ��° �ڸ����� ���
                }
                break;
            case InfoType.Skill:
                myText.text = string.Format("X : {0:F0}", Player.instance.skill5Stack); 
                break;
            case InfoType.level:
                // ���� �ؽ�Ʈ ������Ʈ
                myText.text = string.Format("{0:F0}", GameManager.instance.level); // ���� ���� ǥ��
                break;
        }
    }
}
