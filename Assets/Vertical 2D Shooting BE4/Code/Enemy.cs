using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    public string enemyName;
    public float speed;//이동속도
    public int health;//체력
    public Sprite[] sprites;
    public float curShotSpeed;//장전속도
    public float maxShotSpeed;//최대 장전속도
    public float enemyBulletSpeed;//적 투사체 속도
    public int enemyLevel;
    public int enemyExp;

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
        // 적 이름에 따라 체력을 설정
        switch (enemyName)
        {
            case "L":
                health = 20; // 큰 적
                break;
            case "M":
                health = 15; // 중간 적
                break;
            case "S":
                health = 10;  // 작은 적
                break;
        }
    }
    void Update()
    {
        // 매 FixedUpdate 마다 Move() 호출
        Skill();
        SkillReload();
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
                GameObject itemCoin = objectManager.MakeObj("itemSkill");
                itemCoin.transform.position = transform.position;
            }
            else if (ran < 8)
            {
                //Power 20%
                GameObject itemPower = objectManager.MakeObj("itemExp");
                itemPower.transform.position = transform.position;
            }
            else if (ran < 10)
            {
                //Boom 20%
                GameObject itemBoom = objectManager.MakeObj("itemHealth");
                itemBoom.transform.position = transform.position;
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
