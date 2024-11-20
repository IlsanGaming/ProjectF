using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    // 스크롤 속도를 나타내는 변수
    public float speed;

    // 스프라이트 배열에서 시작 및 끝 인덱스 (배경의 처음과 끝 위치를 추적)
    public int startIndex;
    public int endIndex;

    // 배경 스프라이트들을 저장하는 배열
    public Transform[] sprites;

    // 카메라 높이 정보를 저장하기 위한 변수
    float viewHeight;

    // Awake는 게임 오브젝트가 활성화될 때 호출되며, 초기화 작업에 사용
    private void Awake()
    {
        // 카메라의 orthographic 크기를 이용해 화면 높이(viewHeight) 계산
        // orthographicSize는 카메라의 반높이, 따라서 2배를 곱해 전체 높이를 구함
        viewHeight = Camera.main.orthographicSize * 2;
    }

    // Update는 매 프레임 호출되며, 배경을 움직이고 스크롤링 처리
    void Update()
    {
        // 배경을 아래로 움직이는 함수 호출
        Move();

        // 스프라이트가 화면 아래로 사라지면 재사용하기 위한 스크롤링 처리
        Scrolling();
    }

    // 배경 스프라이트를 스크롤 처리하는 함수
    void Scrolling()
    {
        // 만약 끝 스프라이트가 화면 아래(-viewHeight)로 완전히 벗어났다면
        if (sprites[endIndex].position.y < viewHeight * (-1))
        {
            //#1. Sprite 재활용 처리
            // 시작 스프라이트의 위치를 가져옴
            Vector3 backSpritesPos = sprites[startIndex].transform.localPosition;
            // 끝 스프라이트의 현재 위치를 가져옴
            Vector3 frontSpritePos = sprites[endIndex].transform.localPosition;
            // 끝 스프라이트를 시작 스프라이트의 바로 위쪽으로 이동
            sprites[endIndex].transform.localPosition = backSpritesPos + Vector3.up * viewHeight;

            // 시작 및 끝 인덱스를 갱신 (순환하도록 처리)
            int startIndexSave = startIndex; // 현재 시작 인덱스를 임시 저장
            startIndex = endIndex; // 끝 스프라이트를 새로운 시작으로 설정
            endIndex = (startIndexSave - 1 == -1) ? sprites.Length - 1 : startIndexSave - 1;
            // 끝 인덱스를 이전 인덱스로 설정 (순환 구조로 처리)
        }
    }

    // 배경 스프라이트들을 아래로 움직이는 함수
    void Move()
    {
        // 현재 오브젝트의 위치를 가져옴
        Vector3 curPos = transform.position;

        // 속도(speed)를 기반으로 이동할 새로운 위치를 계산
        // Time.deltaTime은 프레임 간 시간 간격으로, 시간 기반 움직임을 구현
        Vector3 nextPos = Vector3.down * speed * Time.deltaTime;

        // 현재 위치에 이동량을 더해 새로운 위치로 업데이트
        transform.position = curPos + nextPos;
    }
}



