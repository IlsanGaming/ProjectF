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
    Animator anim;

    public ObjectManager objectManager;
    public GameManager gameManager;

    // 적의 패턴을 관리하기 위한 변수
    public int patternIndex; // 현재 패턴 인덱스
    public int curPatternCount; // 현재 패턴 반복 횟수
    public int[] maxPatternCount; // 각 패턴의 최대 반복 횟수


    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();


        // 적 이름이 "B"인 경우 애니메이터 컴포넌트 가져오기 (보스만 해당)
        if (enemyName == "B")
            anim = GetComponent<Animator>();
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
            case "B":
                health = 10;
                Invoke("Stop", 2.5f);
                break;

        }
    }

    // 보스 적을 멈추게 하는 함수
    void Stop()
    {
        if (!gameObject.activeSelf) return;

        // Rigidbody2D를 이용해 적의 속도를 0으로 설정
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.zero;

        // 패턴 전환을 위한 Think 함수 호출
        Invoke("Think", 2);
    }

    // 다음 공격 패턴을 결정하는 함수
    void Think()
    {
        // 패턴 인덱스 순환 (0~3)
        patternIndex = patternIndex == 3 ? 0 : patternIndex + 1;
        curPatternCount = 0; // 현재 패턴 카운트를 초기화

        // 패턴에 따라 다른 공격 수행
        switch (patternIndex)
        {
            case 0:
                FireForward(); // 직선 발사
                break;
            case 1:
                FireShot(); // 샷건 형태 발사
                break;
            case 2:
                FireArc(); // 부채꼴 형태 발사
                break;
            case 3:
                FireAround(); // 원형 발사
                break;
        }
    }

    // 직선 발사 패턴
    void FireForward()
    {
        if (health <= 0) return;

        Debug.Log("앞으로 4발 발사.");
        // 적의 위치를 기준으로 좌우에 총알 생성
        GameObject bulletR = objectManager.MakeObj("bulletBossA");
        bulletR.transform.position = transform.position + Vector3.right * 0.3f;
        GameObject bulletRR = objectManager.MakeObj("bulletBossA");
        bulletRR.transform.position = transform.position + Vector3.right * 0.45f;
        GameObject bulletL = objectManager.MakeObj("bulletBossA");
        bulletL.transform.position = transform.position + Vector3.left * 0.3f;
        GameObject bulletLL = objectManager.MakeObj("bulletBossA");
        bulletLL.transform.position = transform.position + Vector3.left * 0.45f;

        // 생성된 총알에 힘을 가해 아래 방향으로 이동
        Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();

        rigidR.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidRR.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidL.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidLL.AddForce(Vector2.down * 8, ForceMode2D.Impulse);

        // 패턴 반복 관리
        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireForward", 2);
        else
            Invoke("Think", 2);
    }

    // 샷건 패턴 (플레이어 방향으로 여러 발사체)
    void FireShot()
    {
        if (health <= 0) return;

        Debug.Log("플레이어 방향으로 샷건.");
        for (int index = 0; index < 5; index++)
        { 
            // 총알 생성
            GameObject bullet = objectManager.MakeObj("bulletBossB");
            bullet.transform.position = transform.position;

            Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
            // 플레이어 방향으로 약간 랜덤한 궤적 추가
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

    // 부채꼴 패턴
    void FireArc()
    {
        if (health <= 0) return;

        Debug.Log("부채모양으로 발사");
        GameObject bullet = objectManager.MakeObj("bulletEnemyB");
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.identity;

        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
        // 각도를 계산하여 부채꼴 형태로 총알 발사
        Vector3 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 10 * curPatternCount / maxPatternCount[patternIndex]), -1);
        rigidbody.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);

        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireArc", 0.15f);
        else
            Invoke("Think", 3);
    }

    // 원형 발사 패턴
    void FireAround()
    {
        if (health <= 0) return;

        Debug.Log("원 형태로 전체 공격");
        int roundNum = curPatternCount % 2 == 0 ? 50 : 40; // 발사체 수 변경
        for (int index = 0; index < roundNum; index++)
        {
            GameObject bullet = objectManager.MakeObj("bulletEnemyA");
            bullet.transform.position = transform.position;

            Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
            // 원형으로 발사체 방향 계산
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
        if (enemyName == "B") return; // 보스는 별도 패턴
        if (!GameManager.instance.isLive)
        {
            return;
        }
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

        // 보스 적일 경우 애니메이터 트리거 호출
        if (enemyName == "B")
        {
            anim.SetTrigger("OnHit");
        }
        else
        {
            // 일반 적일 경우 맞은 상태의 스프라이트로 변경
            spriteRenderer.sprite = sprites[1];
            // 0.1초 후 원래 스프라이트로 복구
            Invoke("ReturnSprite", 0.1f);
        }

        if (health <= 0)
        {
            gameManager.GetExp();
            DropItem();
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
            gameManager.CallExplosion(transform.position, enemyName);
            // 보스 적을 처치한 경우 스테이지 종료 처리
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
        // 적이 특정 태그를 가진 오브젝트와 충돌했을 때 처리
        if (collision.gameObject.tag == "Borderbullet" && enemyName != "B")
        {
            // 적이 화면 경계를 벗어난 경우 비활성화
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
            gameManager.CallExplosion(transform.position, "P");
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
