using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEditor.Progress;

public class Player : MonoBehaviour
{
    public static Player instance;
    public int skill1Level;
    public int skill2Level;
    public int skill3Level;
    public int skill4Level;
    public int skill5Stack;
    public int PspeedLevel;
    public int BspeedLevel;
    public int AspeedLevel;
    public int WeightLevel;



    public int maxskill1Level;
    public int maxskill2Level;
    public int maxskill3Level;
    public int maxskill4Level;
    public int maxskill5Stack;
    public int maxPspeedLevel;
    public int maxBspeedLevel;
    public int maxAspeedLevel;
    public int maxWeightLevel;

    public float speed;//이동속도
    public float maxSpeed;



    public float health;
    public float maxhealth;
     
    public float curVerticalBulletSpeed;//투사체 속도
    public float curCharge;//충전 속도
    public float maxCharge;//최대 충전 속도
    public float curShotSpeed;//장전속도
    public float maxShotSpeed;//최대 장전속도
    public float lightness;//플레이어가 적, 적 총알에 닿았을때 변화하는 정도


    // 화면 경계와의 충돌 상태를 나타내는 변수
    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchRight;
    public bool isTouchLeft;


    public GameManager gameManager;
    public ObjectManager objectManager;
    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public GameObject boomEffect;
    public GameObject[] followers;
    public Scanner scanner;
    Rigidbody2D rigid;


    Animator anim;

