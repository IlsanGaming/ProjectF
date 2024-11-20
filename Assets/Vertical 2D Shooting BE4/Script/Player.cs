using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // �÷��̾��� �̵� �ӵ��� ���� ���� ����
    public float speed; // �̵� �ӵ�
    public float MaxShotDelay; // �ִ� �߻� ������
    public float CurShotDelay; // ���� �߻� ������
    public int Power; // ���� �Ŀ� ����
    public int maxPower; // �ִ� �Ŀ� ����
    public int boom; // ���� ��ź ����
    public int maxboom; // �ִ� ��ź ����

    // �÷��̾��� �������� ����
    public int life; // ���� ������ ��
    public int score; // ���� ����

    // ȭ�� ������ �浹 ���¸� ��Ÿ���� ����
    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchLeft;
    public bool isTouchRight;

    // �÷��̾ �߻��ϴ� �Ѿ� �� ��ź ����Ʈ ������Ʈ
    public GameObject bulletObjA; // �⺻ �Ѿ�
    public GameObject bulletObjB; // ��ȭ �Ѿ�
    public GameObject boomEffect; // ��ź ����Ʈ

    // ���� �Ŵ��� �� ������Ʈ �Ŵ���
    public GameManager gameManager;
    public ObjectManager objectManager;

    // �÷��̾� ���� ���� ����
    public bool isHit; // ���� �÷��̾ �ǰ� �������� ����
    public bool isBoomTime; // ��ź ��� ���� ����

    // �÷��̾ ����ٴϴ� ���� ������Ʈ
    public GameObject[] followers;
    public bool isRespawnTime; // ������ �������� ����

    // �ִϸ��̼� �� ��������Ʈ ������
    Animator animator;
    SpriteRenderer spriteRenderer;

    // ���̽�ƽ �� ��Ʈ�� ���� ����
    public bool[] joyControl;
    public bool isControl;
    public bool isButtonA; // ���� ��ư ����
    public bool isButtonB; // ��ź ��ư ����

    // Awake: �ʱ�ȭ �۾��� ����
    void Awake()
    {
        // Animator�� SpriteRenderer ������Ʈ ��������
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // OnEnable: ������Ʈ Ȱ��ȭ �� ȣ��
    void OnEnable()
    {
        // ���� ���� Ȱ��ȭ
        Unbeatable();
        Invoke("Unbeatable", 3); // 3�� �� ���� ���� ����
    }
    void Unbeatable()
    {
        isRespawnTime = !isRespawnTime; // ���� ���¸� ���
        if (isRespawnTime)
        {
            // ���� ���¿��� ������ ó��
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);
            foreach (var follower in followers)
            {
                follower.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
            }
        }
        else
        {
            // ���� ���� ���� �� ���� �������� ����
            spriteRenderer.color = new Color(1, 1, 1, 1);
            foreach (var follower in followers)
            {
                follower.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
        }
    }
    // �� ������ ȣ��Ǵ� ������Ʈ �Լ�
    void Update()
    {
        Move();   // �̵� ó��
        Fire();   // �Ѿ� �߻�
        Boom();   // ��ź ���
        Reload(); // �߻� ������ ����
    }
    // ���̽�ƽ �Է� ó�� �Լ�
    public void JoyPanel(int type)
    {
        for (int index = 0; index < 9; index++)
        {
            joyControl[index] = index == type;
        }
    }
    public void JoyDown() { isControl = true; }
    public void JoyUp() { isControl = false; }

    // �÷��̾� �̵� ó�� �Լ�
    void Move()
    {
        // Ű���� �Է°� ��������
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // ���̽�ƽ �Է°� ����
        if (joyControl[0]) { h = -1; v = 1; }
        if (joyControl[1]) { h = 0; v = 1; }
        if (joyControl[2]) { h = 1; v = 1; }
        if (joyControl[3]) { h = -1; v = 0; }
        if (joyControl[4]) { h = 0; v = 0; }
        if (joyControl[5]) { h = 1; v = 0; }
        if (joyControl[6]) { h = -1; v = -1; }
        if (joyControl[7]) { h = 0; v = -1; }
        if (joyControl[8]) { h = 1; v = -1; }

        // ȭ�� ��� �浹 �� �̵� ����
        if ((isTouchRight && h == -1) || (isTouchLeft && h == 1) || !isControl)
            h = 0;
        if ((isTouchTop && v == 1) || (isTouchBottom && v == -1) || !isControl)
            v = 0;

        // ���� ��ġ���� �Է°��� ���� �̵� ���
        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        // ��ġ ������Ʈ
        transform.position = curPos + nextPos;

        // �ִϸ��̼� ������Ʈ
        if (Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal"))
        {
            animator.SetInteger("Input", (int)h);
        }
    }
    void Boom()
    {
        if (!isButtonB || isBoomTime || boom == 0)
            return;

        boom--;
        isBoomTime = true;
        gameManager.UpdateBoomIcon(boom); // UI ������Ʈ

        // ��ź ����Ʈ Ȱ��ȭ
        boomEffect.SetActive(true);
        Invoke("OffBomeEffect", 4f); // 4�� �� ��ź ����Ʈ ��Ȱ��ȭ

        // ���� �� �Ѿ� ����
        RemoveEnemies();
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
    public void ButtonADown()
    {
        isButtonA = true;
    }
    public void ButtonAUp()
    {
        isButtonA = false;
    }
    public void ButtonBDown()
    {
        isButtonB = true;
    }

    void Fire()
    {


        // ���� �Ŀ� ������ ���� �Ѿ� ����
        switch (Power)
        {
            case 1:
                CreateBullet("BulletPlayerA", Vector3.zero);
                break;
            case 2:
                CreateBullet("BulletPlayerA", Vector3.right * 0.1f);
                CreateBullet("BulletPlayerA", Vector3.left * 0.1f);
                break;
            default:
                CreateBullet("BulletPlayerA", Vector3.right * 0.35f);
                CreateBullet("BulletPlayerB", Vector3.zero);
                CreateBullet("BulletPlayerA", Vector3.left * 0.35f);
                break;
        }
        CurShotDelay = 0; // �߻� ������ �ʱ�ȭ
    }

    // �Ѿ� ���� �Լ�
    void CreateBullet(string type, Vector3 offset)
    {
        GameObject bullet = objectManager.MakeObj(type);
        bullet.transform.position = transform.position + offset;
        bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10, ForceMode2D.Impulse);
    }
    void Reload()
    {
        CurShotDelay += Time.deltaTime;
    }
    // �浹 ó�� �Լ�
    void OnTriggerEnter2D(Collider2D collision)
    {
        // ȭ�� ���� �浹
        if (collision.gameObject.tag == "Border")
        {
            SetBorderTouch(collision.gameObject.name, true);
        }
        // �� �Ǵ� �� �Ѿ˰� �浹
        else if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet")
        {
            HandleCollisionWithEnemy(collision);
        }
        // �����۰� �浹
        else if (collision.gameObject.tag == "Item")
        {
            HandleItemPickup(collision.gameObject.GetComponent<Item>());
        }
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

    // �� �Ǵ� �� �Ѿ˰� �浹 ó��
    void HandleCollisionWithEnemy(Collider2D collision)
    {
        if (isRespawnTime || isHit)
            return;

        isHit = true;
        life--;
        gameManager.UpdateLifeIcon(life);
        gameManager.CallExplosion(transform.position, "P");

        if (life == 0)
        {
            gameManager.GameOver();
        }
        else
        {
            gameManager.RespawnPlayer();
        }

        collision.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    // ������ ȹ�� ó��
    void HandleItemPickup(Item item)
    {
        switch (item.type)
        {
            case "Coin":
                score += 1000;
                break;
            case "Power":
                if (Power < maxPower)
                {
                    Power++;
                    AddFollower();
                }
                else
                {
                    score += 500;
                }
                break;
            case "Boom":
                if (boom < maxboom)
                {
                    boom++;
                    gameManager.UpdateBoomIcon(boom);
                }
                else
                {
                    score += 500;
                }
                break;
        }
        Destroy(item.gameObject);
    }
    // ���� ������Ʈ Ȱ��ȭ
    void AddFollower()
    {
        if (Power >= 4 && Power - 4 < followers.Length)
        {
            followers[Power - 4].SetActive(true);
        }
    }

    // ��ź ����Ʈ ��Ȱ��ȭ
    void OffBomeEffect()
    {
        boomEffect.SetActive(false);
        isBoomTime = false;
    }

    // ȭ�� ��踦 ��� �� ȣ��
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            SetBorderTouch(collision.gameObject.name, false);
        }
    }
}
