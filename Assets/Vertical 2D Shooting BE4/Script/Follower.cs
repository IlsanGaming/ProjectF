using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Follower : MonoBehaviour
{
    // 총알 발사와 관련된 딜레이 변수
    public float MaxShotDelay; // 최대 발사 딜레이 (발사 간격)
    public float CurShotDelay; // 현재 발사 딜레이 (시간 경과에 따라 증가)

    // 오브젝트 매니저를 통해 총알 생성
    public ObjectManager objectManager;

    // 팔로우 동작과 관련된 변수
    public Vector3 followPos; // 팔로워가 따라갈 목표 위치
    public int followDelay; // 플레이어를 따라가는 지연 시간 (큐의 크기로 조절)
    public Transform parent; // 팔로워가 따라갈 부모 객체 (예: 플레이어)
    public Queue<Vector3> parentPos; // 부모의 위치를 저장하는 큐
    public float orbitRadius; // 회전 반지름
    public float orbitSpeed; // 회전 속도
    public float angleOffset; // 팔로워별 초기 각도 오프셋



    // Update: 매 프레임 호출되어 팔로워 동작을 처리
    void Update()
    {
        RotateAroundPlayer(); // 플레이어 주위를 회전
        Fire();   // 총알 발사
        Reload(); // 발사 딜레이 갱신
    }

    // 플레이어를 중심으로 회전하는 함수
    // 플레이어를 중심으로 회전하는 함수
    void RotateAroundPlayer()
    {
        // 시간 기반으로 각도 계산
        float angle = Time.time * orbitSpeed + angleOffset;

        // 삼각 함수를 이용해 원형 궤도 계산
        Vector3 offset = new Vector3(
            Mathf.Cos(angle * Mathf.Deg2Rad), // x축 계산
            Mathf.Sin(angle * Mathf.Deg2Rad), // y축 계산
            0
        ) * orbitRadius; // 반지름을 곱해 크기 조정

        // 플레이어 위치를 기준으로 이동
        transform.position = parent.position + offset;
    }

    // 총알을 발사하는 함수
    void Fire()
    {
        if (CurShotDelay < MaxShotDelay)
            return;

        // 총알 생성 및 발사
        GameObject bullet = objectManager.MakeObj("BulletFollower");
        bullet.transform.position = transform.position;

        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
        rigidbody.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

        CurShotDelay = 0;
    }

    // 발사 딜레이를 갱신하는 함수
    void Reload()
    {
        CurShotDelay += Time.deltaTime;
    }
}
