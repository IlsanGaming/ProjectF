using System.Collections;
using System.Collections.Generic;
using KoreanTyper; // Korean Typer ���ӽ����̽� �߰�
using UnityEngine;
using UnityEngine.UI;


public class KoreanTyperOnEnable : MonoBehaviour
{
    public Text TestText; // Ÿ������ ����� Text ������Ʈ
    [TextArea]
    public string typingText; // ����� ���ڿ�
    public float typingSpeed = 0.03f; // Ÿ���� �ӵ�

    //==================================================================================
    // OnEnable | ������Ʈ�� Ȱ��ȭ�� �� ȣ��
    //==================================================================================
    private void OnEnable()
    {
        if (TestText != null)
        {
            StartCoroutine(TypingCoroutine(typingText));
        }
    }

    //==================================================================================
    // TypingCoroutine | Ÿ���� �ڷ�ƾ
    //==================================================================================
    private IEnumerator TypingCoroutine(string str)
    {
        TestText.text = ""; // �ؽ�Ʈ �ʱ�ȭ
        yield return new WaitForSeconds(0.3f); // 1�� ��� (�ɼ�)

        int strTypingLength = str.GetTypingLength(); // �ִ� Ÿ���� ���� ���
        for (int i = 0; i <= strTypingLength; i++)
        {
            TestText.text = str.Typing(i); // Ÿ���� ȿ��
            yield return new WaitForSeconds(typingSpeed); // Ÿ���� �ӵ���ŭ ���
        }
    }
}