    public VariableJoystick joy;
    void Awake()
    {
        instance = this; // Player 인스턴스 초기화
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!GameManager.instance.isLive)
        {
            return;
        }
        // 매 FixedUpdate 마다 Move() 호출
        Move();
        Skill1();
        Skill1Reload();
        Skill2();
        Skill3();
        Skill4();
        Skill4Charge();
    }
    void LateUpdate()
    {
        if (!GameManager.instance.isLive)
        {
            return;
        }
        SetPspeed();
        SetBspeed();
        SetAspeed();
        SetWeight();
    }
    void SetPspeed()
    {
        switch(PspeedLevel)
        {
            case 0:
                speed = 5f;
                break;
            case 1:
                speed = 5.2f;
                break;
            case 2:
                speed = 5.6f;
                break;
            case 3:
                speed = 6.4f;
                break;
            case 4:
                speed = 8f;
                break;
        }
    }
    void SetBspeed()
    {
        switch (BspeedLevel)
        {
            case 0:
                curVerticalBulletSpeed = 10f;
                break;
            case 1:
                curVerticalBulletSpeed = 11f;
                break;
            case 2:
                curVerticalBulletSpeed = 12f;
                break;
            case 3:
                curVerticalBulletSpeed = 13f;
                break;
            case 4:
                curVerticalBulletSpeed = 14f;
                break;
        }
    }
    void SetAspeed()
    {
        switch (AspeedLevel)
        {
            case 0:
                maxShotSpeed = 1f;
                break;
            case 1:
                maxShotSpeed = 0.92f;
                break;
            case 2:
                maxShotSpeed = 0.78f;
                break;
            case 3:
                maxShotSpeed = 0.6f;
                break;
            case 4:
                maxShotSpeed = 0.36f;
                break;
        }                
    }
    void SetWeight()
    {
        switch (WeightLevel)
        {
            case 0:
                lightness = 15f;
                break;
            case 1:
                lightness = 14.5f;
                break;
            case 2:
                lightness = 12.7f;
                break;
            case 3:
                lightness = 11.5f;
                break;
            case 4:
                lightness = 10f;
                break;
        }
    }
    public void HealthCharge()
    {
        health=maxhealth;
    }

    void Move()
    {
        float h = joy.Horizontal;
        float v = joy.Vertical;
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
        if (curShotSpeed < maxShotSpeed)
            return;

        switch (skill1Level)
        {
            case 0:
                CreateSkill1("bulletPlayerA", Vector3.zero, new Color(255, 228, 0)); // 기본 색상
                break;
            case 1:
                CreateSkill1("bulletPlayerA", Vector3.right * 0.1f, new Color(29, 219, 22));
                CreateSkill1("bulletPlayerA", Vector3.left * 0.1f, new Color(29, 219, 22));
                break;
            case 2:
                CreateSkill1("bulletPlayerB", Vector3.zero, new Color(255, 94, 0));
                break;
            case 3:
                CreateSkill1("bulletPlayerB", Vector3.right * 0.25f, new Color(95, 0, 255));
                CreateSkill1("bulletPlayerB", Vector3.left * 0.25f, new Color(95, 0, 255));
                break;
            case 4:
                CreateSkill1("bulletPlayerA", new Vector3(0.65f, -0.25f, 0f), new Color(189, 189, 189));
                CreateSkill1("bulletPlayerB", Vector3.right * 0.25f, new Color(189, 189, 189));
                CreateSkill1("bulletPlayerB", Vector3.left * 0.25f, new Color(189, 189, 189));
                CreateSkill1("bulletPlayerA", new Vector3(-0.65f, -0.25f, 0f), new Color(189, 189, 189));
                break;
            case 5:
                CreateSkill1("bulletPlayerA", new Vector3(0.65f, -0.25f, 0f), new Color(255, 187, 0));
                CreateSkill1("bulletPlayerB", Vector3.right * 0.25f, new Color(255, 187, 0));
                CreateSkill1("bulletPlayerB", Vector3.left * 0.25f, new Color(255, 187, 0));
                CreateSkill1("bulletPlayerA", new Vector3(-0.65f, -0.25f, 0f), new Color(255, 187, 0));
                CreateSkill1("bulletPlayerA", new Vector3(-0.25f, -0.65f, 0f), new Color(255, 187, 0));
                CreateSkill1("bulletPlayerA", new Vector3(0.25f, -0.65f, 0f), new Color(255, 187, 0));
                break;
            case 6:
                CreateSkill1("bulletPlayerB", Vector3.right * 0.25f, new Color(255, 0, 127));
                CreateSkill1("bulletPlayerB", Vector3.left * 0.25f, new Color(255, 0, 127));
                CreateSkill1("bulletPlayerB", new Vector3(-0.65f, -0.5f, 0f), new Color(255, 0, 127));
                CreateSkill1("bulletPlayerB", new Vector3(0.65f, -0.5f, 0f), new Color(255, 0, 127));
                break;
        }
        curShotSpeed = 0;
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);
    }
    void Skill1Reload()
    {
        curShotSpeed += Time.deltaTime;
    }

    void CreateSkill1(string type, Vector3 offset, Color bulletColor)
    {
        GameObject bullet = objectManager.MakeObj(type);
        bullet.transform.position = transform.position + offset;

        // SpriteRenderer를 통해 투사체 색상 변경
        SpriteRenderer spriteRenderer = bullet.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = bulletColor;
        }

        bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.up * curVerticalBulletSpeed, ForceMode2D.Impulse);
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
    void Skill3()
    {
        if (curShotSpeed < maxShotSpeed)
            return;
        if (!scanner.nearestTarget)
            return;
        switch (skill3Level)
        {
            case 1:
                CreateSkill3("bulletChase", Vector3.zero);
                break;
            case 2:
                CreateSkill3("bulletChase", Vector3.right * 0.2f);
                CreateSkill3("bulletChase", Vector3.left * 0.2f);
                break;
            case 3:
                CreateSkill3("bulletChase", Vector3.zero);
                CreateSkill3("bulletChase", Vector3.right * 0.2f);
                CreateSkill3("bulletChase", Vector3.left * 0.2f);
                break;
            case 4:
                CreateSkill3("bulletChase", Vector3.right * 0.1f);
                CreateSkill3("bulletChase", Vector3.left * 0.1f);
                CreateSkill3("bulletChase", new Vector3(-0.1f,0.5f,0));
                CreateSkill3("bulletChase", new Vector3(0.1f, 0.5f, 0));
                break;
            case 5:
                CreateSkill3("bulletChase", new Vector3(-0.1f, -0.5f, 0));
                CreateSkill3("bulletChase", Vector3.zero);
                CreateSkill3("bulletChase", new Vector3(0.1f, -0.5f, 0));
                CreateSkill3("bulletChase", new Vector3(-0.2f, -1f, 0));
                CreateSkill3("bulletChase", new Vector3(0.2f, -1f, 0));
                break;
        }
    }
    void Skill4()
    {
        // 체력이 최대일 경우 스킬 사용 불가
        if (health >= maxhealth)
        {
            health = maxhealth;
            return;
        }
        // 현재 충전량이 최대 충전에 도달하지 않았을 경우 스킬 사용 불가
        if (curCharge < maxCharge)
            return;

        // 스킬 레벨에 따른 체력 회복
        float gainMultiplier = 1f;
        switch (skill4Level)
        {
            case 0:
                curCharge = 0;
                return;
            case 1:
                gainMultiplier = 0.3f;
                break;
            case 2:
                gainMultiplier = 0.4f;
                break;
            case 3:
                gainMultiplier = 0.5f;
                break;
            case 4:
                gainMultiplier = 0.55f;
                break;
            case 5:
                gainMultiplier = 0.6f;
                break;
        }
        HealthGain(gainMultiplier);

        // 충전량 초기화
        curCharge = 0;
    }

    void Skill4Charge()
    {
        // 시간에 따라 충전량 증가
        curCharge += Time.deltaTime;
    }

    void HealthGain(float gainMultiplier)
    {
        // 회복량 계산
        float healAmount = health * (0.1f * gainMultiplier);

        // 체력 회복
        health += healAmount;

        // 체력이 최대 체력을 초과하지 않도록 제한
        if (health > maxhealth)
            health = maxhealth;
    }

    void CreateSkill3(string type, Vector3 offset)
    {
        Vector3 targetPos= scanner.nearestTarget.position;
        Vector3 dir= targetPos- transform.position;
        dir= dir.normalized;
        GameObject bullet = objectManager.MakeObj(type);
        bullet.transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.transform.position = transform.position + offset;
        bullet.GetComponent<Rigidbody2D>().AddForce(dir * curVerticalBulletSpeed*1.2f, ForceMode2D.Impulse);
    }
    public void ButtonSkillDown()
    {
        Skill5();
    }
    void Skill5()
    {
        if (skill5Stack==0)
            return;
        skill5Stack--;
        boomEffect.SetActive(true);
        Invoke("OffBoomEffect", 1.5f);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int index = 0; index < enemies.Length; index++)
        {
            Enemy enemyLogic = enemies[index].GetComponent<Enemy>();
            enemyLogic.OnHit(10000);
        }
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        for (int index = 0; index < bullets.Length; index++)
        {
            RemoveBullets("BulletEnemyA");
            RemoveBullets("BulletEnemyB");
            RemoveBullets("bulletBossA");
            RemoveBullets("bulletBossB");
        }
    }
    // 적 총알 제거 처리 함수
    void RemoveBullets(string type)
    {
        GameObject[] bullets = objectManager.GetPool(type);
        foreach (var bullet in bullets)
            if (bullet.activeSelf) bullet.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // 화면 경계와 충돌
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
    public void HandleItemPickup(Item item)
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Pickup);
        switch (item.type)
        {
            case "Health":
                health *=1.1f;
                if (health >= maxhealth)
                {
                    health = maxhealth; 
                }
                Debug.Log("Health Restored");
                break;

            case "Exp":
                gameManager.GetExp(Mathf.FloorToInt(GameManager.instance.nextExp[GameManager.instance.level]*0.1f));
                break;

            case "Boom":
                if (skill5Stack == maxskill5Stack)
                {
                    gameManager.GetExp(5);
                    Debug.Log("Skill5 Stack Full: Extra Experience Gained");
                }
                else
                {
                    skill5Stack++;
                    Debug.Log("Skill5 Stack Increased");
                }
                break;

            default:
                Debug.LogWarning($"Unknown item type: {item.type}");
                break;
        }
        // 아이템을 비활성화하고 풀로 반환
        item.gameObject.SetActive(false);
    }
    void OffBoomEffect()
    {
        boomEffect.SetActive(false);
    }
    void OnDamaged(Vector3 targetPos)
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.PlayerHit);
        Debug.Log("OnHit!");

        // 기존 속도를 초기화하여 밀려나는 효과가 계속되지 않도록 설정
        rigid.velocity = Vector2.zero;

        // 타격 방향 계산
        Vector3 dirVector = (transform.position - targetPos).normalized;

        // 밀려나는 힘 추가
        StartCoroutine(KnockBack(dirVector));

        // 체력 감소
        health -= 1.48f;

        // 체력이 0보다 작아질 경우 게임 오버 처리
        if (health <= 0)
        {
            StageClear();
            gameManager.StageLose();
        }
    }
    IEnumerator KnockBack(Vector3 direction)
    {
        float knockBackDuration = 0.1f; // 밀려남 지속 시간
        float knockBackTimer = 0;

        while (knockBackTimer < knockBackDuration)
        {
            rigid.velocity = direction * lightness;
            knockBackTimer += Time.deltaTime;
            yield return null;
        }

        // 밀려남 후 속도를 초기화
        rigid.velocity = Vector2.zero;
    }

    // 화면 경계 충돌 상태 설정
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
    // 화면 경계를 벗어날 때 호출
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            SetBorderTouch(collision.gameObject.name, false);
            rigid.constraints = RigidbodyConstraints2D.None;
        }
    }
    public void StageClear()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int index = 0; index < enemies.Length; index++)
        {
            Enemy enemyLogic = enemies[index].GetComponent<Enemy>();
            enemyLogic.transform.localScale= Vector3.zero;
        }
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        for (int index = 0; index < bullets.Length; index++)
        {
            Bullet bulletLogic = bullets[index].GetComponent<Bullet>();
            bulletLogic.transform.localScale= Vector3.zero;
        }
    }
}
