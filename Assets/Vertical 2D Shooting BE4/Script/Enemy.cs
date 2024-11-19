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
    Animator anim;

    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public GameObject player;
    public ObjectManager objectManager;
    public GameManager gameManager;


    public GameObject itemCoin;
    public GameObject itemBoom;
    public GameObject itemPower;



    public float MaxShotDelay;
    public float CurShotDelay;

    public int patternIndex;
    public int curPatternCount;
    public int[] maxPatternCount;


    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if(enemyname == "B")
            anim = GetComponent<Animator>();
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
            case "B":
                health = 3000;
                Invoke("Stop",2);
                break;
        }
    }
    void Stop()
    {
        if (!gameObject.activeSelf)
            return;

        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.zero;

        Invoke("Think", 2);
    }
    void Think()
    {
        patternIndex = patternIndex == 3 ? 0 : patternIndex + 1;
        curPatternCount = 0;

        switch (patternIndex)
        {
            case 0:
                FireForward();
                break;
            case 1:
                FireShot();
                break;
            case 2:
                FireArc();
                break;
            case 3:
                FireAround();
                break;
        }
    }
    void FireForward()
    {
        if (health <= 0) return;
        //Fire 4 Bullet Forward
        Debug.Log("앞으로 4발 발사.");
        GameObject bulletR = objectManager.MakeObj("BulletBossA");
        bulletR.transform.position = transform.position + Vector3.right * 0.3f;
        GameObject bulletRR = objectManager.MakeObj("BulletBossA");
        bulletRR.transform.position = transform.position + Vector3.right * 0.45f;
        GameObject bulletL = objectManager.MakeObj("BulletBossA");
        bulletL.transform.position = transform.position + Vector3.left * 0.3f;
        GameObject bulletLL = objectManager.MakeObj("BulletBossA");
        bulletLL.transform.position = transform.position + Vector3.left * 0.45f;

        Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();

        rigidR.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidRR.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidL.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidLL.AddForce(Vector2.down * 8, ForceMode2D.Impulse);

        //Pattern Count
        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireForward", 2);
        else
            Invoke("Think", 2);
    }
    void FireShot()
    {
        if (health <= 0) return;
        Debug.Log("플레이어 방향으로 샷건.");

        for (int index = 0; index < 5; index++)
        {
            GameObject bullet = objectManager.MakeObj("BulletBossB");
            bullet.transform.position = transform.position;
            Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
            Vector3 dirVec = player.transform.position - transform.position;
            Vector3 ranVec = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(0f, 2f));
            dirVec += ranVec;
            rigidbody.AddForce(dirVec.normalized * 10, ForceMode2D.Impulse);
        }
        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireShot", 3.5f);
        else
            Invoke("Think", 3);
    }
    void FireArc()
    {
        if (health <= 0) return;
        //Fire Arc Continue Fire
        Debug.Log("부채모양으로 발사");
        GameObject bullet = objectManager.MakeObj("BulletEnemyA");
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.identity;

        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
        Vector3 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 10 * curPatternCount / maxPatternCount[patternIndex]), -1);
        rigidbody.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);
        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireArc", 0.15f);
        else
            Invoke("Think", 3);
    }
    void FireAround()
    {
        if (health <= 0) return;
        Debug.Log("원 형태로 전체 공격");
        //Fire Around
        int roundNumA = 50;
        int roundNumB = 40;
        int roundNum=curPatternCount%2==0 ? roundNumA : roundNumB;
        for(int index=0;index<roundNum;index++)
        {
            GameObject bullet = objectManager.MakeObj("BulletBossB");
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;

            Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
            Vector3 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * index / roundNum)
                , Mathf.Sin(Mathf.PI * 2 * index / roundNum));
            rigidbody.AddForce(dirVec.normalized * 2, ForceMode2D.Impulse);
            Vector3 rotVec=Vector3.forward*360*index/roundNum+Vector3.forward*90;
            bullet.transform.Rotate(rotVec);
        }
        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireAround", 0.7f);
        else
            Invoke("Think", 3);
    }

    void Update()
    {
        if (enemyname == "B")
        {
            return;
        }
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
        if(enemyname=="B")
        {
            anim.SetTrigger("OnHit");
        }
        else
        {
            spriteRenderer.sprite = sprites[1];
        Invoke("ReturnSprite", 0.1f);
        }
        


        if(health <=0 )
        {
            Player playerLogic = player.GetComponent<Player>();
            playerLogic.score += enemyscore;

            //#.Random Radio Item Drop
            int ran = enemyname == "B" ? 0 : Random.Range(0, 10);
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
            transform.rotation = Quaternion.identity;
            CancelInvoke();

            gameManager.CallExplosion(transform.position, enemyname);
        }
    }

    void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Borderbullet"&&enemyname !="B")
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
                Debug.Log("hit");

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
