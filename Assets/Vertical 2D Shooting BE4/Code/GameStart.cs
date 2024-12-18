using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public void OnclickStory()
    {
        Debug.Log("OnclickStory 실행"); // 디버깅 로그 추가
        GameManager.instance.stage = 0;
        gameObject.SetActive(false);
        GameManager.instance.ShowPrologue1();
    }
    public void OnclickInfinity()
    {
        Debug.Log("무한모드 실행"); // 디버깅 로그 추가
        gameObject.SetActive(false);
        GameManager.instance.stage = 1;
        GameManager.instance.StageStart();
    }
    public void GameExit()
    {
        Application.Quit();
    }
}
