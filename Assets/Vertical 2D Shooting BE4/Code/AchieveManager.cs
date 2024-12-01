using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchieveManager : MonoBehaviour
{
    public GameObject[] lockMode;
    public GameObject[] unlockMode;

    public GameObject uiNotice;
    enum Achive {UnlockInfinity}
    Achive[] achives;
    void Awake()
    {
        achives = (Achive[])Enum.GetValues(typeof(Achive));

        if(!PlayerPrefs.HasKey("MyData"))
        {
            Init();
        }
    }
    void Init()
    {
        PlayerPrefs.SetInt("MyData", 1);
        foreach(Achive achive in achives)
        {
            PlayerPrefs.SetInt(achive.ToString(), 0);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        UnlockMode();
    }
    void UnlockMode()
    {
        for(int index=0;index<lockMode.Length;index++)
        {
            string achiveName = achives[index].ToString();
            bool isUnlock=PlayerPrefs.GetInt(achiveName)==1;
            lockMode[index].SetActive(!isUnlock);
            unlockMode[index].SetActive(isUnlock);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        foreach(Achive achive in achives)
        {
            CheckAchive(achive);
        }
    }
    void CheckAchive(Achive achive)
    {
        bool isAchieve=false;

        switch(achive)
        {
            case Achive.UnlockInfinity:
                isAchieve = GameManager.instance.StoryClear;
                break;

        }
        if(isAchieve&&PlayerPrefs.GetInt(achive.ToString())==0)
        {
            PlayerPrefs.SetInt(achive.ToString(), 1);
            // UI 알림 표시 설정
            for (int index = 0; index < uiNotice.transform.childCount; index++)
            {
                bool isActive = index == (int)achive;
                uiNotice.transform.GetChild(index).gameObject.SetActive(isActive);
            }

            // 알림 루틴 실행
            StartCoroutine(NoticeRoutine());
        }
    }
    IEnumerator NoticeRoutine()
    {
        uiNotice.SetActive(true); // 알림 UI 활성화
        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp); // 업적 달성 효과음 재생
        yield return new WaitForSeconds(5f); // 설정된 시간(5초) 동안 대기
        uiNotice.SetActive(false); // 알림 UI 비활성화
    }
}
