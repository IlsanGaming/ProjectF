using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public void OnclickStory()
    {
        gameObject.SetActive(false);
        GameManager.instance.Resume();
    }
}
