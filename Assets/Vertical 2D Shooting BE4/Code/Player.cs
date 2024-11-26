using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEditor.Progress;

public class Player : MonoBehaviour
{

    public int skill1Level;
    public int skill2Level;
    public int skill3Level;
    public int skill4Level;
    public int skill5Stack;


    public int maxskill1Level;
    public int maxskill2Level;
    public int maxskill3Level;
    public int maxskill4Level;
    public int maxskill5Stack;

    public float health;
    public float maxhealth;
    public float speed;//�̵��ӵ�  
    public float curVerticalBulletSpeed;//����ü �ӵ�
    public float maxVerticalBulletSpeed;//�ִ� ����ü �ӵ�
    public float curShotSpeed;//�����ӵ�
    public float maxShotSpeed;//�ִ� �����ӵ�
    public float lightness;//�÷��̾ ��, �� �Ѿ˿� ������� ��ȭ�ϴ� ����
    public int Exp;


    // ȭ�� ������ �浹 ���¸� ��Ÿ���� ����
    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchRight;
    public bool isTouchLeft;

    public bool isSkill5;

    public GameManager gameManager;
    public ObjectManager objectManager;
    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public GameObject boomEffect;
    public GameObject[] followers;
    Rigidbody2D rigid;


    Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // �� FixedUpdate ���� Move() ȣ��
        Move();
        Skill1();
        Skill1Reload();
        Skill2();
        //Skill2Reload();
        //Skill3();
        //Skill3Reload();
        //Skill4();
        //Skill4Reload();
        Skill5();
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if ((isTouchRight && h == -1) || (isTouchLeft && h == 1))
            h = 0;
        if ((isTouchTop && v == 1) || (isTouchBottom && v == -1))
            v = 0;
        Vector3 curPos = transform.position;
        Vector3 nextVec = new Vector3(h, v, 0) * speed * Time.deltaTime;
        transform.position = curPos + nextVec;

