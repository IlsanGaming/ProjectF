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
        if (gameManager == null)
            gameManager = FindObjectOfType<GameManager>();
        enemyLevel = gameManager.difficulty;
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
        enemyLevel = gameManager.difficulty;
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
            gameManager.GetExp();
            DropItem();
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
        }
    }
    void DropItem()
    {
        int ran = Random.Range(0, 10);
        GameObject item = null;

        if (ran < 3)
        {
            Debug.Log("Not Item");
        }
        else if (ran < 6)
        {
            Debug.Log("Attempting to create itemSkill...");
            item = objectManager.MakeObj("itemSkill");
        }
        else if (ran < 8)
        {
            Debug.Log("Attempting to create itemExp...");
            item = objectManager.MakeObj("itemExp");
        }
        else if (ran < 10)
        {
            Debug.Log("Attempting to create itemHealth...");
            item = objectManager.MakeObj("itemHealth");
        }

        if (item != null)
        {
            item.transform.position = transform.position;
            Rigidbody2D rigid = item.GetComponent<Rigidbody2D>();
            if (rigid != null)
            {
                rigid.velocity = Vector2.down * 1.5f;
            }
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
                    CreateBullet("bulletEnemyA", Vector3.zero);
                    break;
                default:
                    CreateBullet("bulletEnemyA", Vector3.right * 0.1f);
                    CreateBullet("bulletEnemyA", Vector3.left * 0.1f);
                    break;
            }
        }
        else if (enemyName == "L")
        {
            switch (enemyLevel)
            {
                default:
                    CreateBullet("bulletEnemyB", Vector3.right * 0.2f);
                    CreateBullet("bulletEnemyB", Vector3.left * 0.2f);
                    break;
            }
        }
        curShotSpeed = 0;
    }
    void CreateBullet(string type, Vector3 offset)
    {
        GameObject bullet = objectManager.MakeObj(type);
        Vector3 dirVec = player.transform.position - transform.position;
        bullet.transform.position = transform.position + offset;
        bullet.GetComponent<Rigidbody2D>().AddForce(dirVec.normalized * 10, ForceMode2D.Impulse);
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
                // 체력이 0 이하일 경우 적을 비활성화
                if (bullet != null)
                {
                    OnHit(bullet.dmg);
                }
                else
                {
                    Debug.LogWarning("충돌한 오브젝트에 Bullet 컴포넌트가 없습니다.");
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
