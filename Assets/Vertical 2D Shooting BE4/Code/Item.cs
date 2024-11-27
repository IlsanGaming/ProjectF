using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string type; // 아이템 종류 (Health, Exp, Boom 등)
    public ObjectManager objectManager; // ObjectManager 참조

    void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어와 충돌 시
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.HandleItemPickup(this); // 플레이어가 아이템을 처리
                gameObject.SetActive(false); // 아이템 비활성화
            }
        }

        // BorderBullet과 충돌 시
        if (collision.gameObject.tag == "Borderbullet")
        {
            gameObject.SetActive(false); // 아이템 비활성화
        }
    }
}