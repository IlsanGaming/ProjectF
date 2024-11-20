using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // 폭발 애니메이션을 제어하기 위한 Animator 변수
    Animator anim;

    // Awake는 게임 오브젝트가 생성될 때 한 번 호출되며, 초기화 작업에 사용
    void Awake()
    {
        // Animator 컴포넌트를 가져옴
        anim = GetComponent<Animator>();
    }

    // 오브젝트가 활성화될 때 호출
    void OnEnable()
    {
        // 2초 후 Disable() 함수 호출
        Invoke("Disable", 2f);
    }

    // 오브젝트가 비활성화될 때 호출
    void OnDisable()
    {
        // 오브젝트를 비활성화
        gameObject.SetActive(false);
    }

    // 폭발을 시작하는 함수
    public void StartExplosion(string target)
    {
        // 폭발 애니메이션 트리거를 실행
        anim.SetTrigger("OnExplosion");

        // 폭발의 크기를 목표(target)에 따라 설정
        switch (target)
        {
            case "S": // 작은 적에 대한 폭발
                transform.localScale = Vector3.one * 0.7f; // 크기를 0.7로 설정
                break;
            case "M": // 중간 적에 대한 폭발
            case "P": // 플레이어에 대한 폭발
                transform.localScale = Vector3.one * 1f; // 기본 크기 1로 설정
                break;
            case "L": // 큰 적에 대한 폭발
                transform.localScale = Vector3.one * 2f; // 크기를 2로 설정
                break;
            case "B": // 보스 적에 대한 폭발
                transform.localScale = Vector3.one * 3f; // 크기를 3으로 설정
                break;
        }
    }

    // 폭발 애니메이션이 끝난 후 오브젝트를 비활성화하는 함수
    void Disable()
    {
        // 오브젝트를 비활성화
        gameObject.SetActive(false);
    }
}
