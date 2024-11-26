using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public float MaxShotDelay; // 최대 발사 딜레이
    public float CurShotDelay; // 현재 발사 딜레이
    public ObjectManager objectManager;

    public Transform parent; // 따라갈 부모 (플레이어)
    public Player player;

    public float orbitRadius; // 회전 반지름
    public float orbitSpeed; // 회전 속도
    public float angleOffset; // 팔로워별 초기 각도 오프셋

    void Update()
    {
        RotateAroundPlayer(); // 플레이어 주위를 회전
        Fire();   // 총알 발사
        Reload(); // 발사 딜레이 갱신
    }

    // 플레이어를 중심으로 회전하는 함수
    void RotateAroundPlayer()
    {
        // parent가 설정되지 않은 경우 반환
        if (parent == null) return;

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
        switch(player.skill2Level)
        {
            case 1:
                MaxShotDelay = 1.25f;
                orbitSpeed = 100f;
                break;
            case 2:
                MaxShotDelay = 0.8f;
                orbitSpeed = 120f;
                break;
            case 3:
                MaxShotDelay = 0.5f;
                orbitSpeed = 140f;
                break;
            case 4:
                MaxShotDelay = 0.3f;
                orbitSpeed = 160f;
                break;
            case 5:
                MaxShotDelay = 0.2f;
                orbitSpeed = 180f;
                break;
        }
        Debug.Log(MaxShotDelay);
        if (CurShotDelay < MaxShotDelay)
            return;

        CurShotDelay = 0;
    }

    // 발사 딜레이를 갱신하는 함수
    void Reload()
    {
        CurShotDelay += Time.deltaTime;
    }
}