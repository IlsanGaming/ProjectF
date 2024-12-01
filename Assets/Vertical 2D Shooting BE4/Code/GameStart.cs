using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public void OnclickStory()
    {
        Debug.Log("OnclickStory 실행"); // 디버깅 로그 추가
        gameObject.SetActive(false);
        GameManager.instance.ShowPrologue1();
    }
}