        if (Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal"))
            anim.SetInteger("Input", (int)h);
    }
    void Skill1()
    {
        if(curShotSpeed < maxShotSpeed)
            return;
        switch(skill1Level)
        {
            case 1:
                GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.up * curVerticalBulletSpeed, ForceMode2D.Impulse);
                break;
            case 2:
                GameObject bulletR = Instantiate(bulletObjA, transform.position + Vector3.right * 0.1f, transform.rotation);
                GameObject bulletL = Instantiate(bulletObjA, transform.position + Vector3.left * 0.1f, transform.rotation);
                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
                rigidR.AddForce(Vector2.up * curVerticalBulletSpeed, ForceMode2D.Impulse);
                rigidL.AddForce(Vector2.up * curVerticalBulletSpeed, ForceMode2D.Impulse);
                break;
            case 3:
                GameObject bulletRR = Instantiate(bulletObjA, transform.position + Vector3.right * 0.35f, transform.rotation);
                GameObject bulletCC = Instantiate(bulletObjB, transform.position, transform.rotation);
                GameObject bulletLL = Instantiate(bulletObjA, transform.position + Vector3.left * 0.35f, transform.rotation);
                Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidCC = bulletCC.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();
                rigidRR.AddForce(Vector2.up * curVerticalBulletSpeed, ForceMode2D.Impulse);
                rigidCC.AddForce(Vector2.up * curVerticalBulletSpeed, ForceMode2D.Impulse);
                rigidLL.AddForce(Vector2.up * curVerticalBulletSpeed, ForceMode2D.Impulse);
                break;
        }
        curShotSpeed = 0;
    }
    void Skill1Reload()
    {
        curShotSpeed += Time.deltaTime;
    }
    void CreatSkill1Bullet(string type,Vector3 offset)
    {

    }
    void Skill2()
    {
        switch (skill2Level)
        {
            case 1:
                followers[0].SetActive(true);
                break;
            case 2:
                followers[1].SetActive(true);
                break;
            case 3:
                followers[2].SetActive(true);
                break;
            case 4:
                for(int index=0;index<followers.Length;index++)
                {
                    Follower followerLogic= followers[index].GetComponent<Follower>();
                    followerLogic.MaxShotDelay = 1;
                }
                
                break;
        }

    }
    void Skill5()
    {
        if (!Input.GetButton("Fire2"))
            return;
        if(isSkill5)
            return;
        if (skill5Stack==0)
            return;
        skill5Stack--;
        isSkill5 = true;
        gameManager.UpdateSkill5Icon(skill5Stack);
        boomEffect.SetActive(true);
        Invoke("OffBoomEffect", 1.5f);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int index = 0; index < enemies.Length; index++)
        {
            Enemy enemyLogic = enemies[index].GetComponent<Enemy>();
            enemyLogic.OnHit(1000);
        }
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        for (int index = 0; index < bullets.Length; index++)
        {
            RemoveEnemies();
        }
    }
    // �� ���� ó�� �Լ�
    void RemoveEnemies()
    {
        GameObject[] enemiesL = objectManager.GetPool("EnemyL");
        GameObject[] enemiesM = objectManager.GetPool("EnemyM");
        GameObject[] enemiesS = objectManager.GetPool("EnemyS");

        // �� �� ������Ʈ�� ���� ���� ó��
        foreach (var enemy in enemiesL)
            if (enemy.activeSelf) enemy.GetComponent<Enemy>().OnHit(1000);

        foreach (var enemy in enemiesM)
            if (enemy.activeSelf) enemy.GetComponent<Enemy>().OnHit(1000);

        foreach (var enemy in enemiesS)
            if (enemy.activeSelf) enemy.GetComponent<Enemy>().OnHit(1000);

        // �� �Ѿ� ����
        RemoveBullets("BulletEnemyA");
        RemoveBullets("BulletEnemyB");
    }
    // �� �Ѿ� ���� ó�� �Լ�
    void RemoveBullets(string type)
    {
        GameObject[] bullets = objectManager.GetPool(type);
        foreach (var bullet in bullets)
            if (bullet.activeSelf) bullet.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // ȭ�� ���� �浹
        if (collision.gameObject.tag == "Border")
        {
            SetBorderTouch(collision.gameObject.name, true);
            rigid.constraints = RigidbodyConstraints2D.FreezePosition;
        }
        else if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet")
        {
            OnDamaged(collision.transform.position);
        }
        else if (collision.gameObject.tag == "Item")
        {
            HandleItemPickup(collision.gameObject.GetComponent<Item>());
        }
    }
    // ������ ȹ�� ó��
    void HandleItemPickup(Item item)
    {
        switch (item.type)
        {
            case "Health":
                health = maxhealth;
                break;
            case "Exp":
                Exp++;
                break;
            case "Boom":
                if (skill5Stack == maxskill5Stack)
                {
                    Debug.Log("ExpGain");
                    Exp++;
                }
                else
                {
                    Debug.Log("skill5StackGain");
                    skill5Stack++;
                    gameManager.UpdateSkill5Icon(skill5Stack);
                }
                break;
        }
        Destroy(item.gameObject);
    }
    void OffBoomEffect()
    {
        boomEffect.SetActive(false);
        isSkill5 = false;
    }
    void OnDamaged(Vector3 targetPos)
    {
        Debug.Log("OnHit!");

        // ���� �ӵ��� �ʱ�ȭ�Ͽ� �з����� ȿ���� ��ӵ��� �ʵ��� ����
        rigid.velocity = Vector2.zero;

        // Ÿ�� ���� ���
        Vector3 dirVector = (transform.position - targetPos).normalized;

        // �з����� �� �߰�
        StartCoroutine(KnockBack(dirVector));

        // ü�� ����
        health -= 2;

        // ü���� 0���� �۾��� ��� ���� ���� ó��
        if (health <= 0)
        {
            Debug.Log("GameOver!");
            gameObject.SetActive(false);
            Time.timeScale = 0; // ���� �ð� ����
        }
    }
    IEnumerator KnockBack(Vector3 direction)
    {
        float knockBackDuration = 0.1f; // �з��� ���� �ð�
        float knockBackTimer = 0;

        while (knockBackTimer < knockBackDuration)
        {
            rigid.velocity = direction * lightness;
            knockBackTimer += Time.deltaTime;
            yield return null;
        }

        // �з��� �� �ӵ��� �ʱ�ȭ
        rigid.velocity = Vector2.zero;
    }

    // ȭ�� ��� �浹 ���� ����
    void SetBorderTouch(string borderName, bool state)
    {
        switch (borderName)
        {
            case "Top": isTouchTop = state; break;
            case "Bottom": isTouchBottom = state; break;
            case "Right": isTouchRight = state; break;
            case "Left": isTouchLeft = state; break;
        }
    }
    // ȭ�� ��踦 ��� �� ȣ��
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            SetBorderTouch(collision.gameObject.name, false);
            rigid.constraints = RigidbodyConstraints2D.None;
        }
    }
}
