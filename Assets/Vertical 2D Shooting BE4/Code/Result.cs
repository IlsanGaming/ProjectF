using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{
    public GameObject[] titles;

    public void Lose()
    {
        titles[0].SetActive(true);
    }
    public void Win()
    {
        titles[1].SetActive(true);
    }
    public void BackToMain()
    {
        Enemy.instance.isClear = true;
        GameManager.instance.isLive = false;
        GameManager.instance.spawnList.Clear();
        SceneManager.LoadScene(0);
    }
}
