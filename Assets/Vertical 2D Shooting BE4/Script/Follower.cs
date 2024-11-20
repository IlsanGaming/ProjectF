using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Follower : MonoBehaviour
{
    // �Ѿ� �߻�� ���õ� ������ ����
    public float MaxShotDelay; // �ִ� �߻� ������ (�߻� ����)
    public float CurShotDelay; // ���� �߻� ������ (�ð� ����� ���� ����)

    // ������Ʈ �Ŵ����� ���� �Ѿ� ����
    public ObjectManager objectManager;

    // �ȷο� ���۰� ���õ� ����
    public Vector3 followPos; // �ȷο��� ���� ��ǥ ��ġ
    public int followDelay; // �÷��̾ ���󰡴� ���� �ð� (ť�� ũ��� ����)
    public Transform parent; // �ȷο��� ���� �θ� ��ü (��: �÷��̾�)
    public Queue<Vector3> parentPos; // �θ��� ��ġ�� �����ϴ� ť
    public float orbitRadius; // ȸ�� ������
    public float orbitSpeed; // ȸ�� �ӵ�
    public float angleOffset; // �ȷο��� �ʱ� ���� ������



    // Update: �� ������ ȣ��Ǿ� �ȷο� ������ ó��
    void Update()
    {
        RotateAroundPlayer(); // �÷��̾� ������ ȸ��
        Fire();   // �Ѿ� �߻�
        Reload(); // �߻� ������ ����
    }

    // �÷��̾ �߽����� ȸ���ϴ� �Լ�
    // �÷��̾ �߽����� ȸ���ϴ� �Լ�
    void RotateAroundPlayer()
    {
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

        CurShotDelay = 0;
    }

    // �߻� �����̸� �����ϴ� �Լ�
    void Reload()
    {
        CurShotDelay += Time.deltaTime;
    }
}
