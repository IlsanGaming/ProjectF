using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public void OnclickStory()
    {
        Debug.Log("OnclickStory ����"); // ����� �α� �߰�
        gameObject.SetActive(false);
        GameManager.instance.ShowPrologue1();
    }
    public void OnclickInfinity()
    {
        Debug.Log("���Ѹ�� ����"); // ����� �α� �߰�
        gameObject.SetActive(false);
        GameManager.instance.StageStart();
    }
}
