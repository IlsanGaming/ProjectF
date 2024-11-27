using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string type; // ������ ���� (Health, Exp, Boom ��)
    public ObjectManager objectManager; // ObjectManager ����

    void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾�� �浹 ��
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.HandleItemPickup(this); // �÷��̾ �������� ó��
                gameObject.SetActive(false); // ������ ��Ȱ��ȭ
            }
        }

        // BorderBullet�� �浹 ��
        if (collision.gameObject.tag == "Borderbullet")
        {
            gameObject.SetActive(false); // ������ ��Ȱ��ȭ
        }
    }
}