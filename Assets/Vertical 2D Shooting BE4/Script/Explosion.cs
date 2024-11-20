using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // ���� �ִϸ��̼��� �����ϱ� ���� Animator ����
    Animator anim;

    // Awake�� ���� ������Ʈ�� ������ �� �� �� ȣ��Ǹ�, �ʱ�ȭ �۾��� ���
    void Awake()
    {
        // Animator ������Ʈ�� ������
        anim = GetComponent<Animator>();
    }

    // ������Ʈ�� Ȱ��ȭ�� �� ȣ��
    void OnEnable()
    {
        // 2�� �� Disable() �Լ� ȣ��
        Invoke("Disable", 2f);
    }

    // ������Ʈ�� ��Ȱ��ȭ�� �� ȣ��
    void OnDisable()
    {
        // ������Ʈ�� ��Ȱ��ȭ
        gameObject.SetActive(false);
    }

    // ������ �����ϴ� �Լ�
    public void StartExplosion(string target)
    {
        // ���� �ִϸ��̼� Ʈ���Ÿ� ����
        anim.SetTrigger("OnExplosion");

        // ������ ũ�⸦ ��ǥ(target)�� ���� ����
        switch (target)
        {
            case "S": // ���� ���� ���� ����
                transform.localScale = Vector3.one * 0.7f; // ũ�⸦ 0.7�� ����
                break;
            case "M": // �߰� ���� ���� ����
            case "P": // �÷��̾ ���� ����
                transform.localScale = Vector3.one * 1f; // �⺻ ũ�� 1�� ����
                break;
            case "L": // ū ���� ���� ����
                transform.localScale = Vector3.one * 2f; // ũ�⸦ 2�� ����
                break;
            case "B": // ���� ���� ���� ����
                transform.localScale = Vector3.one * 3f; // ũ�⸦ 3���� ����
                break;
        }
    }

    // ���� �ִϸ��̼��� ���� �� ������Ʈ�� ��Ȱ��ȭ�ϴ� �Լ�
    void Disable()
    {
        // ������Ʈ�� ��Ȱ��ȭ
        gameObject.SetActive(false);
    }
}
