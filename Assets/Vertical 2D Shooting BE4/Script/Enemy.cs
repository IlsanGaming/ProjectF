using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // ���� �̸� (e.g., "S", "M", "L", "B"�� ����)
    public string enemyname;
    // ���� óġ���� �� �÷��̾ ��� ����
    public int enemyscore;

    // ���� �̵� �ӵ��� ü��
    public float speed;
    public int health;

    // ���� ��������Ʈ �迭 (�⺻ �� ��Ʈ ���� ��)
    public Sprite[] sprites;

    // ���� ��������Ʈ �������� �ִϸ����͸� ���� ����
    SpriteRenderer spriteRenderer;
    Animator anim;

    // ���� ����� �Ѿ� ������Ʈ
    public GameObject bulletObjA;
    public GameObject bulletObjB;

    // �÷��̾� ������Ʈ�� �����ϱ� ���� ����
    public GameObject player;

    // ������Ʈ �� ���� �����ڸ� �����ϱ� ���� ����
    public ObjectManager objectManager;
    public GameManager gameManager;

    // ������ ����� ���� ������Ʈ
    public GameObject itemCoin;
    public GameObject itemBoom;
    public GameObject itemPower;

    // �Ѿ� �߻� ������ ����
    public float MaxShotDelay;
    public float CurShotDelay;

    // ���� ������ �����ϱ� ���� ����
    public int patternIndex; // ���� ���� �ε���
    public int curPatternCount; // ���� ���� �ݺ� Ƚ��
    public int[] maxPatternCount; // �� ������ �ִ� �ݺ� Ƚ��

    // Awake �Լ��� �� ������Ʈ�� Ȱ��ȭ�� �� �ʱ�ȭ �۾��� ����
    void Awake()
    {
        // ��������Ʈ ������ ������Ʈ ��������
        spriteRenderer = GetComponent<SpriteRenderer>();

        // �� �̸��� "B"�� ��� �ִϸ����� ������Ʈ �������� (������ �ش�)
        if (enemyname == "B")
            anim = GetComponent<Animator>();
    }

    // �� ������Ʈ�� Ȱ��ȭ�� �� �ʱ�ȭ �۾� ����
    void OnEnable()
    {
        // �� �̸��� ���� ü���� ����
        switch (enemyname)
        {
            case "L":
                health = 40; // ū ��
                break;
            case "M":
                health = 10; // �߰� ��
                break;
            case "S":
                health = 3;  // ���� ��
                break;
            case "B":
                health = 100; // ���� ��
                Invoke("Stop", 2); // �ʱ� 2�� �� ���� ���·� ��ȯ
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
        GameObject bulletR = objectManager.MakeObj("BulletBossA");
        bulletR.transform.position = transform.position + Vector3.right * 0.3f;
        GameObject bulletRR = objectManager.MakeObj("BulletBossA");
        bulletRR.transform.position = transform.position + Vector3.right * 0.45f;
        GameObject bulletL = objectManager.MakeObj("BulletBossA");
        bulletL.transform.position = transform.position + Vector3.left * 0.3f;
        GameObject bulletLL = objectManager.MakeObj("BulletBossA");
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
            GameObject bullet = objectManager.MakeObj("BulletBossB");
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
        GameObject bullet = objectManager.MakeObj("BulletEnemyA");
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
            GameObject bullet = objectManager.MakeObj("BulletBossB");
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

    // ������Ʈ �Լ� (���� ���¸� �� ������ ����)
    void Update()
    {
        if (enemyname == "B") return; // ������ ���� ����

        Fire();   // �Ϲ� �߻� ó��
        Reload(); // �߻� ������ ����
    }

    // �߻� ó��
    void Fire()
    {
        if (CurShotDelay < MaxShotDelay) return; // �����̰� ���� ���� ������ ����

        // ���� �̸��� ���� �ٸ� �߻� ���
        if (enemyname == "S") { /* ���� �� ���� */ }
        else if (enemyname == "L") { /* ū �� ���� */ }
        CurShotDelay = 0;
    }

    // �߻� ������ ����
    void Reload()
    {
        CurShotDelay += Time.deltaTime;
    }
    // ���� �¾��� �� ó��
    public void OnHit(int dmg)
    {
        // ���� ü���� �̹� 0 �����̸� �ƹ� �۾��� �������� ����
        if (health <= 0)
            return;

        // ���� �������� ü�¿��� ����
        health -= dmg;

        // ���� ���� ��� �ִϸ����� Ʈ���� ȣ��
        if (enemyname == "B")
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

        // ü���� 0 ���Ϸ� ������ ��� ó��
        if (health <= 0)
        {
            // �÷��̾� ���� ����
            Player playerLogic = player.GetComponent<Player>();
            playerLogic.score += enemyscore;

            // ���� �������� ����߸� Ȯ�� ��� (������ �׻� �������� ������� ����)
            int ran = enemyname == "B" ? 0 : Random.Range(0, 10);
            if (ran < 3)
            {
                // 30% Ȯ���� �ƹ� �����۵� ������� ����
                Debug.Log("Not Item");
            }
            else if (ran < 6)
            {
                // 30% Ȯ���� ���� ������ ���
                GameObject itemCoin = objectManager.MakeObj("ItemCoin");
                itemCoin.transform.position = transform.position;
            }
            else if (ran < 8)
            {
                // 20% Ȯ���� �Ŀ��� ������ ���
                GameObject itemPower = objectManager.MakeObj("ItemPower");
                itemPower.transform.position = transform.position;
            }
            else if (ran < 10)
            {
                // 20% Ȯ���� ��ź ������ ���
                GameObject itemBoom = objectManager.MakeObj("ItemBoom");
                itemBoom.transform.position = transform.position;
            }

            // �� ��Ȱ��ȭ �� �ʱ�ȭ
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;

            // ���� ����Ʈ�� ȣ��
            gameManager.CallExplosion(transform.position, enemyname);

            // ���� ���� óġ�� ��� �������� ���� ó��
            if (enemyname == "B")
            {
                gameManager.StageEnd();
            }
        }
    }

    // �� ��������Ʈ�� ���� ���·� ����
    void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }

    // �浹 ó�� �Լ�
    void OnTriggerEnter2D(Collider2D collision)
    {
        // ���� Ư�� �±׸� ���� ������Ʈ�� �浹���� �� ó��
        if (collision.gameObject.tag == "Borderbullet" && enemyname != "B")
        {
            // ���� ȭ�� ��踦 ��� ��� ��Ȱ��ȭ
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
        }
        else if (collision.gameObject.tag == "PlayerBullet")
        {
            // �÷��̾��� �Ѿ˰� �浹���� ���
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                // �������� ����
                OnHit(bullet.dmg);
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
