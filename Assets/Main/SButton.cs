using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SButton : MonoBehaviour
{
    public static SButton instance;
    public bool isOpen;

    private void Awake()
    {
        instance = this;
    }
    public void OnClick()
    {
        SceneManager.LoadScene(1); // 1¹ø ¾À ·Îµå
    }
}
