using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
public class Enemy : MonoBehaviour
{
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
            case "B":
                health = 10;
                Invoke("Stop", 2.5f);
                break;

        }
    }

    // ���� ���� ���߰� �ϴ� �Լ�
    void Stop()
    {
        if (!gameObject.activeSelf) return;

        // Rigidbody2D�� �̿��� ���� �ӵ��� 0���� ����
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.zero;

        // ���� ��ȯ�� ���� Think �Լ� ȣ��
        Invoke("Think", 2);
    }

    // ���� ���� ������ �����ϴ� �Լ�
    void Think()
    {
        // ���� �ε��� ��ȯ (0~3)
        patternIndex = patternIndex == 3 ? 0 : patternIndex + 1;
        curPatternCount = 0; // ���� ���� ī��Ʈ�� �ʱ�ȭ

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
    }

    // ���� �߻� ����
    void FireForward()
    {
        if (health <= 0) return;

        Debug.Log("������ 4�� �߻�.");
        // ���� ��ġ�� �������� �¿쿡 �Ѿ� ����
        GameObject bulletR = objectManager.MakeObj("bulletBossA");
        bulletR.transform.position = transform.position + Vector3.right * 0.3f;
        GameObject bulletRR = objectManager.MakeObj("bulletBossA");
        bulletRR.transform.position = transform.position + Vector3.right * 0.45f;
        GameObject bulletL = objectManager.MakeObj("bulletBossA");
        bulletL.transform.position = transform.position + Vector3.left * 0.3f;
        GameObject bulletLL = objectManager.MakeObj("bulletBossA");
        bulletLL.transform.position = transform.position + Vector3.left * 0.45f;

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

        Debug.Log("�� ���·� ��ü ����");
        int roundNum = curPatternCount % 2 == 0 ? 50 : 40; // �߻�ü �� ����
        for (int index = 0; index < roundNum; index++)
        {
            GameObject bullet = objectManager.MakeObj("bulletEnemyA");
            bullet.transform.position = transform.position;

            Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
            // �������� �߻�ü ���� ���
            Vector3 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * index / roundNum), Mathf.Sin(Mathf.PI * 2 * index / roundNum));
            rigidbody.AddForce(dirVec.normalized * 2, ForceMode2D.Impulse);
        }

        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireAround", 0.7f);
        else
            Invoke("Think", 3);
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

        if (health <= 0)
        {
            gameManager.GetExp();
            DropItem();
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
            gameManager.CallExplosion(transform.position, enemyName);
            // ���� ���� óġ�� ��� �������� ���� ó��
            if (enemyName == "B")
            {
                gameManager.StageEnd();
                gameManager.storyClear = true;
            }
        }
    }
    void DropItem()
    {
        int ran = enemyName == "B" ? 0 : Random.Range(0, 10);
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
