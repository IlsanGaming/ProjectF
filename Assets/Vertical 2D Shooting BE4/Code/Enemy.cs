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
    public float speed;//이동속도
    public int health;//체력
    public Sprite[] sprites;
    public float curShotSpeed;//장전속도
    public float maxShotSpeed;//최대 장전속도
    public float enemyBulletSpeed;//적 투사체 속도
    public int enemyExp;
    public int enemyLevel;

    public bool isClear; 

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
        instance = this;
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

        // 체력을 포함한 상태 초기화
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
                health = 30; // 보스 체력 고정
                Invoke("Stop", 1.6f);
                break;
        }

        curShotSpeed = 0; // 장전 속도 초기화
    }
    // 보스 적을 멈추게 하는 함수
    void Stop()
    {
        Debug.Log("Stop실행완료");
        if (!gameObject.activeSelf) return;

        // Rigidbody2D를 이용해 적의 속도를 0으로 설정
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.zero;

        // 패턴 전환을 위한 Think 함수 호출
        Invoke("Think", 2.5f);
    }

    // 다음 공격 패턴을 결정하는 함수
    void Think()
    {
        Debug.Log("Think실행완료");
        if (!GameManager.instance.isLive)
        {
            return;
        }
        // 패턴 인덱스 순환 (0~3)
        patternIndex = patternIndex == 3 ? 0 : patternIndex + 1;
        curPatternCount = 0; // 현재 패턴 카운트를 초기화
        Debug.Log("Think실행완료2");
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
        Debug.Log("Think실행완료3");
    }

    // 직선 발사 패턴
    void FireForward()
    {
        if (health <= 0) return;

        Debug.Log("앞으로 4발 발사.");
        // 적의 위치를 기준으로 좌우에 총알 생성
        GameObject bulletR = objectManager.MakeObj("bulletBossA");
        bulletR.transform.position = transform.position + Vector3.right * 0.4f;
        GameObject bulletRR = objectManager.MakeObj("bulletBossA");
        bulletRR.transform.position = transform.position + Vector3.right * 1.2f;
        GameObject bulletL = objectManager.MakeObj("bulletBossA");
        bulletL.transform.position = transform.position + Vector3.left * 0.4f;
        GameObject bulletLL = objectManager.MakeObj("bulletBossA");
        bulletLL.transform.position = transform.position + Vector3.left * 1.2f;

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
        int roundNum = curPatternCount % 2 == 0 ? 50 : 40; // 발사체 수 변경
        CreateAround("bulletEnemyB");
        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireAround", 0.7f);
        else
            Invoke("Think", 3);
    }
    void CreateAround(string type)
    {
        int roundNum = curPatternCount % 2 == 0 ? 50 : 40; // 발사체 수 변경
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
        AudioManager.instance.PlaySfx(AudioManager.Sfx.EnemyHit);

        if (health <= 0)
        {
            health = 0; // 체력을 0으로 고정
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
                collision.gameObject.SetActive(false);
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
