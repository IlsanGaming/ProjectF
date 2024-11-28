using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchieveManager : MonoBehaviour
{
    public GameObject[] lockMode;
    public GameObject[] unlockMode;

    public GameObject uiNotice;
    enum Achive {UnlockInfinity,Harder}
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
                isAchieve = Enemy.instance.isClear == true;
                break;

        }
        if(isAchieve&&PlayerPrefs.GetInt(achive.ToString())==0)
        {
            PlayerPrefs.SetInt(achive.ToString(), 1);
        }
    }
}
