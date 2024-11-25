using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    public string enemyName;
    public float speed;//�̵��ӵ�
    public int health;//ü��
    public Sprite[] sprites;
    public float curShotSpeed;//�����ӵ�
    public float maxShotSpeed;//�ִ� �����ӵ�
    public float enemyBulletSpeed;//�� ����ü �ӵ�
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
    // �� ������Ʈ�� Ȱ��ȭ�� �� �ʱ�ȭ �۾� ����
    void OnEnable()
    {
        // �� �̸��� ���� ü���� ����
        switch (enemyName)
        {
            case "L":
                health = 20; // ū ��
                break;
            case "M":
                health = 15; // �߰� ��
                break;
            case "S":
                health = 10;  // ���� ��
                break;
        }
    }
    void Update()
    {
        // �� FixedUpdate ���� Move() ȣ��
        Skill();
        SkillReload();
    }
    public void OnHit(int dmg)
    {
        // ���� ü���� �̹� 0 �����̸� �ƹ� �۾��� �������� ����
        if (health <= 0)
            return;

        // ���� �������� ü�¿��� ����
        health -= dmg;
        spriteRenderer.sprite = sprites[1];
        // 0.1�� �� ���� ��������Ʈ�� ����
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
            // �� ��Ȱ��ȭ �� �ʱ�ȭ
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
                OnHit(bullet.dmg); // ���� �������� �����ϰ� Health�� ���ҽ�Ŵ
                Debug.Log("hit");
                // ü���� 0 ������ ��� ���� ��Ȱ��ȭ
                if (health <= 0)
                {
                    gameObject.SetActive(false);
                    transform.rotation = Quaternion.identity;
                }
            }
        }
    }
}
