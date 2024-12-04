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
            Debug.LogError("GameManager.instance가 null입니다.");
            return;
        }
        if (myText == null && mySlider == null)
        {
            Debug.LogError("myText와 mySlider가 둘 다 null입니다.");
            return;
        }
        if (type == InfoType.HealthG || type == InfoType.HealthP)
        {
            if (Player.instance == null)
            {
                Debug.LogError("Player.instance가 null입니다.");
                return;
            }
        }
        switch (type)
        {
            case InfoType.Exp:
                // 경험치 슬라이더 업데이트
                float curExp = GameManager.instance.exp; // 현재 경험치
                float maxExp = GameManager.instance.nextExp[Mathf.Min(GameManager.instance.level, GameManager.instance.nextExp.Length - 1)]; // 현재 레벨에서의 최대 경험치
                mySlider.value = curExp / maxExp; // 경험치 비율로 슬라이더 값 설정
                break;
            case InfoType.Time:
                // 진행 시간 텍스트 업데이트
                float gameTime = GameManager.instance.gameTime; // 남은 시간 계산
                int min = Mathf.FloorToInt(gameTime / 60); // 분 단위 계산
                int sec = Mathf.FloorToInt(gameTime % 60); // 초 단위 계산
                myText.text = string.Format("{0:D2}:{1:D2}", min, sec); // "MM:SS" 형식으로 시간 표시
                break;
            case InfoType.HealthG:
                // 체력 슬라이더 업데이트
                float curHealth = Player.instance.health; // 현재 체력
                float maxHealth = Player.instance.maxhealth;
                mySlider.value = curHealth / maxHealth; // 체력 비율로 슬라이더 값 설정
                break;
            case InfoType.HealthP:
                // 체력 슬라이더 업데이트
                float cHealth = Player.instance.health; // 현재 체력
                if (cHealth < 0)
                {
                    myText.text = "0%"; // 0 미만일 경우 출력하지 않음
                }
                else
                {
                    myText.text = string.Format("{0:F1}%", cHealth); // 소수점 첫 번째 자리까지 출력
                }
                break;
            case InfoType.Skill:
                myText.text = string.Format("X : {0:F0}", Player.instance.skill5Stack); 
                break;
            case InfoType.level:
                // 레벨 텍스트 업데이트
                myText.text = string.Format("{0:F0}", GameManager.instance.level); // 현재 레벨 표시
                break;
        }
    }
}
