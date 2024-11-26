using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    public LevelData[] levelDatas;
    public string enemyName;
    public float speed;//이동속도
    public int health;//체력
    public Sprite[] sprites;
    public float curShotSpeed;//장전속도
    public float maxShotSpeed;//최대 장전속도
    public float enemyBulletSpeed;//적 투사체 속도
    public int enemyExp;
    public int enemyLevel;

    public GameObject player;

    SpriteRenderer spriteRenderer;
    public ObjectManager objectManager;
    public GameManager gameManager;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }
    // 적 오브젝트가 활성화될 때 초기화 작업 수행
    void OnEnable()
    {
        enemyLevel = gameManager.difference;
        switch (enemyName)
        {
            case "L":
                switch(enemyLevel)
                {
                    case 0:
                        health = levelDatas[0].health;
                        speed = levelDatas[0].speed;
                        maxShotSpeed = levelDatas[0].maxShotSpeed;
                        speed=levelDatas[0].speed;
                        enemyExp=levelDatas[0].enemyExp;
                        break;
                    case 1:
                        health = levelDatas[1].health;
                        speed = levelDatas[1].speed;
                        maxShotSpeed = levelDatas[1].maxShotSpeed;
                        speed = levelDatas[1].speed;
                        enemyExp = levelDatas[1].enemyExp;
                        break;
                    case 2:
                        health = levelDatas[2].health;
                        speed = levelDatas[2].speed;
                        maxShotSpeed = levelDatas[2].maxShotSpeed;
                        speed = levelDatas[2].speed;
                        enemyExp = levelDatas[2].enemyExp;
                        break;
                    case 3:
                        health = levelDatas[3].health;
                        speed = levelDatas[3].speed;
                        maxShotSpeed = levelDatas[3].maxShotSpeed;
                        speed = levelDatas[3].speed;
                        enemyExp = levelDatas[3].enemyExp;
                        break;
                    case 4:
                        health = levelDatas[4].health;
                        speed = levelDatas[4].speed;
                        maxShotSpeed = levelDatas[4].maxShotSpeed;
                        speed = levelDatas[4].speed;
                        enemyExp = levelDatas[4].enemyExp;
                        break;
                    case 5:
                        health = levelDatas[5].health;
                        speed = levelDatas[5].speed;
                        maxShotSpeed = levelDatas[5].maxShotSpeed;
                        speed = levelDatas[5].speed;
                        enemyExp = levelDatas[5].enemyExp;
                        break;
                    case 6:
                        health = levelDatas[6].health;
                        speed = levelDatas[6].speed;
                        maxShotSpeed = levelDatas[6].maxShotSpeed;
                        speed = levelDatas[6].speed;
                        enemyExp = levelDatas[6].enemyExp;
                        break;
                    case 7:
                        health = levelDatas[7].health;
                        speed = levelDatas[7].speed;
                        maxShotSpeed = levelDatas[7].maxShotSpeed;
                        speed = levelDatas[7].speed;
                        enemyExp = levelDatas[7].enemyExp;
                        break;
                    case 8:
                        health = levelDatas[8].health;
                        speed = levelDatas[8].speed;
                        maxShotSpeed = levelDatas[8].maxShotSpeed;
                        speed = levelDatas[8].speed;
                        enemyExp = levelDatas[8].enemyExp;
                        break;
                    case 9:
                        health = levelDatas[9].health;
                        speed = levelDatas[9].speed;
                        maxShotSpeed = levelDatas[9].maxShotSpeed;
                        speed = levelDatas[9].speed;
                        enemyExp = levelDatas[9].enemyExp;
                        break;
                }
                break;
            case "M":
                switch (enemyLevel)
                {
                    case 0:
                        health = levelDatas[0].health;
                        speed = levelDatas[0].speed;
                        maxShotSpeed = levelDatas[0].maxShotSpeed;
                        speed = levelDatas[0].speed;
                        enemyExp = levelDatas[0].enemyExp;
                        break;
                    case 1:
                        health = levelDatas[1].health;
                        speed = levelDatas[1].speed;
                        maxShotSpeed = levelDatas[1].maxShotSpeed;
                        speed = levelDatas[1].speed;
                        enemyExp = levelDatas[1].enemyExp;
                        break;
                    case 2:
                        health = levelDatas[2].health;
                        speed = levelDatas[2].speed;
                        maxShotSpeed = levelDatas[2].maxShotSpeed;
                        speed = levelDatas[2].speed;
                        enemyExp = levelDatas[2].enemyExp;
                        break;
                    case 3:
                        health = levelDatas[3].health;
                        speed = levelDatas[3].speed;
                        maxShotSpeed = levelDatas[3].maxShotSpeed;
                        speed = levelDatas[3].speed;
                        enemyExp = levelDatas[3].enemyExp;
                        break;
                    case 4:
                        health = levelDatas[4].health;
                        speed = levelDatas[4].speed;
                        maxShotSpeed = levelDatas[4].maxShotSpeed;
                        speed = levelDatas[4].speed;
                        enemyExp = levelDatas[4].enemyExp;
                        break;
                    case 5:
                        health = levelDatas[5].health;
                        speed = levelDatas[5].speed;
                        maxShotSpeed = levelDatas[5].maxShotSpeed;
                        speed = levelDatas[5].speed;
                        enemyExp = levelDatas[5].enemyExp;
                        break;
                    case 6:
                        health = levelDatas[6].health;
                        speed = levelDatas[6].speed;
                        maxShotSpeed = levelDatas[6].maxShotSpeed;
                        speed = levelDatas[6].speed;
                        enemyExp = levelDatas[6].enemyExp;
                        break;
                    case 7:
                        health = levelDatas[7].health;
                        speed = levelDatas[7].speed;
                        maxShotSpeed = levelDatas[7].maxShotSpeed;
                        speed = levelDatas[7].speed;
                        enemyExp = levelDatas[7].enemyExp;
                        break;
                    case 8:
                        health = levelDatas[8].health;
                        speed = levelDatas[8].speed;
                        maxShotSpeed = levelDatas[8].maxShotSpeed;
                        speed = levelDatas[8].speed;
                        enemyExp = levelDatas[8].enemyExp;
                        break;
                    case 9:
                        health = levelDatas[9].health;
                        speed = levelDatas[9].speed;
                        maxShotSpeed = levelDatas[9].maxShotSpeed;
                        speed = levelDatas[9].speed;
                        enemyExp = levelDatas[9].enemyExp;
                        break;
                }
                break;
            case "S":
                switch (enemyLevel)
                {
                    case 0:
                        health = levelDatas[0].health;
                        speed = levelDatas[0].speed;
                        maxShotSpeed = levelDatas[0].maxShotSpeed;
                        speed = levelDatas[0].speed;
                        enemyExp = levelDatas[0].enemyExp;
                        break;
                    case 1:
                        health = levelDatas[1].health;
                        speed = levelDatas[1].speed;
                        maxShotSpeed = levelDatas[1].maxShotSpeed;
                        speed = levelDatas[1].speed;
                        enemyExp = levelDatas[1].enemyExp;
                        break;
                    case 2:
                        health = levelDatas[2].health;
                        speed = levelDatas[2].speed;
                        maxShotSpeed = levelDatas[2].maxShotSpeed;
                        speed = levelDatas[2].speed;
                        enemyExp = levelDatas[2].enemyExp;
                        break;
                    case 3:
                        health = levelDatas[3].health;
                        speed = levelDatas[3].speed;
                        maxShotSpeed = levelDatas[3].maxShotSpeed;
                        speed = levelDatas[3].speed;
                        enemyExp = levelDatas[3].enemyExp;
                        break;
                    case 4:
                        health = levelDatas[4].health;
                        speed = levelDatas[4].speed;
                        maxShotSpeed = levelDatas[4].maxShotSpeed;
                        speed = levelDatas[4].speed;
                        enemyExp = levelDatas[4].enemyExp;
                        break;
                    case 5:
                        health = levelDatas[5].health;
                        speed = levelDatas[5].speed;
                        maxShotSpeed = levelDatas[5].maxShotSpeed;
                        speed = levelDatas[5].speed;
                        enemyExp = levelDatas[5].enemyExp;
                        break;
                    case 6:
                        health = levelDatas[6].health;
                        speed = levelDatas[6].speed;
                        maxShotSpeed = levelDatas[6].maxShotSpeed;
                        speed = levelDatas[6].speed;
                        enemyExp = levelDatas[6].enemyExp;
                        break;
                    case 7:
                        health = levelDatas[7].health;
                        speed = levelDatas[7].speed;
                        maxShotSpeed = levelDatas[7].maxShotSpeed;
                        speed = levelDatas[7].speed;
                        enemyExp = levelDatas[7].enemyExp;
                        break;
                    case 8:
                        health = levelDatas[8].health;
                        speed = levelDatas[8].speed;
                        maxShotSpeed = levelDatas[8].maxShotSpeed;
                        speed = levelDatas[8].speed;
                        enemyExp = levelDatas[8].enemyExp;
                        break;
                    case 9:
                        health = levelDatas[9].health;
                        speed = levelDatas[9].speed;
                        maxShotSpeed = levelDatas[9].maxShotSpeed;
                        speed = levelDatas[9].speed;
                        enemyExp = levelDatas[9].enemyExp;
                        break;
                }
                break;
        }
    }
    void Update()
    {
        // 매 FixedUpdate 마다 Move() 호출
        Skill();
        SkillReload();
        enemyLevel = gameManager.difference;
    }
    public void OnHit(int dmg)
    {
        // 적의 체력이 이미 0 이하이면 아무 작업도 수행하지 않음
        if (health <= 0)
            return;

        // 받은 데미지를 체력에서 차감
        health -= dmg;
        spriteRenderer.sprite = sprites[1];
        // 0.1초 후 원래 스프라이트로 복구
        Invoke("ReturnSprite", 0.1f);

        if (health <= 0)
        {
            Player playerLogic = player.GetComponent<Player>();
            playerLogic.Exp += enemyExp;
            int ran = Random.Range(0, 10);
            if (ran < 3)
            {
                //Not Item 30%
                Debug.Log("Not Item");
            }
            else if (ran < 6)
            {
                //Coin 30%
                GameObject itemSkill = objectManager.MakeObj("itemSkill");
                itemSkill.transform.position = transform.position;
                Rigidbody2D rigid= itemSkill.GetComponent<Rigidbody2D>();
                rigid.velocity = Vector2.down * 1.5f;
            }
            else if (ran < 8)
            {
                //Power 20%
                GameObject itemExp = objectManager.MakeObj("itemExp");
                itemExp.transform.position = transform.position;
                Rigidbody2D rigid = itemExp.GetComponent<Rigidbody2D>();
                rigid.velocity = Vector2.down * 1.5f;
            }
            else if (ran < 10)
            {
                //Boom 20%
                GameObject itemHealth = objectManager.MakeObj("itemHealth");
                itemHealth.transform.position = transform.position;
                Rigidbody2D rigid = itemHealth.GetComponent<Rigidbody2D>();
                rigid.velocity = Vector2.down * 1.5f;
            }
            // 적 비활성화 및 초기화
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
        }
    }
    void Skill()
    {
        if (transform.position.y <= -5)
            return;
        if (curShotSpeed < maxShotSpeed)
            return;
        if (enemyName == "S")
        {
            switch (enemyLevel)
            {
                case 1:
                    GameObject bullet = objectManager.MakeObj("BulletEnemyA");
                    bullet.transform.position = transform.position;
                    Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
                    Vector3 dirVec = player.transform.position - transform.position;
                    rigidbody.AddForce(dirVec.normalized * 10, ForceMode2D.Impulse);
                    break;
                case 2:
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
                    break;
            }
        }
        else if (enemyName == "L")
        {
            switch (enemyLevel)
            {
                case 1:
                    GameObject bullet = objectManager.MakeObj("BulletEnemyA");
                    bullet.transform.position = transform.position;
                    Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
                    Vector3 dirVec = player.transform.position - transform.position;
                    rigidbody.AddForce(dirVec.normalized * 10, ForceMode2D.Impulse);
                    break;
                case 2:
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
                    break;
            }
        }
        curShotSpeed = 0;
    }
    void SkillReload()
    {
        curShotSpeed += Time.deltaTime;
    }
    void CreatSkill1Bullet(string type, Vector3 offset)
    {

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
        }
        else if (collision.gameObject.tag == "PlayerBullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                OnHit(bullet.dmg); // 먼저 데미지를 적용하고 Health를 감소시킴
                Debug.Log("hit");
                // 체력이 0 이하일 경우 적을 비활성화
                if (health <= 0)
                {
                    gameObject.SetActive(false);
                    transform.rotation = Quaternion.identity;
                }
            }
        }
    }
}
[System.Serializable]
public class LevelData
{
    public int health;
    public float maxShotSpeed;
    public float speed;//이동속도
    public int enemyExp;
}
