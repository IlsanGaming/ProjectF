using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyname;
    public int enemyscore;

    public float speed;
    public int health;
    public Sprite[] sprites;

    SpriteRenderer spriteRenderer;

    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public GameObject player;
    public ObjectManager objectManager;


    public GameObject itemCoin;
    public GameObject itemBoom;
    public GameObject itemPower;



    public float MaxShotDelay;
    public float CurShotDelay;




    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void OnEnable()
    {
        switch(enemyname)
        {
            case "L":
                health = 40;
                break;
            case "M":
                health = 10;
                break;
            case "S":
                health = 3;
                break;
        }
    }

    void Update()
    {
        Fire();
        Reload();
    }
    void Fire()
    {

        if (CurShotDelay < MaxShotDelay)
            return;
        if(enemyname == "S")
        {
            GameObject bullet = objectManager.MakeObj("BulletEnemyA");
            bullet.transform.position = transform.position;
            Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
            Vector3 dirVec = player.transform.position - transform.position;
            rigidbody.AddForce(dirVec.normalized * 10, ForceMode2D.Impulse);
        }
        else if(enemyname == "L")
        {
            GameObject bulletR = objectManager.MakeObj("BulletEnemyB");
            bulletR.transform.position = transform.position + Vector3.right * 0.3f;
            GameObject bulletL = objectManager.MakeObj("BulletEnemyB");
            bulletL.transform.position = transform.position + Vector3.left * 0.3f;

            Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();

            Vector3 dirVecR = player.transform.position - (transform.position + Vector3.right * 0.3f);
            Vector3 dirVecL = player.transform.position - (transform.position + Vector3.left * 0.3f);

            rigidR.AddForce(dirVecR.normalized * 10, ForceMode2D.Impulse);
            rigidL.AddForce(dirVecL.normalized * 10, ForceMode2D.Impulse);
        }

        CurShotDelay = 0;
    }
    void Reload()
    {
        CurShotDelay += Time.deltaTime;
    }

    public void OnHit(int dmg)
    {
        if (health <= 0)
            return;
        health -= dmg;
        spriteRenderer.sprite = sprites[1];
        Invoke("ReturnSprite", 0.1f);


        if(health <=0 )
        {
            Player playerLogic = player.GetComponent<Player>();
            playerLogic.score += enemyscore;

            //#.Random Radio Item Drop
            int ran = Random.Range(0, 10);
            if(ran<3)
            {
                //Not Item 30%
                Debug.Log("Not Item");
            }
            else if(ran<6)
            {
                //Coin 30%
                GameObject itemCoin = objectManager.MakeObj("ItemCoin");
                itemCoin.transform.position=transform.position;
            }
            else if(ran<8)
            {
                //Power 20%
                GameObject itemPower = objectManager.MakeObj("ItemPower");
                itemPower.transform.position=transform.position;
            }
            else if (ran<10)
            {
                //Boom 20%
                GameObject itemBoom = objectManager.MakeObj("ItemBoom");
                itemBoom.transform.position = transform.position;
            }
            gameObject.SetActive(false);
        }
    }

    void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Borderbullet")
        {
            gameObject.SetActive(false);
            transform.rotation= Quaternion.identity;
        }

        else if (collision.gameObject.tag == "PlayerBullet")
        {
            // 플레이어의 총알과 충돌 시
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                OnHit(bullet.dmg); // 먼저 데미지를 적용하고 Health를 감소시킴

                // 적의 체력이 0 이하일 경우에만 비활성화
                if (health <= 0)
                {
                    gameObject.SetActive(false);
                    transform.rotation = Quaternion.identity;
                }
            }
        }
    }
}
