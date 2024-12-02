using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class Enemy : MonoBehaviour
{
    public static Enemy instance;
    public LevelData[] levelDatas;
    public string enemyName;
    public float speed;//�̵��ӵ�
    public int health;//ü��
    public Sprite[] sprites;
    public float curShotSpeed;//�����ӵ�
    public float maxShotSpeed;//�ִ� �����ӵ�
    public float enemyBulletSpeed;//�� ����ü �ӵ�
    public int enemyExp;
    public int enemyLevel;

    public bool isClear; 

    public GameObject player;

    SpriteRenderer spriteRenderer;
    Animator anim;

    public ObjectManager objectManager;
    public GameManager gameManager;

    // ���� ������ �����ϱ� ���� ����
    public int patternIndex; // ���� ���� �ε���
    public int curPatternCount; // ���� ���� �ݺ� Ƚ��
    public int[] maxPatternCount; // �� ������ �ִ� �ݺ� Ƚ��


    void Awake()
    {
        instance = this;
        spriteRenderer = GetComponent<SpriteRenderer>();


        // �� �̸��� "B"�� ��� �ִϸ����� ������Ʈ �������� (������ �ش�)
        if (enemyName == "B")
            anim = GetComponent<Animator>();
    }
    // �� ������Ʈ�� Ȱ��ȭ�� �� �ʱ�ȭ �۾� ����
    void OnEnable()
    {
        if (gameManager == null)
            gameManager = FindObjectOfType<GameManager>();

        enemyLevel = gameManager.difficulty;

        // ü���� ������ ���� �ʱ�ȭ
        switch (enemyName)
        {
            case "L":
                health = levelDatas[enemyLevel].health;
                speed = levelDatas[enemyLevel].speed;
                maxShotSpeed = levelDatas[enemyLevel].maxShotSpeed;
                enemyExp = levelDatas[enemyLevel].enemyExp;
                break;
            case "EL":
                health = levelDatas[enemyLevel].health;
                speed = levelDatas[enemyLevel].speed;
                maxShotSpeed = levelDatas[enemyLevel].maxShotSpeed;
                enemyExp = levelDatas[enemyLevel].enemyExp;
                break;
            case "M":
                health = levelDatas[enemyLevel].health;
                speed = levelDatas[enemyLevel].speed;
                maxShotSpeed = levelDatas[enemyLevel].maxShotSpeed;
                enemyExp = levelDatas[enemyLevel].enemyExp;
                break;
            case "EM":
                health = levelDatas[enemyLevel].health;
                speed = levelDatas[enemyLevel].speed;
                maxShotSpeed = levelDatas[enemyLevel].maxShotSpeed;
                enemyExp = levelDatas[enemyLevel].enemyExp;
                break;
            case "S":
                health = levelDatas[enemyLevel].health;
                speed = levelDatas[enemyLevel].speed;
                maxShotSpeed = levelDatas[enemyLevel].maxShotSpeed;
                enemyExp = levelDatas[enemyLevel].enemyExp;
                break;
            case "ES":
                health = levelDatas[enemyLevel].health;
                speed = levelDatas[enemyLevel].speed;
                maxShotSpeed = levelDatas[enemyLevel].maxShotSpeed;
                enemyExp = levelDatas[enemyLevel].enemyExp;
                break;
            case "B":
                health = 30; // ���� ü�� ����
                Invoke("Stop", 1.6f);
                break;
        }

        curShotSpeed = 0; // ���� �ӵ� �ʱ�ȭ
    }
    // ���� ���� ���߰� �ϴ� �Լ�
    void Stop()
    {
        Debug.Log("Stop����Ϸ�");
        if (!gameObject.activeSelf) return;

        // Rigidbody2D�� �̿��� ���� �ӵ��� 0���� ����
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.zero;

        // ���� ��ȯ�� ���� Think �Լ� ȣ��
        Invoke("Think", 2.5f);
    }

    // ���� ���� ������ �����ϴ� �Լ�
    void Think()
    {
        Debug.Log("Think����Ϸ�");
        if (!GameManager.instance.isLive)
        {
            return;
        }
        // ���� �ε��� ��ȯ (0~3)
        patternIndex = patternIndex == 3 ? 0 : patternIndex + 1;
        curPatternCount = 0; // ���� ���� ī��Ʈ�� �ʱ�ȭ
        Debug.Log("Think����Ϸ�2");
        // ���Ͽ� ���� �ٸ� ���� ����
        switch (patternIndex)
        {
            case 0:
                FireForward(); // ���� �߻�
                break;
            case 1:
                FireShot(); // ���� ���� �߻�
                break;
            case 2:
                FireArc(); // ��ä�� ���� �߻�
                break;
            case 3:
                FireAround(); // ���� �߻�
                break;
        }
        Debug.Log("Think����Ϸ�3");
    }

    // ���� �߻� ����
    void FireForward()
    {
        if (health <= 0) return;

        Debug.Log("������ 4�� �߻�.");
        // ���� ��ġ�� �������� �¿쿡 �Ѿ� ����
        GameObject bulletR = objectManager.MakeObj("bulletBossA");
        bulletR.transform.position = transform.position + Vector3.right * 0.4f;
        GameObject bulletRR = objectManager.MakeObj("bulletBossA");
        bulletRR.transform.position = transform.position + Vector3.right * 1.2f;
        GameObject bulletL = objectManager.MakeObj("bulletBossA");
        bulletL.transform.position = transform.position + Vector3.left * 0.4f;
        GameObject bulletLL = objectManager.MakeObj("bulletBossA");
        bulletLL.transform.position = transform.position + Vector3.left * 1.2f;

        // ������ �Ѿ˿� ���� ���� �Ʒ� �������� �̵�
        Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();

        rigidR.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidRR.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidL.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidLL.AddForce(Vector2.down * 8, ForceMode2D.Impulse);

        // ���� �ݺ� ����
        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireForward", 2);
        else
            Invoke("Think", 2);
    }

    // ���� ���� (�÷��̾� �������� ���� �߻�ü)
    void FireShot()
    {
        if (health <= 0) return;

        Debug.Log("�÷��̾� �������� ����.");
        for (int index = 0; index < 5; index++)
        { 
            // �Ѿ� ����
            GameObject bullet = objectManager.MakeObj("bulletBossB");
            bullet.transform.position = transform.position;

            Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
            // �÷��̾� �������� �ణ ������ ���� �߰�
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

    // ��ä�� ����
    void FireArc()
    {
        if (health <= 0) return;

        Debug.Log("��ä������� �߻�");
        GameObject bullet = objectManager.MakeObj("bulletEnemyB");
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.identity;

        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
        // ������ ����Ͽ� ��ä�� ���·� �Ѿ� �߻�
        Vector3 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 10 * curPatternCount / maxPatternCount[patternIndex]), -1);
        rigidbody.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);

        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireArc", 0.15f);
        else
            Invoke("Think", 3);
    }

    // ���� �߻� ����
    void FireAround()
    {
        if (health <= 0) return;
        int roundNum = curPatternCount % 2 == 0 ? 50 : 40; // �߻�ü �� ����
        CreateAround("bulletEnemyB");
        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireAround", 0.7f);
        else
            Invoke("Think", 3);
    }
    void CreateAround(string type)
    {
        int roundNum = curPatternCount % 2 == 0 ? 50 : 40; // �߻�ü �� ����
        for (int index = 0; index < roundNum; index++)
        {
            GameObject bullet = objectManager.MakeObj(type);
            bullet.transform.position = transform.position;
            Vector3 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * index / roundNum), Mathf.Sin(Mathf.PI * 2 * index / roundNum));
            bullet.GetComponent<Rigidbody2D>().AddForce(dirVec.normalized * 2, ForceMode2D.Impulse);
        }
    }
    void Update()
    {
        if (enemyName == "B") return; // ������ ���� ����
        if (!GameManager.instance.isLive)
        {
            return;
        }
        // �� FixedUpdate ���� Move() ȣ��
        Skill();
        SkillReload();
        enemyLevel = gameManager.difficulty;
    }
    public void OnHit(int dmg)
    {
        // ���� ü���� �̹� 0 �����̸� �ƹ� �۾��� �������� ����
        if (health <= 0)
            return;

        // ���� �������� ü�¿��� ����
        health -= dmg;

        // ���� ���� ��� �ִϸ����� Ʈ���� ȣ��
        if (enemyName == "B")
        {
            anim.SetTrigger("OnHit");
        }
        else
        {
            // �Ϲ� ���� ��� ���� ������ ��������Ʈ�� ����
            spriteRenderer.sprite = sprites[1];
            // 0.1�� �� ���� ��������Ʈ�� ����
            Invoke("ReturnSprite", 0.1f);
        }
        AudioManager.instance.PlaySfx(AudioManager.Sfx.EnemyHit);

        if (health <= 0)
        {
            health = 0; // ü���� 0���� ����
            gameManager.GetExp(enemyExp);
            DropItem();
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
            gameManager.CallExplosion(transform.position, enemyName);
            if(GameManager.instance.isLive)
                AudioManager.instance.PlaySfx(AudioManager.Sfx.EnemyDead);
            if (enemyName == "B")
            {
                GameManager.instance.StoryClear = true;
                Invoke("Clear", 5f);

            }
                
        }
    }
    void Clear()
    {
        GameManager.instance.isLive=false;
        gameManager.StageWin();
    }
    void DropItem()
    {
        int ran = enemyName == "B" ? 0 : Random.Range(0, 10);
        GameObject item = null;

        if (ran < 6)
        {
            Debug.Log("Not Item");
        }
        else if (ran < 7)
        {
            Debug.Log("Attempting to create itemSkill...");
            item = objectManager.MakeObj("itemSkill");
        }
        else if (ran < 9)
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
        else if (enemyName == "ES")
        {
            switch (enemyLevel)
            {
                default:
                    CreateBullet("bulletEnemyB", Vector3.right * 0.2f);
                    CreateBullet("bulletEnemyB", Vector3.left * 0.2f);
                    break;
            }
        }
        else if (enemyName == "EL")
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
    void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        // ���� Ư�� �±׸� ���� ������Ʈ�� �浹���� �� ó��
        if (collision.gameObject.tag == "Borderbullet" && enemyName != "B")
        {
            // ���� ȭ�� ��踦 ��� ��� ��Ȱ��ȭ
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
            gameManager.CallExplosion(transform.position, "P");
        }
        else if (collision.gameObject.tag == "PlayerBullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                collision.gameObject.SetActive(false);
                OnHit(bullet.dmg); // ���� �������� �����ϰ� Health�� ���ҽ�Ŵ

                // ü���� 0 ������ ��� ���� ��Ȱ��ȭ
                if (bullet != null)
                {
                    OnHit(bullet.dmg);
                }
                else
                {
                    Debug.LogWarning("�浹�� ������Ʈ�� Bullet ������Ʈ�� �����ϴ�.");
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
    public float speed;//�̵��ӵ�
    public int enemyExp;
}
