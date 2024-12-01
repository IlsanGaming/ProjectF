using System.Collections;
using System.Collections.Generic;
using KoreanTyper; // Korean Typer 네임스페이스 추가
using UnityEngine;
using UnityEngine.UI;


public class KoreanTyperOnEnable : MonoBehaviour
{
    public Text TestText; // 타이핑을 출력할 Text 컴포넌트
    [TextArea]
    public string typingText; // 출력할 문자열
    public float typingSpeed = 0.03f; // 타이핑 속도

    //==================================================================================
    // OnEnable | 오브젝트가 활성화될 때 호출
    //==================================================================================
    private void OnEnable()
    {
        if (TestText != null)
        {
            StartCoroutine(TypingCoroutine(typingText));
        }
    }

    //==================================================================================
    // TypingCoroutine | 타이핑 코루틴
    //==================================================================================
    private IEnumerator TypingCoroutine(string str)
    {
        TestText.text = ""; // 텍스트 초기화
        yield return new WaitForSeconds(0.3f); // 1초 대기 (옵션)

        int strTypingLength = str.GetTypingLength(); // 최대 타이핑 길이 계산
        for (int i = 0; i <= strTypingLength; i++)
        {
            TestText.text = str.Typing(i); // 타이핑 효과
            yield return new WaitForSeconds(typingSpeed); // 타이핑 속도만큼 대기
        }
    }
}