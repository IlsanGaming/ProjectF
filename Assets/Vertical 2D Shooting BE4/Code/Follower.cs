using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public float MaxShotDelay; // �ִ� �߻� ������
    public float CurShotDelay; // ���� �߻� ������
    public ObjectManager objectManager;

    public Transform parent; // ���� �θ� (�÷��̾�)
    public Player player;

    public float orbitRadius; // ȸ�� ������
    public float orbitSpeed; // ȸ�� �ӵ�
    public float angleOffset; // �ȷο��� �ʱ� ���� ������

    void Update()
    {
        RotateAroundPlayer(); // �÷��̾� ������ ȸ��
        Fire();   // �Ѿ� �߻�
        Reload(); // �߻� ������ ����
    }

    // �÷��̾ �߽����� ȸ���ϴ� �Լ�
    void RotateAroundPlayer()
    {
        // parent�� �������� ���� ��� ��ȯ
        if (parent == null) return;

        // �ð� ������� ���� ���
        float angle = Time.time * orbitSpeed + angleOffset;

        // �ﰢ �Լ��� �̿��� ���� �˵� ���
        Vector3 offset = new Vector3(
            Mathf.Cos(angle * Mathf.Deg2Rad), // x�� ���
            Mathf.Sin(angle * Mathf.Deg2Rad), // y�� ���
            0
        ) * orbitRadius; // �������� ���� ũ�� ����

        // �÷��̾� ��ġ�� �������� �̵�
        transform.position = parent.position + offset;
    }

    // �Ѿ��� �߻��ϴ� �Լ�
    void Fire()
    {
        if (CurShotDelay < MaxShotDelay)
            return;

        // �Ѿ� ���� �� �߻�
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

    // �߻� �����̸� �����ϴ� �Լ�
    void Reload()
    {
        CurShotDelay += Time.deltaTime;
    }
}